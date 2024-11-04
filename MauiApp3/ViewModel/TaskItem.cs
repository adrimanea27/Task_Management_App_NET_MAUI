using SQLite;

namespace MauiApp3.ViewModel
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }

    }
}
