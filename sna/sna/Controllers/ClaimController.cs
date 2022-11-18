using Microsoft.AspNetCore.Mvc;
using sna.Models;

namespace sna.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : Controller
    {
        [HttpGet(Name = "GetClaim")]
        public IEnumerable<Claim> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Claim
            {
                Id = index,
                Description = "Claim " + index,
                Status = "In Progress",
                Date = DateTime.Now,
                VehicleId = 1
            })
            .ToArray();
        }
    }
}
