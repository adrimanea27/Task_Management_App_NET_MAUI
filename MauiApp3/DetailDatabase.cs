//using MauiApp2.ViewModel;
//using SQLite;
//using MauiApp2.Model;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace MauiApp2
//{
//    public class DetailDatabase
//    {
//        private readonly SQLiteAsyncConnection _databaseDetail;

//        public DetailDatabase(string dbPathDetail)
//        {
//            _databaseDetail = new SQLiteAsyncConnection(dbPathDetail);
//            var result = _databaseDetail.CreateTableAsync<DetailItem>().Result; // Verifică rezultatul creării tabelului
//            System.Diagnostics.Debug.WriteLine($"Table creation result: {result}");
//        }

//        // Metodă pentru a obține toate detaliile
//        public Task<List<DetailItem>> GetDetailsAsync(int taskId)
//        {
//            return _databaseDetail.Table<DetailItem>()
//                .Where(d => d.TaskId == taskId)
//                .ToListAsync();
//        }

//        // Metodă pentru a salva un detaliu
//        public async Task<int> SaveDetailAsync(DetailItem detail)
//        {
//            System.Diagnostics.Debug.WriteLine($"Saving detail: {detail.DetailText}, TaskId: {detail.TaskId}");
//            try
//            {
//                return await _databaseDetail.InsertAsync(detail);
//            }
//            catch (Exception ex)
//            {
//                // Loghează eroarea
//                System.Diagnostics.Debug.WriteLine($"Error saving detail: {ex.Message}");
//                return 0; // Returnează 0 în cazul unei erori
//            }
//        }

//        // Metodă pentru a șterge un detaliu
//        public Task<int> DeleteDetailAsync(DetailItem detail)
//        {
//            return _databaseDetail.DeleteAsync(detail);
//        }
//    }
//}

using SQLite;
using MauiApp3.ViewModel;



namespace MauiApp3
{
    public class DetailDatabase
    {
        private readonly SQLiteAsyncConnection _databaseDetail;

        public DetailDatabase()
        {
            
            var dbPathDetail = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DetailDatabase.db3");
            _databaseDetail = new SQLiteAsyncConnection(dbPathDetail);

            
            _databaseDetail.CreateTableAsync<DetailItem>().Wait();
            //System.Diagnostics.Debug.WriteLine("Detail database and table initialized.");
        }

       
        public Task<List<DetailItem>> GetDetailsAsync()
        {
            return _databaseDetail.Table<DetailItem>().ToListAsync();
        }

        
        public async Task<int> SaveDetailAsync(DetailItem detail)
        {
            System.Diagnostics.Debug.WriteLine($"Saving detail: {detail.DetailText}");
            try
            {
                if (detail.Id != 0)
                {
                    
                    return await _databaseDetail.UpdateAsync(detail);
                }
                else
                {
                    
                    return await _databaseDetail.InsertAsync(detail);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving detail: {ex.Message}");
                return 0; 
            }
        }





        // Metodă pentru a șterge un detaliu
        public Task<int> DeleteDetailAsync(DetailItem detail)
        {
            return _databaseDetail.DeleteAsync(detail);
        }

        public async Task<List<DetailItem>> GetDetailsByTaskIdAsync(int taskId)
        {
            return await _databaseDetail.Table<DetailItem>()
                .Where(detail => detail.TaskItemId == taskId) 
                .ToListAsync(); 
        }

    }
}
