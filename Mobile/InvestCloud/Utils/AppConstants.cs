namespace InvestCloud.Utils;

public static class AppConstants
{
    // you can change this name based on whatever you need
    public static readonly string DatabaseName = "greenfield.db3";
    
    // First Name and Last Name must not have these special characters (!@#$%^&)
    public static readonly string IgnoreSpecialCharacters = "[(!@#$%^&)]";
    
    public static readonly string LowerCaseLetter = "[a-z]";
    public static readonly string UpperCaseLetter = "[A-Z]";
    
    //Password must have from 8 to 15 characters
    public const int MinCharacterLength = 8;
    public const int MaxCharacterLength = 15;
    
    public static readonly string MaskedPhoneNumberFormat = "(___)-___-____";
}