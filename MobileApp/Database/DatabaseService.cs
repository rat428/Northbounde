using System.IO;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.Dto;
using SQLite;


namespace Northboundei.Mobile.Database
{

    public static class DatabaseService
    {
        private static SQLiteAsyncConnection database;

        public static async Task<SQLiteAsyncConnection> GetDatabaseConnection()
        {
            // in debug mode, we want to recreate the database each time so we can iterate on the schema

            if (database == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "north.db");
                database = new SQLiteAsyncConnection(databasePath);


                await database.CreateTablesAsync(CreateFlags.None,
                    typeof(UserEntity),
                    typeof(SyncRecord)
                );

                var notesTbl = await database.CreateTableAsync<SessionNoteData>();

                var serviceAuthTbl = await database.CreateTableAsync<ServiceAuthData>();

            }
            return database;
        }

        // Clear Sync Tables
        static public async Task ClearSyncTables()
        {
            var db = await GetDatabaseConnection();
            await db.DeleteAllAsync<ServiceAuthData>();
            await db.DeleteAllAsync<SessionNoteData>();
        }

        static public async Task AddAllAsync<T>(IEnumerable<T> entities) where T : class
        {
            var db = await GetDatabaseConnection();
            await db.InsertAllAsync(entities);
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

        public static async Task ClearAllDataAsync<T>() where T : class, new()
        {
            var db = await GetDatabaseConnection();
            await db.DeleteAllAsync<T>();
        }

        public static async Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class, new()
        {
            var db = await GetDatabaseConnection();
            return await db.Table<T>().ToListAsync();
        }
    }
}
