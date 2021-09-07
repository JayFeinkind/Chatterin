using System;
using System.Collections.Generic;
using System.Net;

namespace Chatterin.ClassLibrary
{
	public class ApiResult<T>
	{
		public ApiResultData<T> Data { get; set; }

		public Exception Exception { get; set; }

		public HttpStatusCode ResultCode { get; set; }

		public bool Success { get; set; }
	}

	public class ApiResultData<T>
    {
		public T Result { get; set; }
		public List<string> Errors { get; internal set; } = new List<string>();
	}
}
