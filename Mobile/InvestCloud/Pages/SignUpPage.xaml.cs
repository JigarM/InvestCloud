using InvestCloud.Extensions;
using InvestCloud.Resources.Strings;
using InvestCloud.Utils;
using InvestCloud.ViewModels;
using Syncfusion.Maui.Inputs;

namespace InvestCloud.Pages;

public partial class SignUpPage : ContentPage
{
    private SignUpViewModel vm;
    
    public SignUpPage(SignUpViewModel signUpViewModel)
    {
        InitializeComponent();
        BindingContext = vm = signUpViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.InitializeAsync();
        
        FirstNameEntry.Text = string.Empty;
        LastNameEntry.Text = string.Empty;
        UsernameEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        ServiceStartDatePicker.Date = DateTime.Today;
    }

    private void OnFirstNameChanged(object sender, TextChangedEventArgs e)
    {
        var firstNameEntry = sender as Entry;
        FirstNameTextInputLayout.HasError = firstNameEntry.Text.HasSpecialCharacters();
        vm.IsValidFirstName = !FirstNameTextInputLayout.HasError;
        UpdateSignUpLayout(sender, e);
    }
    
    private void OnLastNameChanged(object sender, TextChangedEventArgs e)
    {
        var lastNameEntry = sender as Entry;
        LastNameTextInputLayout.HasError = lastNameEntry.Text.HasSpecialCharacters();
        vm.IsValidLastName = !LastNameTextInputLayout.HasError;
        UpdateSignUpLayout(sender, e);
    }
    
    private void OnPasswordChanged(object sender, TextChangedEventArgs e)
    {
        var passwordEntry = sender as Entry;
        if (string.IsNullOrWhiteSpace(passwordEntry.Text))
        {
            PasswordTextInputLayout.HasError = vm.IsValidPassword = false;
            return;
        }
        
        //Password must have from 8 to 15 characters
        var hasValidPasswordLength = passwordEntry.Text.HasValidPasswordCharacterLength();
        PasswordTextInputLayout.HasError = !hasValidPasswordLength;
        PasswordTextInputLayout.ErrorText = hasValidPasswordLength ? string.Empty : AppResources.PasswordMinAndMaxCharErrorText;

        //Password must have at least one lowercase letter
        var hasOneLowerCaseLetter = false;
        if (hasValidPasswordLength)
        {
            hasOneLowerCaseLetter = passwordEntry.Text.HasOneLowerCaseLetter();
            PasswordTextInputLayout.HasError = !hasOneLowerCaseLetter;
            PasswordTextInputLayout.ErrorText = hasOneLowerCaseLetter ? string.Empty : AppResources.PasswordOneLowerCaseErrorText;
        }
        
        //Password must have at least one uppercase letter
        var hasOneUpperCaseLetter = false;
        if (hasValidPasswordLength && hasOneLowerCaseLetter)
        {
            hasOneUpperCaseLetter = passwordEntry.Text.HasOneUpperCaseLetter();
            PasswordTextInputLayout.HasError = !hasOneUpperCaseLetter;
            PasswordTextInputLayout.ErrorText = hasOneUpperCaseLetter ? string.Empty : AppResources.PasswordOneUpperCaseErrorText;
        }

        //Password must not have repetitive any sequence of characters (i.e. “abcabc”, “111”, “12ab12ab” are not allowed)
        var hasRepetitiveAnySequenceOfLetters = false;
        if (hasValidPasswordLength && hasOneLowerCaseLetter && hasOneUpperCaseLetter)
        {
            hasRepetitiveAnySequenceOfLetters = passwordEntry.Text.HasAnyRepetitiveSequenceOfCharacters();
            PasswordTextInputLayout.HasError = hasRepetitiveAnySequenceOfLetters;
            PasswordTextInputLayout.ErrorText = hasRepetitiveAnySequenceOfLetters ? AppResources.PasswordRepetitiveErrorText : string.Empty;
        }

        vm.IsValidPassword = !PasswordTextInputLayout.HasError;
        UpdateSignUpLayout(sender, e);
    }
    
    private async void OnPhoneNumberValueChanged(object sender, MaskedEntryValueChangedEventArgs e)
    {
        var maskedEntry = sender as SfMaskedEntry; 
        PhoneNumberTextInputLayout.HasError = !e.IsMaskCompleted; 
        PhoneNumberTextInputLayout.ErrorText = !e.IsMaskCompleted ? AppResources.PhoneNumberErrorText : string.Empty;    
        
        vm.IsValidPhoneNumber = e.IsMaskCompleted;
        
        UpdateSignUpLayout(sender, null);
    }
    
    private void OnServiceStartDateSelected(object? sender, DateChangedEventArgs e)
    {
        var selectedDate = vm.ServiceStartDate = e.NewDate;
        ServiceStartDateTextInputLayout.HasError = selectedDate > DateTime.Today.AddDays(31);
        vm.IsValidServiceStartDate = !ServiceStartDateTextInputLayout.HasError;
        
        UpdateSignUpLayout(sender, null);
    }
    
    /// <summary>
    /// Update Sign up button layout
    /// </summary>
    private void UpdateSignUpLayout(object sender, TextChangedEventArgs e)
    {
        var hasFilledData =  !string.IsNullOrWhiteSpace(vm.FirstName) &&
                             !string.IsNullOrWhiteSpace(vm.LastName) &&
                             !string.IsNullOrWhiteSpace(vm.Username) &&
                             !string.IsNullOrWhiteSpace(vm.Password) &&
                             !string.IsNullOrWhiteSpace(vm.PhoneNumber);

        var hasValidData = vm.IsValidFirstName &&
                           vm.IsValidLastName &&
                           vm.IsValidPassword &&
                           vm.IsValidPhoneNumber &&
                           vm.IsValidServiceStartDate;

        SignUpButton.IsEnabled = hasFilledData && hasValidData;
    }

    /// <summary>
    /// On Sign Up Tapped
    /// </summary>
    private async void OnSignUpClikced(object sender, EventArgs e)
    {
        var intialTemplate = (DataTemplate)Resources["InitialTemplate"];
        var loadingTemplate = (DataTemplate)Resources["LoadingTemplate"];
        if(SignUpButton.Content == intialTemplate)
        {
            SignUpButton.Content = loadingTemplate;
        }

        await vm.SaveUserData();
        await Task.Delay(1000); // Dummy delay - to review the loading on button for a second
        SignUpButton.Content = intialTemplate; // set to Default
        await AppShell.Current.GoToAsync(nameof(SignupSuccessPage), true);
    }
    
    /// <summary>
    /// On back button tapped
    /// </summary>
    private async void OnBackButtonTapped(object sender, TappedEventArgs e)
    {
        await AppShell.Current.GoToAsync("..", true);
    }
}