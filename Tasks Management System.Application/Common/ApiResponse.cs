using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks_Management_System.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}