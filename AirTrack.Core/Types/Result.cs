﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AirTrack.Core.Types
{
    
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class Result<T> : Result{
       public T Data { get; set; }
    }
}
