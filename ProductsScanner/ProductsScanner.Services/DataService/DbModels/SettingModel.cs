namespace ProductsScanner.Services.DataService.DbModels
{
    using SQLite;

    [Table("settings")]
    public class SettingModel
    {
        [PrimaryKey, Column("settingId")]
        public int SettingId { get; set; }

        [Column("settingValue"), MaxLength(255)]
        public string SettingValue { get; set; }

        [Column("description"), MaxLength(255)]
        public string Description { get; set; }

        [Column("lastModifiedDateTicks")]
        public long LastModifiedDateTicks { get; set; }
    }
}