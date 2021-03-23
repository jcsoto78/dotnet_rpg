using dotnet_rpg.Dtos.Codily;
using dotnet_rpg.Dtos.Fight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[Controller]")] //routes by controller name matching
    public class CodilyController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> MinNotInArray(MinNotInArrayRequestDto request)
        {
            var myHash = new HashSet<int>(request.arrayOfInts);

            const int maxInteger = 100000;

            var myTask = new Task<int>(() => {

                int response = 0; //invalid response

                for (int i = 1; i < maxInteger; i++)
                {
                    if (!myHash.Contains(i))
                    {
                        response = i;
                        break;
                    }
                }

                return response;
            });

            myTask.Start();

            return Ok(await myTask);
        }
    }
}
