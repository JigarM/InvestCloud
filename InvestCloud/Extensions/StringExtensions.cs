using System.Text.RegularExpressions;
using InvestCloud.Utils;

namespace InvestCloud.Extensions;

public static class StringExtensions
{
    public static bool HasSpecialCharacters(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        
        var regex = new Regex(AppConstants.IgnoreSpecialCharacters);
        return regex.IsMatch(text);
    }
    
    public static bool HasValidPasswordCharacterLength(this string text)
    {
        return !string.IsNullOrWhiteSpace(text) && 
               text.Length >= AppConstants.MinCharacterLength && 
               text.Length <= AppConstants.MaxCharacterLength;
    }
    
    public static bool HasOneLowerCaseLetter(this string text)
    {
        var regex = new Regex(AppConstants.LowerCaseLetter);
        return regex.IsMatch(text);
    }
    
    public static bool HasOneUpperCaseLetter(this string text)
    {
        var regex = new Regex(AppConstants.UpperCaseLetter);
        return regex.IsMatch(text);
    }
    
    public static bool HasAnyRepetitiveSequenceOfCharacters(this string text)
    {
        var repeat1CharRegex = new Regex(@"(.)\1"); // Repetitive any Sequence Of 1 continous Characters
        var repeat2CharRegex = new Regex(@"(..)\1"); // Repetitive any Sequence Of 2 continous Characters
        var repeat3CharRegex = new Regex(@"(...)\1"); // Repetitive any Sequence Of 3 continous Characters
        var repeat4CharRegex = new Regex(@"(....)\1"); // Repetitive any Sequence Of 4 continous Characters
        
        return repeat1CharRegex.IsMatch(text) || 
               repeat2CharRegex.IsMatch(text) || 
               repeat3CharRegex.IsMatch(text) || 
               repeat4CharRegex.IsMatch(text);
    }
}