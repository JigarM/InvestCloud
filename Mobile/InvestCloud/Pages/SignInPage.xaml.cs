using InvestCloud.Resources.Strings;
using InvestCloud.ViewModels;

namespace InvestCloud.Pages;

public partial class SignInPage : ContentPage
{
    private SignInViewModel vm;
    
    public SignInPage(SignInViewModel signInViewModel)
    {
        InitializeComponent();
        BindingContext = vm = signInViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UsernameEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
    }

    /// <summary>
    /// On Sign In Tapped
    /// </summary>
    private async void OnSignInClikced(object? sender, EventArgs e)
    {
        var intialTemplate = (DataTemplate)Resources["InitialTemplate"];
        var loadingTemplate = (DataTemplate)Resources["LoadingTemplate"];
        if(SignInButton.Content == intialTemplate)
        {
            SignInButton.Content = loadingTemplate;
        }

        await Task.Delay(1000); 
        SignInButton.Content = intialTemplate; // set to Default
        var isValidUser = await vm.AuthenticateUser();
        await DisplayAlert(vm.PopupTitle, vm.PopupMessage, AppResources.OK);
    }

    /// <summary>
    /// Update SignIn button layout
    /// </summary>
    private void UpdateSignInLayout(object? sender, TextChangedEventArgs e)
    {
        SignInButton.IsEnabled = !string.IsNullOrWhiteSpace(vm.Username) && !string.IsNullOrWhiteSpace(vm.Password);
    }

    /// <summary>
    /// On Register Tapped
    /// </summary>
    private async void OnRegisterTapped(object? sender, TappedEventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(SignUpPage));
    }
    
    /// <summary>
    /// On back button tapped
    /// </summary>
    private async void OnBackButtonTapped(object sender, TappedEventArgs e)
    {
        await AppShell.Current.GoToAsync("..", true);
    }

    /// <summary>
    /// On Forgot Password tapped
    /// </summary>
    private async void OnForgotPasswordTapped(object? sender, TappedEventArgs e)
    {
        await DisplayAlert(AppResources.Alert, AppResources.ForgotPasswordTappedMessage, AppResources.OK);
    }
}