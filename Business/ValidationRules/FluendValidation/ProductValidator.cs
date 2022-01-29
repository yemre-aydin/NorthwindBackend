using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluendValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {

        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(2, 30);
        }
    }
}
