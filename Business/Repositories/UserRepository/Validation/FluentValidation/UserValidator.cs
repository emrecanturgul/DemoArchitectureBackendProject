using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation; 

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("kullanıcı adı boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("email boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("geçerli bir mail adresi giriniz");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("resim boş olamaz");
           


        }
    }
}
    