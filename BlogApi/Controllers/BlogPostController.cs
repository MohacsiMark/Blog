using BlogApi.Models;
using BlogApi.Models.dtos;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly string connectionString =
            "server=localhost;uid=root;password=;database=blog;";

        [HttpGet]
        public ActionResult<IEnumerable<BlogPost>> GetBlogPosts()
        {
            var blogPosts = new List<BlogPost>();

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"SELECT id, title, content, postTime, updateTime, blogId
                                 FROM blogpost
                                 ORDER BY id";

            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                blogPosts.Add(MapBlogPost(reader));
            }

            return Ok(blogPosts);
        }


        [HttpGet("{id:int}")]
        public ActionResult<BlogPost> GetBlogPost(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"SELECT id, title, content, postTime, updateTime, blogId
                                 FROM blogpost
                                 WHERE id = @id";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return NotFound(new { message = "A bejegyzés nem található." });

            return Ok(MapBlogPost(reader));
        }


        [HttpPost]
        public ActionResult CreateBlogPost([FromBody] AddBlogPostDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length > 40)
                return BadRequest(new { message = "A Title kötelező és legfeljebb 40 karakter lehet." });

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"INSERT INTO blogpost (Title, Content, postTime, updateTime, blogId)
                                 VALUES (@title, @content, @postTime, @updateTime, @blogId)";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@title", dto.Title);
            command.Parameters.AddWithValue("@content", dto.Content);
            command.Parameters.AddWithValue("@postTime", DateTime.Now);
            command.Parameters.AddWithValue("@updateTime", DateTime.Now);
            command.Parameters.AddWithValue("@blogId", dto.BlogId);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex) when (ex.Number == 1452)
            {
                return BadRequest(new { message = "A megadott blogger nem létezik." });
            }

            var id = (int)command.LastInsertedId;
            return CreatedAtAction(nameof(GetBlogPost), new { id }, new
            {
                message = "Sikeres felvétel.",
                id
            });
        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateBlogPost(int id, [FromBody] UpdateBlogPostDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length > 40)
                return BadRequest(new { message = "A Title kötelező és legfeljebb 40 karakter lehet." });

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"UPDATE blogpost
                                 SET Title = @title,
                                     Content = @content,
                                     updateTime = @updateTime
                                 WHERE id = @id";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@title", dto.Title);
            command.Parameters.AddWithValue("@content", dto.Content);
            command.Parameters.AddWithValue("@updateTime", DateTime.Now);
            command.Parameters.AddWithValue("@id", id);

            var affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 0)
                return NotFound(new { message = "A bejegyzés nem található." });

            return Ok(new { message = "Sikeres frissítés." });
        }


        [HttpDelete("{id:int}")]
        public ActionResult DeleteBlogPost(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = "DELETE FROM blogpost WHERE id = @id";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            var affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 0)
                return NotFound(new { message = "A bejegyzés nem található." });

            return Ok(new { message = "Sikeres törlés." });
        }


        [HttpGet("blogger/{bloggerId:int}")]
        public ActionResult GetBloggerInformation(int bloggerId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"SELECT Name, Email
                                 FROM blogger
                                 WHERE id = @bloggerId";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@bloggerId", bloggerId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return NotFound(new { message = "A blogger nem található." });

            return Ok(new
            {
                Name = reader.GetString("Name"),
                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                    ? null
                    : reader.GetString("Email")
            });
        }

        [HttpGet("blogger/{bloggerId:int}/posts")]
        public ActionResult GetBloggerPosts(int bloggerId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"SELECT b.Name, p.Title, p.Content
                                 FROM blogger b
                                 INNER JOIN blogpost p ON p.blogId = b.id
                                 WHERE b.id = @bloggerId
                                 ORDER BY p.postTime DESC";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@bloggerId", bloggerId);

            using var reader = command.ExecuteReader();
            var posts = new List<object>();

            while (reader.Read())
            {
                posts.Add(new
                {
                    Name = reader.GetString("Name"),
                    Title = reader.GetString("Title"),
                    Content = reader.GetString("Content")
                });
            }

            if (posts.Count == 0)
                return NotFound(new { message = "A blogger nem található, vagy nincs bejegyzése." });

            return Ok(posts);
        }


        [HttpGet("count")]
        public ActionResult GetPostCount()
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = "SELECT COUNT(*) FROM blogpost";

            using var command = new MySqlCommand(sql, connection);
            var count = Convert.ToInt64(command.ExecuteScalar());

            return Ok(new { count });
        }


        [HttpGet("blogger/{bloggerId:int}/count")]
        public ActionResult GetBloggerPostCount(int bloggerId)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            const string sql = @"SELECT b.Name, COUNT(p.id) AS PostCount
                                 FROM blogger b
                                 LEFT JOIN blogpost p ON p.blogId = b.id
                                 WHERE b.id = @bloggerId
                                 GROUP BY b.id, b.Name";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@bloggerId", bloggerId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return NotFound(new { message = "A blogger nem található." });

            return Ok(new
            {
                Name = reader.GetString("Name"),
                PostCount = reader.GetInt64("PostCount")
            });
        }

        private static BlogPost MapBlogPost(MySqlDataReader reader)
        {
            return new BlogPost
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                Content = reader.GetString("content"),
                PostTime = reader.GetDateTime("postTime"),
                UpdateTime = reader.GetDateTime("updateTime"),
                BlogId = reader.GetInt32("blogId")
            };
        }
    }
}
