namespace InvestCloud.Pages;

public partial class SignupSuccessPage : ContentPage
{
    public SignupSuccessPage()
    {
        InitializeComponent();
    }

    private async void OnClickedSignInNow(object? sender, EventArgs e)
    {
        await GoBackToMainPage();
    }

    protected override bool OnBackButtonPressed()
    {
        _ = GoBackToMainPage();
        return base.OnBackButtonPressed();
    }
    
    private async Task GoBackToMainPage()
    {
        await AppShell.Current.GoToAsync("../..", false);
    }
}