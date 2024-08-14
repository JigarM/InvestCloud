using AsyncAwaitBestPractices;
using InvestCloud.Datastore;

namespace InvestCloud;

public partial class App : Application
{
    static SQLiteDatabase database;
    
    public App()
    {
        //Register Syncfusion license
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQyNzcwMkAzMjM2MmUzMDJlMzBudnNQcEpCcWhDQkl6UzBteVg2a3hWT2wyanJDNlpNcDhPQ0piOEFOVzhJPQ==");
        
        InitializeComponent();
        MainPage = new AppShell();
        
        // For many tables
        // SqliteDataStore.InitializeDataStoreAsync().SafeFireAndForget();
    }
    

    // Create the database connection as a singleton.
    public static SQLiteDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new SQLiteDatabase();
            }
            return database;
        }
    }
}