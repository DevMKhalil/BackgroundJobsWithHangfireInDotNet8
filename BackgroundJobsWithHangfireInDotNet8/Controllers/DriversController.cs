using BackgroundJobsWithHangfireInDotNet8.Models;
using BackgroundJobsWithHangfireInDotNet8.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobsWithHangfireInDotNet8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        public List<Driver> Drivers { get; set; } = new();
        private readonly ILogger<DriversController> _logger;

        public DriversController(ILogger<DriversController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult AddDriver(Driver driver)
        {
            if (ModelState.IsValid)
            {
                Drivers.Add(driver);

                var jopId = BackgroundJob.Enqueue<IServiceManagement>(x => x.SendEmail());

                return CreatedAtAction("GetDriver",new { driver.Id },driver);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetDriver(Guid id) 
        {
            var driver = Drivers.FirstOrDefault(x => x.Id == id);

            if (driver is null)
                return NotFound();

            return Ok(driver);
        }

        [HttpDelete]
        public IActionResult DeleteDriver(Guid id) 
        {
            var driver = Drivers.FirstOrDefault(x => x.Id == id);

            if (driver is null)
                return NotFound();
            driver.Status = 0;
            return NoContent();
        }
    }
}
