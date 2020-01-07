using SQLite;

namespace ProductsScanner.Services.DataService.DbModels
{
    [Table("history")]
    public class HistoryModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("content"), MaxLength(255)]
        public string Content { get; set; }
        
        [Column("creationDateTicks")]
        public long CreationDateTicks { get; set; }
    }
}