using BlogApi.Models;
using BlogApi.Models.dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=blog;";
        [HttpGet]
        public object GetBloggers()
        {
            List<Blogger> bloggers = new List<Blogger>();

            var connection=new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM blogger";

            var cmd = new MySqlCommand(sql,connection);

            var data = cmd.ExecuteReader();

            while(data.Read())
            {
                var blogger = new Blogger()
                {
                    Id=data.GetInt32("id"),
                    Name=data.GetString("name"),
                    Email=data.GetString("email"),
                    Age=data.GetInt32("age"),
                    Password=data.GetString("password"),
                    RegistrationTime=data.GetDateTime("registrationtime")
                };

                bloggers.Add(blogger);  
            }

            connection.Close();
                
            return bloggers;
        }

        [HttpPost]
        public object AddNewBlogger([FromBody]AddNewBloggerDTO addNewBloggerDTO)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"INSERT INTO `blogger`(`Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES (@name,@email,@age,@password,@registrationTime)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", addNewBloggerDTO.Name);
            cmd.Parameters.AddWithValue("@email", addNewBloggerDTO.Email);
            cmd.Parameters.AddWithValue("@age", addNewBloggerDTO.Age);
            cmd.Parameters.AddWithValue("@password", addNewBloggerDTO.Password);
            cmd.Parameters.AddWithValue("@registrationTime", DateTime.Now);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new {message="Sikeres felvétel.", result=addNewBloggerDTO};
        }

        [HttpDelete]

        public object DeleteBlogger(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"DELETE From `blogger` WHERE `id`=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            connection.Close();
            return new { message = "Sikeres törlés.", result="" };
        }

        [HttpPut]

        public object UpdateBlogger([FromQuery]int id, UpdateBloggerDTO updateBloggerDTO)
        {
            var connection =new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"UPDATE `blogger` SET `Name`=@name,`Email`=@email,`Age`=@age,`Password`=@password WHERE `id`=@id;";

            var cmd=new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", updateBloggerDTO.Name);
            cmd.Parameters.AddWithValue("@email", updateBloggerDTO.Email);
            cmd.Parameters.AddWithValue("@age", updateBloggerDTO.Age);
            cmd.Parameters.AddWithValue("@password", updateBloggerDTO.Password);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            connection.Close();
            return new { message = "Sikeres frissítés.", result = "" };
        }

        [HttpGet("byId")]

        public object GetBloggerId(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"SELECT * FROM `blogger` WHERE `id`=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read() == true)
            {
                var blogger = new Blogger
                {
                    Id = datareader.GetInt32(0),
                    Name = datareader.GetString(1),
                    Email = datareader.GetString(2),
                    Age = datareader.GetInt32(3),
                    Password = datareader.GetString(4),
                    RegistrationTime = datareader.GetDateTime(5)
                };

                return new { message = "Sikeres lekérdezés.", result = blogger };
            }
            else
            {
                return new { message = "Sikertelen lekérdezés.", result = "" };
            }

            connection.Clone();

            return data;
        }
    }
}
