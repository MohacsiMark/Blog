namespace BlogApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime postTime { get; set; }
        public DateTime dateTime { get; set; }
        public int blogId { get; set; }
    }
}
