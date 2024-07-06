using Microsoft.AspNetCore.Mvc;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Api
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

        [HttpPost]
        public async Task<IActionResult> PostValues([FromBody] List<ValueDto> values)
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
                    return BadRequest($"Stanice s ID {valueDto.StationId} nebyla nalezena.");
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

            return Ok(new { message = "Hodnoty byly úspěšně uloženy." });
        }
    }

    public class ValueDto
    {
        public int StationId { get; set; }
        public double MeasuredValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
