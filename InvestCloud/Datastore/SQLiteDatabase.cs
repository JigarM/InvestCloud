using InvestCloud.Models;
using InvestCloud.Utils;
using SQLite;

namespace InvestCloud.Datastore;

public class SQLiteDatabase
{
    private readonly SQLiteAsyncConnection _database;

    private const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
    
    private string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppConstants.DatabaseName);
    
    public SQLiteDatabase()
    {
        System.Diagnostics.Debug.WriteLine("DB PAth : " + DatabasePath);
        _database = new SQLiteAsyncConnection(DatabasePath, Flags);
        _database.CreateTableAsync<User>();
    }
    
    public async Task<User> GetUserAsync(User item)
    {
        return await _database.Table<User>().Where(i => i.Id == item.Id).FirstOrDefaultAsync();
    }
    
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _database.Table<User>().Where(i => i.Username == username).FirstOrDefaultAsync();
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _database.Table<User>().ToListAsync();
    }
        
    public async Task<int> AddUserAsync(User item)
    {
        return await _database.InsertAsync(item);
    }
}