using FluentValidation;
using RestfulApiExample.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Services.Validations
{
	public class ProductDtoValidator : AbstractValidator<ProductDto>
	{
		public ProductDtoValidator()
		{
			RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
			RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
			RuleFor(x => x.Description).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
		}


	}
}
