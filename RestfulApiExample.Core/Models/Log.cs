using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.Models
{
	public class Log
	{
		public int Id { get; set; }
		public string Method { get; set; }
		public string Path { get; set; }
		public int StatusCode { get; set; }
		public DateTime RequestTime { get; set; }
		public DateTime ResponseTime { get; set; }
	}

}
