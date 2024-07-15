using FluentValidation;
using RestfulApiExample.Core.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Validations.Book
{
    public class BookValidator : AbstractValidator<CreateBookDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Kitap adı boş olamaz.");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Tür ID boş olamaz.");
            RuleFor(x => x.PageCount).GreaterThan(0).WithMessage("Sayfa sayısı sıfırdan büyük olmalıdır.");
            RuleFor(x => x.PublishDate).LessThan(DateTime.Now).WithMessage("Yayın tarihi bugünden küçük olmalıdır.");
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Yazar ID boş olamaz.");
        }
    }


}
