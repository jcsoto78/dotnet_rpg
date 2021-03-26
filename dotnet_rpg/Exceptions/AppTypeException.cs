using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Exceptions
{
    public class AppTypeException : Exception
    {
        public AppTypeException(string message) : base(message)
        {

        }
    }
}
