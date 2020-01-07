namespace ProductsScanner.Services.DataService
{
    using ProductsScanner.Enums.Services;
    using ProductsScanner.Services.DataService.DbModels;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class DataService : IDataService
    {
        private const string database = "scannerdb.db3";
        private readonly SQLiteConnection dbConnection;
        private readonly Environment.SpecialFolder specialFolder = Environment.SpecialFolder.Personal;

        public DataService()
        {
            var folder = Environment.GetFolderPath(specialFolder);
            var path = Path.Combine(folder, database);

            dbConnection = new SQLiteConnection(path);            
        }
        
        public List<HistoryModel> GetAllHistory()
        {
            var creationResult = dbConnection.CreateTable<HistoryModel>();
            var result = dbConnection.Table<HistoryModel>().ToList();
            return result;
        }

        public async Task<List<HistoryModel>> GetAllHistoryAsync()
        {
            var folder = Environment.GetFolderPath(specialFolder); 
            var path = Path.Combine(folder, database);

            var asyncConnection = new SQLiteAsyncConnection(path);
            
            var creationResult = await asyncConnection.CreateTableAsync<HistoryModel>();
            var result = await asyncConnection.Table<HistoryModel>().ToListAsync();
            return result;
        }
        
        public void RemoveHistory(string content)
        {
            var item = dbConnection.Table<HistoryModel>()
                .FirstOrDefault(model => model.Content == content);

            if (item != null)
            {
                dbConnection.Delete(item);
            }
        }

        public void SaveHistory(string content)
        {
            var newItem = new HistoryModel
            {
                Id = 0,
                Content = content,
                CreationDateTicks = DateTime.Now.Ticks
            };

            dbConnection.Insert(newItem);
        }

        public void InsertToDb<T>(T model)
        {
            dbConnection.Insert(model);
        }

        #region SettingsDataRegion

        public SettingModel GetSettingFromDb(SettingType settingType)
        {
            dbConnection.CreateTable<SettingModel>();

            return dbConnection.Table<SettingModel>()
                        .FirstOrDefault(x => x.SettingId == (int)settingType);
        }

        public void UpdateSettingToDb<TValue>(SettingType settingType, TValue value)
        {
            try
            {
                var setting = 
                    dbConnection.Table<SettingModel>()
                    .FirstOrDefault(x => x.SettingId == (int)settingType);

                if (setting == null)
                {
                    return;
                }

                setting.SettingValue = value.ToString();
                setting.LastModifiedDateTicks = DateTime.Now.Ticks;

                dbConnection.Update(setting);
            }
            catch
            {
            }
        }

        #endregion SettingsDataRegion
    }
}