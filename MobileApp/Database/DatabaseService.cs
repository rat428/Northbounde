using System.IO;
using Northboundei.Mobile.Database.Models;
using SQLite;


namespace Northboundei.Mobile.Database
{

    public static class DatabaseService
    {
        private static SQLiteAsyncConnection database;

        public static async Task<SQLiteAsyncConnection> GetDatabaseConnection()
        {
            if (database == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "north.db");
                database = new SQLiteAsyncConnection(databasePath);
                await database.CreateTableAsync<UserEntity>();
                await database.CreateTableAsync<SyncRecord>();
            }
            return database;
        }

        static public async Task AddDataAsync<T>(T data) where T : class
        {
            var db = await GetDatabaseConnection();
            await db.InsertAsync(data);
        }

        static public async Task UpdateDataAsync<T>(T data) where T : class
        {
            var db = await GetDatabaseConnection();
            await db.UpdateAsync(data, typeof(T));
        }

        public static async Task DeleteAsync<T>(T data) where T : class
        {
            var db = await GetDatabaseConnection();
            await db.DeleteAsync(data);
        }

        public static async Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class, new()
        {
            var db = await GetDatabaseConnection();
            return await db.Table<T>().ToListAsync();
        }
    }
}
