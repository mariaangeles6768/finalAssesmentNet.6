using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace sna.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        [HttpGet(Name = "GetVehicle")]
        public IEnumerable<Vehicle> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Vehicle
            {
                Id = index,
                Brand = "Porsche",
                Vin = "TYUCIA",
                Color = "Gray",
                Year = DateTime.Now.Year,
                OwnerId = 1
            })
            .ToArray();
        }
    }
}
