using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using sna.Models;
using System.Data;

namespace sna.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : Controller
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=jjjjjj;password=123;database=snaDatabase");

        [HttpGet(Name = "GetClaims")]
        public LinkedList<Claim> GetClaims()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM claims", connection);

            da.SelectCommand.CommandType = CommandType.Text;

            DataTable dt = new DataTable();

            da.Fill(dt);

            LinkedList<Claim> claims_list = new LinkedList<Claim>();

            foreach (DataRow r in dt.Select())
            {
                claims_list.AddLast(new Claim
                {
                    Id = Int16.Parse(r["id"].ToString()),
                    Description = r["description"].ToString(),
                    Status = r["status"].ToString(),
                    Date = DateTime.Parse(r["cdate"].ToString()),
                    VehicleId = Int16.Parse(r["vehicle_id"].ToString())
                });
            }
            return claims_list;
        }

        [HttpPost(Name = "PostClaim")]
        public void PostClaim(Claim claim)
        {
            string sql = @"insert into claims(description, status, cdate, vehicle_id) values 
                    (@description,@status,@cdate, @vehicle_id)";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = claim.Description;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = claim.Status;
            cmd.Parameters.Add("@cdate", MySqlDbType.VarChar).Value = claim.Date;
            cmd.Parameters.Add("@vehicle_id", MySqlDbType.VarChar).Value = claim.VehicleId;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
