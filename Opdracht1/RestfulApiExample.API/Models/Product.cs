using System.ComponentModel.DataAnnotations;

namespace RestfulApiExample.API.Models
{
	public class Product 
	{

		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(500)]
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }
	}
}
