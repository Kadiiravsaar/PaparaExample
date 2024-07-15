using FluentValidation;
using RestfulApiExample.Core.DTOs.Book;

namespace RestfulApiExample.Service.Validations.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Kitap adı boş olamaz.");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Tür ID boş olamaz.");
            RuleFor(x => x.PageCount).GreaterThan(0).WithMessage("Sayfa sayısı sıfırdan büyük olmalıdır.");
            RuleFor(x => x.PublishDate).LessThan(DateTime.Now).WithMessage("Yayın tarihi bugünden küçük olmalıdır.");
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Yazar ID boş olamaz.");
        }
    }

}
