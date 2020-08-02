using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CarSales.Demo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        /// <summary>
        /// Retrieve all the Vehicle types.
        /// </summary>
        [HttpGet("types")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<string>> GetVehicleTypes()
        {
            IEnumerable<string> vehicleTypes;

            try
            {
                vehicleTypes = _vehicleService.GetVehicleTypes();

                if (vehicleTypes == null) return NotFound();
            }
            catch (AggregateException)
            {
                return BadRequest();//catch/throw/log
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
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<VehicleDetail>>> GetVehicleProperties(string type)

        {
            if (string.IsNullOrEmpty(type)) return BadRequest(ModelState);

            IEnumerable<VehicleDetail> vehicleProperties;
            try
            {
                vehicleProperties = await _vehicleService.GetVehicleProperties(type);
                if (vehicleProperties == null) return NotFound();
            }
            catch (AggregateException)
            {
                return BadRequest();//catch/throw/log
            }

            return Ok(vehicleProperties);

        }
        /// <summary>
        /// Retrieves all the vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(List<Vehicle>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles;
            try
            {
                vehicles = await _vehicleService.GetAllVehicles();
                if (vehicles == null) return NotFound();
            }
            catch (AggregateException)
            {
                return BadRequest();//catch/throw/log
            }

            return Ok(vehicles);

        }
        /// <summary>
        /// Add a vehicle with detail
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> AddVehicle([FromBody]JObject vehicle)
        {
            if (vehicle == null || !ModelState.IsValid) return BadRequest(ModelState);

            string vehicleAddmessage;

            try
            {
                vehicleAddmessage = await _vehicleService.AddVehicle(vehicle);
            }
            catch (AggregateException)
            {
                return BadRequest();//catch/throw/log
            }

            return Ok(vehicleAddmessage);
        }
    }
}