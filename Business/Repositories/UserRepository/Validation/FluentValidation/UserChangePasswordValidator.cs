using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("şifre boş olamaz");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("şifre en az 6 karakter olmalıdır");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("şifre en az bir büyük harf içermelidir");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("şifre en az bir küçük harf içermelidir");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("şifre en az bir küçük harf içermelidir");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("şifreniz en az 1 adet özel karakter içermelidir");
        }
    }
}
