namespace ChineseAnimeCatalog.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = "";
        public string CoverUrl { get; set; } = "";
        public string VideoUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}