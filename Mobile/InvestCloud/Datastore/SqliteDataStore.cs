using InvestCloud.Models;
using InvestCloud.Utils;
using SQLite;

namespace InvestCloud.Datastore;

public sealed class SqliteDataStore
{
    private readonly SQLiteAsyncConnection database;
    private static Lazy<SqliteDataStore> lazy = null;

    private SqliteDataStore(string path)
    {
        database = new SQLiteAsyncConnection(path);
    }

    public SQLiteAsyncConnection Database => database;

    public static SqliteDataStore Instance
    {
        get
        {
            if (lazy is null)
            {
                throw new Exception();
            }
            return lazy.Value;
        }
    }

    private static void CreateSharedDataStore(string path)
    {
        lazy ??= new Lazy<SqliteDataStore>(() => new SqliteDataStore(path));
    }

    public static async Task InitializeDataStoreAsync()
    {
        var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppConstants.DatabaseName);
        System.Diagnostics.Debug.WriteLine("DB PAth : " + databasePath);
        CreateSharedDataStore(databasePath);
        await CreateAllTablesAsync();
    }

    private static async Task CreateAllTablesAsync()
    {
        await SqliteDataStore.Instance.Database.CreateTableAsync<User>();
    }
}