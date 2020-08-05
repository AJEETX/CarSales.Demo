using System.Collections.Generic;
using System.Threading.Tasks;
using CarSales.Demo.Api.Domain.Helper;
using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Examples;

namespace CarSales.Demo.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        readonly IVehicleManagerService _vehicleManagerService;
        public VehicleController(IVehicleManagerService vehicleManagerService)
        {
            _vehicleManagerService = vehicleManagerService;
        }
        /// <summary>
        /// Retrieve all the Vehicle types.
        /// </summary>
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<string>> GetVehicleTypes()
        {
            IEnumerable<string> vehicleTypes;

            try
            {
                vehicleTypes = _vehicleManagerService.GetVehicleTypes();

                if (vehicleTypes == null) return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok(vehicleTypes);

        }
        /// <summary>
        /// Retrieves all properties of the supplied vehicle type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VehicleDetail>>> GetVehicleProperties(string type)

        {
            if (string.IsNullOrEmpty(type)) return BadRequest(ModelState);

            IEnumerable<VehicleDetail> vehicleProperties;
            try
            {
                vehicleProperties = await _vehicleManagerService.GetVehicleProperties(type);
                if (vehicleProperties == null) return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok(vehicleProperties);

        }
        /// <summary>
        /// Retrieves all the vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Vehicle>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles;
            try
            {
                vehicles =  _vehicleManagerService.GetAllVehicles();
                if (vehicles == null) return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok(vehicles);

        }
        /// <summary>
        /// Add a vehicle with detail
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Vehicle))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(JObject), typeof(VehiclRequestExample))]
        public async Task<IActionResult> AddVehicle([FromBody] JObject jObject)
        {
            if (jObject == null || !ModelState.IsValid) return BadRequest(ModelState);

            Vehicle vehicle;

            try
            {
                vehicle = await _vehicleManagerService.AddVehicle(jObject);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok(vehicle);
        }
    }
}