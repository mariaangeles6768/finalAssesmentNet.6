using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using sna.Models;
using System.ComponentModel;
using System.Data;

namespace sna.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=jjjjjj;password=123;database=snaDatabase");

        [HttpGet(Name = "GetVehicles")]
        public LinkedList<Vehicle> GetVehicles()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM vehicles", connection);

            da.SelectCommand.CommandType = CommandType.Text;

            DataTable dt = new DataTable();

            da.Fill(dt);

            LinkedList<Vehicle> vehicles_list = new LinkedList<Vehicle>();

            foreach (DataRow r in dt.Select())
            {
                vehicles_list.AddLast(new Vehicle
                {
                    Id = Int16.Parse(r["id"].ToString()),
                    Brand = r["brand"].ToString(),
                    Vin = r["vin"].ToString(),
                    Color = r["color"].ToString(),
                    Year = Int16.Parse(r["year"].ToString()),
                    OwnerId = Int16.Parse(r["owner_id"].ToString())
                });
            }
            return vehicles_list;
        }

        [HttpPost(Name = "PostVehicle")]
        public void PostVehicle(Vehicle vehicle)
        {
            string sql = @"insert into vehicles(brand, vin, color, year, owner_id) 
                values (@brand,@vin,@color, @year, @owner_id)";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.Add("@brand", MySqlDbType.VarChar).Value = vehicle.Brand;
            cmd.Parameters.Add("@vin", MySqlDbType.VarChar).Value = vehicle.Vin;
            cmd.Parameters.Add("@color", MySqlDbType.VarChar).Value = vehicle.Color;
            cmd.Parameters.Add("@year", MySqlDbType.Int16).Value = vehicle.Year;
            cmd.Parameters.Add("@owner_id", MySqlDbType.Int16).Value = vehicle.OwnerId;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
