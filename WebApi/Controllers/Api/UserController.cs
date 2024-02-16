using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string ConnectionString = "Data Source=192.168.168.248;Initial Catalog=Test;User ID=ansibledb;Password=123123";

        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var _User = new { FirstName = "Mohammad", LastName = "Rahimi" };

            return Ok(new { User = _User, Status = "200" });
        }

        [Route("GetUserSql")]
        [HttpGet]
        public async Task<IActionResult> GetUserSql()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cm = new SqlCommand("select * from Customers", connection);
            connection.Open();
            SqlDataReader sdr = await cm.ExecuteReaderAsync();
            var _Users = new List<string>();

            while (sdr.Read())
            {
                _Users.Add(sdr["FirstName"] + " " + sdr["LastName"]);
            }

            return Ok(new { Users = _Users, Status = "200" });
        }
    }
}
