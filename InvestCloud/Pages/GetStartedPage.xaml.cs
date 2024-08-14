namespace InvestCloud.Pages;

public partial class GetStartedPage : ContentPage
{
    public GetStartedPage()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// On Sign In Tapped
    /// </summary>
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(SignInPage));
    }
    
    /// <summary>
    /// On Sign Up Tapped
    /// </summary>
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(SignUpPage));
    }
}