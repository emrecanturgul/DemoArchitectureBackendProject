using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication.Validation.FluentValidation
{
   public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("kullanıcı adı boş olamaz"); 
            RuleFor(p => p.Email).NotEmpty().WithMessage("email boş olamaz"); 
            RuleFor(p => p.Email).EmailAddress().WithMessage("geçerli bir mail adresi giriniz"); 
            RuleFor(p=> p.Image.Name).NotEmpty().WithMessage("resim boş olamaz"); 
            RuleFor(p=> p.Password).NotEmpty().WithMessage("şifre boş olamaz");
            RuleFor(p=> p.Password).MinimumLength(6).WithMessage("şifre en az 6 karakter olmalıdır"); 
            RuleFor(p=> p.Password).Matches("[A-Z]").WithMessage("şifre en az bir büyük harf içermelidir");
            RuleFor(p=> p.Password).Matches("[a-z]").WithMessage("şifre en az bir küçük harf içermelidir");
            RuleFor(p=> p.Password).Matches("[0-9]").WithMessage("şifre en az bir küçük harf içermelidir");
            RuleFor(p=> p.Password).Matches("[^a-zA-Z0-9]").WithMessage("şifreniz en az 1 adet özel karakter içermelidir");



        }
    }
}
