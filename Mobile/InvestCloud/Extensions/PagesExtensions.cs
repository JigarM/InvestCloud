using InvestCloud.Pages;

namespace InvestCloud.Extensions;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        // pages that are navigated to
        builder.Services.AddTransient<GetStartedPage>();
        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<SignUpPage>();
        builder.Services.AddTransient<SignupSuccessPage>();
        return builder;
    }
}