using FluentValidation;
using RestfulApiExample.Core.DTOs.Author;

namespace RestfulApiExample.Service.Validations.Author
{
	public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
	{
		public UpdateAuthorValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz.");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim boş olamaz.");
			RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden küçük olmalıdır.");
		}
	}


}
