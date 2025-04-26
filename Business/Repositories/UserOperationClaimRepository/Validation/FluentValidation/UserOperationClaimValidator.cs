using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(p => p.UserId).Must(isValidId).WithMessage("yetki asamasi icin kullanici secimi yapmalisin"); 
           // RuleFor(p => p.UserId).GreaterThan(0).WithMessage("id 0 dan büyük olmalı"); 

            RuleFor(p => p.OperationClaimId).NotEmpty().WithMessage("yetki asamasi icin yetki secimi yapmalisin");
        }
        private bool isValidId(int id)
        {
            if (id > 0 && id != null)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}
