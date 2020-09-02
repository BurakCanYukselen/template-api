using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace API.Base.Core.Models
{
    public class ErrorModel
    {
        public List<string> ErrorMessage { get; } = new List<string>();
        public string StackTrace { get; set; }

        public void AddErrorMessage(string message)
        {
            ErrorMessage.Add(message);
        }
    }
}