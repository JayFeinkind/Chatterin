using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.Services
{
    public class ApiResult<TData>
    {
        public TData Result { get; set; }
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; internal set; } = new List<string>();
    }
}
