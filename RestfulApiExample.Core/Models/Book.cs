using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.Models
{
	public class Book : BaseEntity
	{
		public int GenreId { get; set; }
		public string Title { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishDate { get; set; }
		public Genre Genre { get; set; }
		public Author Author { get; set; }
	}
}
