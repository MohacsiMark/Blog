using BlogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        public string connectionString = "server=localhost;uid=root;password=;database=blog;";

        [HttpGet]

        public object BloggerGetinformation(int id,Blogger blogger)
        {
            return new { message = "Sikeres lekérdezés.", result = "" };
        }
    }
}
