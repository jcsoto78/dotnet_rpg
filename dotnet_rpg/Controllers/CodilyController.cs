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

        [HttpGet("MinJumps/{x}/{y}/{d}")] //adds /min to controller routing
        public async Task<IActionResult> MinJumps(int x, int y, int d)
        {
            var test = 0;

            var myTask = new Task<int>(() => {

                int cont = 0;

                while (x <= y)
                {
                    cont++;
                    x += d;
                }
                
                return cont;
            });

            myTask.Start();

            return Ok(await myTask);
        }

        [HttpPost("Rotate")] //adds /min to controller routing
        public async Task<IActionResult> RotateArray(RotateArrayRequestDto request)
        {

            var myTask = new Task<int[]>(() => {

                var rotatedArray = new int[request.arrayOfInts.Length];

                for (int i = 0; i < request.arrayOfInts.Length; i++)
                {
                    var rotatedIndex = (i + request.k) % (request.arrayOfInts.Length);
                    rotatedArray[rotatedIndex] = request.arrayOfInts[i];
                }

                return rotatedArray;
            });

            myTask.Start();

            return Ok(await myTask);
        }

        [HttpPost("Min")] //adds /min to controller routing
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
