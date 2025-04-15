using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
   public class UserValidator : AbstractValidator<RegisterAuthDto>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("kullanıcı adı boş olamaz"); 
            RuleFor(p => p.Email).NotEmpty().WithMessage("email boş olamaz"); 
            RuleFor(p=> p.ImageUrl).NotEmpty().WithMessage("resim boş olamaz"); 
            RuleFor(p=> p.Password).NotEmpty().WithMessage("şifre boş olamaz");
            RuleFor(p=> p.Password).MinimumLength(6).WithMessage("şifre en az 6 karakter olmalıdır"); 

        }
    }
}
