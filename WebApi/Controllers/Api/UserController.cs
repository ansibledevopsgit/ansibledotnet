using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _Configuration;
        public UserController(IConfiguration Configuration) { _Configuration = Configuration; }

        [Route("All")]
        [HttpGet]
        public IActionResult All()
        {
            return Ok(new { All = "mamad rahimi ", Status = "200" });
        }


        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var _User = new { FirstName = "Ali", LastName = "Rahimi" };

            return Ok(new { User = _User, Status = "200" });
        }

        [Route("GetUserSql")]
        [HttpGet]
        public IActionResult GetUserSql()
        {
            try
            {
                string ConnectionString = _Configuration.GetConnectionString("DbAnsible")!;

                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlCommand cm = new SqlCommand("select * from Customers", connection);
                connection.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                var _Users = new List<string>();

                while (sdr.Read())
                {
                    _Users.Add(sdr["FirstName"] + " " + sdr["LastName"]);
                }

                return Ok(new { Users = _Users, Status = "200" });
            }
            catch (Exception ex)
            {
                return Ok(new { Users = ex.ToString(), Status = "400" });
            }

        }
    }
}
