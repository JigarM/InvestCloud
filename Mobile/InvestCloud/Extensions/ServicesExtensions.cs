using InvestCloud.Interfaces;
using InvestCloud.Services;

namespace InvestCloud.Extensions;

public static class ServicesExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<ISqliteService, SqliteService>();
        return builder;
    }
}