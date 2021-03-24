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

        ////https://app.codility.com/programmers/lessons/15-caterpillar_method/count_triangles/

        [HttpPost("Triplets")] //adds /min to controller routing
        public async Task<IActionResult> FindTriplets(GetTripletsRequestDto request)
        {
            var myTask = new Task<int>(() => {

                int n = request.array.Length;

                Array.Sort(request.array); 

                int[] sortedArray = request.array;

                int pI, qI, rI; // PI is back index of sliding window ''caterpillar, RI is head index
                int tripletsCount = 0;

                for (int i = 0; i < n - 2; i++) // stops by back index is  
                {
                    pI = i; //back
                    qI = pI + 1;
                    rI = qI + 1; //header

                    while (rI < n && sortedArray[pI] + sortedArray[qI] > sortedArray[rI])
                    {
                        tripletsCount++;

                        if (rI == n-1)
                        {
                            qI++;
                            rI = qI + 1; //header
                        }
                        else
                        {
                            rI++;
                        }
                    }
                }

                return tripletsCount;
            });

            myTask.Start();

            return Ok(await myTask);
        }

        [HttpGet("MinJumps/{x}/{y}/{d}")] //adds /min to controller routing
        public async Task<IActionResult> MinJumps(int x, int y, int d)
        {
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
