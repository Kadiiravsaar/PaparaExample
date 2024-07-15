using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.Models
{
	public class Author
	{
		public int Id { get; set; }
		public int? BookId { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public bool IsActive { get; set; } = true;
		public Book Book { get; set; }

	}
}
