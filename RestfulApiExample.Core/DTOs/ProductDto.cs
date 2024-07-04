using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.DTOs
{
	public class ProductDto 
	{
		// Dto kullandım çünkü => kullanıcıya dönmek istemediğim alanları burada vermeyeceğim ve kullanıcıya dto döneceğim
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }

	}	
}
