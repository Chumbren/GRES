using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using GRES.Exceptions;
using GRES.Validators;

namespace GRES.Routing.Address;


internal class Url
{
    public const string PATTERN = @"\{(\w+(\:\w+)?)\}";

    private readonly ref struct ParameterStringDTO(string name, string nameType)
    {
        internal readonly string Name = name;
        internal readonly string NameType = nameType;
    }

    internal readonly IAddress[] Address;

    internal readonly string Source;
    internal Url(string source, IAddress[] addresses)
    {
        Source = source;
        Address = addresses;
    }

    public static implicit operator Url(string address)
    {
        var resultParse = ParseStringToAddresses(address);
        
        ArgumentNullException.ThrowIfNull(resultParse.Value);

        Url url = new(address, resultParse.Value);
        return url;
    }

    public static bool operator==(Url left,  Url right) => left.Equals(right);
    public static bool operator!=(Url left,  Url right) => !left.Equals(right);

    public override bool Equals(object? obj)
    {
        Url? url = null;
        if(obj is string @string)
            url = Url.Parse(@string);
        else if(obj is Url _url) 
            url = _url;
        
        if(url is null)
            return false;
        
        foreach (var address in Address)
        {
            if(address is Parameter parameter)
            {
                
            }
            else if(address is Address addressString)
            {
                
            }
        }
    }

    private bool CompareWithString(string @string)
    {
        Span<Range> buffer = stackalloc Range[MAX_LENGTH_BUFFER];
        var written = SplitString(@string, buffer);
        
        if(written != Address.Length)
            return false;
        
        for(int i = 0; i < written; i++)
        {
            Range distance = buffer[i];
            ReadOnlySpan<char> segment = @string[distance];

            if(Address[i] is Address address && address.Source != segment)
                return false;
            else if(Address[i] is Parameter parameter && !parameter.Equals(@string))
                return false;
        }
    }

    private static bool IsCompareWithString(IAddress address) => ;
    private static bool IsCompareWithStringParameter(IAddress address) => ;

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private const int MAX_LENGTH_BUFFER = 50;

    private readonly struct ParseError(string message, in Range distance)
    {
        internal readonly string Message = message;
        internal readonly Range Distance = distance;
    }

    private readonly struct ParseResult<TValue>(TValue? value, List<ParseError> errors)
    {
       internal readonly TValue? Value = value;
       internal readonly List<ParseError> Errors = errors;

       internal bool IsError() => Errors.Count > 0 && Value == null;
    }

    internal static Url Parse(string address)
    {
        var resultParse = ParseStringToAddresses(address);
        if(resultParse.IsError())
            ThrowErrors(resultParse.Errors);

        ArgumentNullException.ThrowIfNull(resultParse.Value);

        Url url = new(address, resultParse.Value);
        return url;
    }

    internal static bool TryParse(string address, out Url? url)
    {
        var resultParse = ParseStringToAddresses(address);
        if(resultParse.IsError())
        {
            url = null;
            return false;
        }

        if(resultParse.Value == null)
            throw new ArgumentNullException(nameof(address),  "Результат парсинга пустой но ошибок не найдено");

        url = new(address, resultParse.Value);
        return true;
    }

    private static void ThrowErrors(List<ParseError> errors)
    {
        string message = "";
        foreach (var error in errors)
            message += error.Message;
        throw new FormatException(message);
    }

    private static ParseResult<IAddress[]> ParseStringToAddresses(string address)
    {
        Span<Range> buffer = stackalloc Range[MAX_LENGTH_BUFFER];
        var written = SplitString(address, buffer);

        var result = GetAddresses(address, written, buffer);
        return result;
    }

    private static ParseResult<IAddress[]> GetAddresses(string address, int wrriten, in ReadOnlySpan<Range> buffer)
    {
        IAddress[] addresses = new IAddress[wrriten];
        for (int i = 0, indexAddress = 0; i < wrriten; i++, indexAddress++)
        {
            Range distance = buffer[i];
            ReadOnlySpan<char> segment = address[distance];
            
            var resultParse = GetAddress(segment, distance.Start.Value);
            if(resultParse.IsError())
                return new(null, resultParse.Errors);

            ArgumentNullException.ThrowIfNull(resultParse.Value);

            addresses[indexAddress] = resultParse.Value;
        }

        return new(addresses, []);
    }

    private static ParseResult<IAddress> GetAddress(in ReadOnlySpan<char> segment, in int startIndex)
    {
        ParseResult<IAddress> result;
        if (segment[0] == '{')
        {
                ParseError? error = segment[^1] != '}'
                    ? new("Ожидалось в конце определния параметра '}'", startIndex..(segment.Length+startIndex))
                    : null;
                ReadOnlySpan<char> @string = segment[1..(segment.Length-1)];
                
                var resultParameter = ConvertStringToParameter(@string);
                List<ParseError> errors = error != null ? [(ParseError)error] : [];
                result = new(resultParameter.Value, errors);
        }
        else result = new(new Address(segment.ToString()), []);
         
        return result;
    }

    private static int SplitString(string address, in Span<Range> buffer)
    {
        ReadOnlySpan<char> spanAddress = address.AsSpan();
        int written = spanAddress.Split(buffer,
                                        '.', 
                                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        return written;
    }

    private static TypeParameter ConvertStringToTypeParameter(in ReadOnlySpan<char> @string)
    {
        if(@string.IsEmpty)
            ArgumentNullException.ThrowIfNullOrEmpty(@string.ToString());
        
        TypeParameter type = @string switch
        {
          "i32" => TypeParameter.Int32,
           "str" => TypeParameter.String,
           _ => throw new FormatException($"Строка не является типом параметра {@string} ")
        };

        return type;
    }

    private static ParseResult<Parameter> ConvertStringToParameter(in ReadOnlySpan<char> @string)
    {
        string name = "";
        string type = "";
        bool isStartDefinitionType = false;
        foreach (var @char in @string)
        {
            switch (@char, isStartDefinitionType)
            {
                case (':', false):
                    isStartDefinitionType = true;
                    break;
                case (_, true):
                    type += @char;
                    break;
                default:
                    name += @char;
                    break;
            }
        }
        var parameter = new Parameter(name, ConvertStringToTypeParameter(@string),
                                     @string.ToString());
        return new(parameter, []);
    }
}
