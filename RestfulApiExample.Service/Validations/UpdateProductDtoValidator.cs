using FluentValidation;
using RestfulApiExample.Core.DTOs;

namespace RestfulApiExample.Services.Validations
{
	public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
	{
		public UpdateProductDtoValidator()
		{
			RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
			RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
			RuleFor(x => x.Description).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
		}


	}
}
