using MauiApp3.ViewModel;
using SQLite;


namespace MauiApp3
{
    public class TaskDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TaskDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

        }

        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskItem task)
        {
            return _database.InsertAsync(task);
        }

        public async Task<TaskItem> GetTaskByTitleAsync(string title)
        {
            return await _database.Table<TaskItem>().Where(t => t.Title == title).FirstOrDefaultAsync();
        }


        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }


    }
}