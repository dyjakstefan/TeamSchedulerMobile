using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TSM.Models;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.Services
{
    public static class LocalDatabase
    {
        public static SQLiteAsyncConnection Database { get; private set; }
        private static bool initialized;

        public static async Task Initialize()
        {
            if (Database == null)
            {
                Database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("xamarin_db.db3"));
                await Database.CreateTableAsync<User>();
                await Database.CreateTableAsync<Jwt>();
            }
        }

        public static async Task<List<T>> GetTable<T>() where T : new()
        {
            if (Database == null)
                await Initialize();
            return await Database.Table<T>().ToListAsync();
        }

        public static async Task<T> GetSingle<T>(int id) where T : Entity, new()
        {
            if (Database == null)
                await Initialize();
            return await Database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<int> InsertSingle<T>(T item) where T : Entity, new()
        {
            if (Database == null)
                await Initialize();
            return await Database.InsertAsync(item);
        }

        public static async Task<int> UpdateSingle<T>(T item) where T : Entity, new()
        {
            if (Database == null)
                await Initialize();
            return await Database.UpdateAsync(item);
        }
    }
}
