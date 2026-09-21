using System.Reflection.Metadata.Ecma335;

namespace BlogApi.Models.dtos
{
    public class AddNewBloggerDTO
    {
        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
    }
}
