namespace BlogApi.Models.dtos
{
    public class AddBlogPostDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int BlogId { get; set; }
    }
}
