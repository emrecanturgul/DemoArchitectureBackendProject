﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OperationClaimRepository.Validation.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().WithMessage("yetki adi bos olamaz");
        }
    }
    
}
