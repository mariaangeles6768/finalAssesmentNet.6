using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using sna.Models;
using System;
using System.Data;
using System.Text.Json.Nodes;

namespace sna.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=jjjjjj;password=123;database=snaDatabase");

        [HttpGet(Name = "GetOwners")]
        public LinkedList<Owner> GetOwners()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM owners", connection);

            da.SelectCommand.CommandType = CommandType.Text;

            DataTable dt = new DataTable();

            da.Fill(dt);

            LinkedList<Owner> owners_list = new LinkedList<Owner>();
            
            foreach (DataRow r in dt.Select()){
                owners_list.AddLast(new Owner
                {
                    Id = Int16.Parse(r["id"].ToString()),
                    FirstName = r["first_name"].ToString(),
                    LastName = r["last_name"].ToString(),
                    DriverLicense = r["driver_license"].ToString()
                });
            }
            return owners_list;
        }

        [HttpPost(Name = "PostOwner")]
        public void PostOwner(Owner owner)
        {
            string sql = @"insert into owners
             (first_name,last_name,driver_license) 
             values (@first_name,@last_name,@driver_license)";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.Add("@first_name", MySqlDbType.VarChar).Value = owner.FirstName;
            cmd.Parameters.Add("@last_name", MySqlDbType.VarChar).Value = owner.LastName;
            cmd.Parameters.Add("@driver_license", MySqlDbType.VarChar).Value = owner.DriverLicense;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
