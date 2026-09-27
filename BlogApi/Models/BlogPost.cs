namespace BlogApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PostTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int BlogId { get; set; }
    }
}
