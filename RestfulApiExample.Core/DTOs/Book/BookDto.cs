using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Core.DTOs.Book
{
	public class BookDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int GenreId { get; set; }
		public string GenreName { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishDate { get; set; }
		public bool IsActive { get; set; }
		public int AuthorId { get; set; }
		public string AuthorName { get; set; }
		public string AuthorLastName { get; set; }
	}

}
