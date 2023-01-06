namespace StrengthJournal.MVC.ApiModels
{
    public class DataPage<T>
    {
        public int PerPage { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
