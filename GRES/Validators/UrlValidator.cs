using System;
using System.Text.RegularExpressions;

namespace GRES.Validators;

internal partial class UrlValidator : IValidator<string>
{
    private const string PATTERN = @"\{(\w+(\:\w+)?)\}";
    public bool Validate(string entity)
    {
        if(string.IsNullOrEmpty(entity) || string.IsNullOrWhiteSpace(entity))
            return false;

        return UrlRegex().IsMatch(entity.AsSpan());
    }


    [GeneratedRegex(PATTERN, RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex UrlRegex();
}
