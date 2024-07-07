using Microsoft.AspNetCore.Mvc;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using RiverMonitoring.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiverMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Receives a list of values and saves them to the database.
        /// </summary>
        /// <param name="values">List of value view models.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpPost]
        public async Task<IActionResult> PostValues([FromBody] List<ValueViewModel> values)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newValues = new List<Value>();

            foreach (var valueDto in values)
            {
                var station = await _context.Stations.FindAsync(valueDto.StationId);
                if (station == null)
                {
                    return BadRequest($"Station with ID {valueDto.StationId} was not found.");
                }

                var value = new Value
                {
                    StationId = valueDto.StationId,
                    MeasuredValue = valueDto.MeasuredValue,
                    Timestamp = valueDto.Timestamp
                };

                newValues.Add(value);
            }

            _context.Values.AddRange(newValues);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Values have been successfully saved." });
        }
    }
}
