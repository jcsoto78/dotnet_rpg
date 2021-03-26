using dotnet_rpg.Dtos.Codily;
using dotnet_rpg.Dtos.Fight;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [ApiController] // declares this class instaces take Http requests
    [Route("[Controller]")] //routes by controller name matching
    public class CodilyController : ControllerBase
    {

        //check this Solution/Images/1
        [HttpGet("MaxByInserting5/{N}")] //adds /min to controller routing
        public IActionResult MaxByInserting5(int N)
        {
            int k = 5;

            string inputString = "$" + N.ToString().Trim('-');

            int n = inputString.Length;

            int max = -8000;

            var sb = new StringBuilder(inputString);

            for (int i = 0; i < n + 1; i++)
            {
                sb.Replace('$', Convert.ToChar(k.ToString()));

                int parsedValue = int.Parse(sb.ToString());
                int possibleMax = N >= 0 ? parsedValue : parsedValue * -1;

                max = possibleMax > max ? possibleMax : max;

                if (i < n - 1)
                {
                    var rightValue = sb[i + 1]; //save right value
                    sb[i + 1] = '$';
                    sb[i] = rightValue;
                }
            }

            return Ok(max);
        }

        //https://app.codility.com/programmers/lessons/13-fibonacci_numbers/ladder/
        // sol https://github.com/Mickey0521/Codility/blob/master/Ladder.java

        [HttpPost("Ladder")]
        public IActionResult GetLadder(GetLadderRequestDto request)
        {
            int L = request.arrayA.Length;

            // determine the "max" for Fibonacci
            var maxFibValue = request.arrayA.Max();

            int[] fibonacci = new int[maxFibValue + 1]; // plus one for "0"

            // initial setting of Fibonacci (importnat)
            fibonacci[0] = 1;
            fibonacci[1] = 1;

            //fibonacci generator without overflow
            for (int i = 2; i < maxFibValue; i++)
            {
                fibonacci[i] = (fibonacci[i - 1] + fibonacci[i - 2]) % (1 << 30); // 1<<30 ++ 2^30 without overflow
            }

            // to find "results"
            int[] results = new int[L];

            for (int i = 0; i < L; i++)
            {
                results[i] = fibonacci[request.arrayA[i]] % (1 << request.arrayB[i]); // where, "1 << B[i]" means 2^B[i]
            }

            return Ok(results);
        }

        ////https://app.codility.com/programmers/lessons/15-caterpillar_method/count_triangles/

        [HttpPost("Triplets")]
        public async Task<IActionResult> FindTriplets(GetTripletsRequestDto request)
        {
            if (request.array.Length < 3)
            {
                return BadRequest($"Input array Length must be at least 3, provided Length : {request.array.Length}");
            }

            var myTask = new Task<int>(() => {

                int n = request.array.Length;

                Array.Sort(request.array); 

                int[] sortedArray = request.array;

                int pI =0, qI= pI + 1, rI = qI + 1; // PI is back index of sliding window ''caterpillar, RI is head index
                int tripletsCount = 0;
                                
                while (pI < n-2) //stops when the window indexes are invalid by +1
                {
                    ////since the array is sorted, ri and qi must move forward looking for further cases, else move the sliding window
                    if (sortedArray[pI] + sortedArray[qI] > sortedArray[rI])
                    {
                        tripletsCount++;
                    }

                    if (rI < n - 1)
                    {
                        //move rI
                        rI++;
                    }
                    else if (qI < n - 2)
                    {
                        //move qI
                        qI++;
                        rI = qI + 1;
                    }
                    else //rI is at [n-1] and qI is at [n-2]
                    {
                        //move position indexes window
                        pI++; qI = pI + 1; rI = qI + 1;
                    }
                }

                return tripletsCount;
            });

            myTask.Start();

            return Ok(await myTask);
        }

        [HttpGet("MinJumps/{x}/{y}/{d}")]
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

        [HttpPost("Rotate")] 
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

        [HttpPost("Min")] 
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
