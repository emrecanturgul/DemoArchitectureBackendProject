﻿using FluentValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {    
       public static void Validate(IValidator validator , object entity)
        {   
            var context = new ValidationContext<object>(entity); 
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

        }
        

        
    }
}
