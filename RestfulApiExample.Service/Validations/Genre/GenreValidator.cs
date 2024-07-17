using FluentValidation;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Validations.Genre
{

	public class GenreValidator : AbstractValidator<GenreDto>
	{
		public GenreValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Tür alanı boş olamaz.");
		
			
		}
	}
}
