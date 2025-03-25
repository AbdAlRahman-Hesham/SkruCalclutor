using Contexts.Models;
using SQLite;

namespace Contexts.Repos;

public class SavePlayerRepo : ISavePlayerRepo
{
    private SQLiteAsyncConnection _database;
    private readonly string _dbPath;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public SavePlayerRepo(string dbPath)
    {
        _dbPath = dbPath;
    }

    private async Task InitAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_dbPath);
            await _database.CreateTableAsync<SavedPlayer>();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> AddPlayer(SavedPlayer player)
    {
        await InitAsync();

        try
        {
            var result = await _database.InsertAsync(player);
            return result > 0;
        }
        catch (Exception ex)
        {
            // Log error here
            Console.WriteLine($"Error adding player: {ex.Message}");
            return false;
        }
    }

    public async Task<List<SavedPlayer>> GetAllPlayersAsync()
    {
        await InitAsync();
        return await _database.Table<SavedPlayer>().ToListAsync();
    }

    public async Task<SavedPlayer> GetPlayerByIdAsync(int id)
    {
        await InitAsync();
        return await _database.Table<SavedPlayer>()
                             .Where(p => p.Id == id)
                             .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdatePlayerAsync(SavedPlayer player)
    {
        await InitAsync();
        var result = await _database.UpdateAsync(player);
        return result > 0;
    }

    public async Task<bool> DeletePlayerAsync(int id)
    {
        await InitAsync();
        var player = await GetPlayerByIdAsync(id);
        if (player == null) return false;

        var result = await _database.DeleteAsync(player);
        return result > 0;
    }
}
public interface ISavePlayerRepo
{
    Task<bool> AddPlayer(SavedPlayer player);
    Task<List<SavedPlayer>> GetAllPlayersAsync();
    Task<SavedPlayer> GetPlayerByIdAsync(int id);
    Task<bool> UpdatePlayerAsync(SavedPlayer player);
    Task<bool> DeletePlayerAsync(int id);
}
