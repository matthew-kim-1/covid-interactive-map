using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.CustomException;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [Route("v1/statecodename")]
    public class StateCodeNameController : Controller
    {
        private readonly IStateCodeNameService _stateCodeNameService;

        public StateCodeNameController(IStateCodeNameService stateCodeNameService)
        {
            _stateCodeNameService = stateCodeNameService;
        }

        [HttpGet("GetAllStateCodeNames")]
        public async Task<IActionResult> GetAllStateCodeNamesAsync()
        {
            var stateCodeNames = await _stateCodeNameService.GetAllStateCodeNamesAsync();
            if (stateCodeNames == null)
            {
                throw new NotFoundException("StateCodeNames not found.");
            }

            return Ok(stateCodeNames);
        }

        [HttpGet("GetStateCodeNamesFromCsv")]
        public async Task<IActionResult> GetStateCodeNamesFromCsvAsync()
        {
            var stateCodeNames = await _stateCodeNameService.GetStateCodeNamesFromCsvAsync();
            if (stateCodeNames == null)
            {
                return NotFound();
            }

            return Ok(stateCodeNames);
        }
    }
}
