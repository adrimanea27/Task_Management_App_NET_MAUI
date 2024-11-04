//using SQLite;
//using MauiApp2.Model;


//namespace MauiApp2.ViewModel
//{
//    public class DetailItem
//    {
//        [PrimaryKey, AutoIncrement]
//        public int Id { get; set; }
//        public string DetailText { get; set; } // Asigură-te că numele este exact acesta
//    //  public int TaskId { get; set; }


//    }

//}

using SQLite;


namespace MauiApp3.ViewModel
{
    public class DetailItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
       // public string? TaskText { get; set; }
        public string? DetailText { get; set; } // Asigură-te că numele este exact acesta

        public int TaskItemId { get; set; }
    }
}
