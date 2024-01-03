using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException() { }

        public UserDoesNotExistException(string message) 
            : base(message) { }
        
        public UserDoesNotExistException(string message, Exception inner) 
            : base(message, inner) { }
    }
}