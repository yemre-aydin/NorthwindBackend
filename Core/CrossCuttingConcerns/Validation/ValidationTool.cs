﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var validationContext = new ValidationContext<object>(entity);
            var result = validator.Validate(validationContext);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);

            }
        }
    }
}



