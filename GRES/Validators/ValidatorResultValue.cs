using System;

namespace GRES.Validators;

internal readonly struct ValidateErrorValue(string message, in Range distance)
{
    internal readonly string Message = message;
    internal readonly Range Distance = distance;
}
internal readonly ref struct ValidatorResultValue(bool isMatch, ReadOnlySpan<ValidateErrorValue> errors)
{
    internal readonly bool IsMatch = isMatch;
    internal readonly ReadOnlySpan<ValidateErrorValue> Errors = errors;
}
