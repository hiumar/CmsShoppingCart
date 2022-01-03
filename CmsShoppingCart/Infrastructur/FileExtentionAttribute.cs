using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers.Infrastructur
{
    public class FileExtentionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var file = value as FormFile;
            if(file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                string[] extentions = { "jpg", "png" };
                bool result = extentions.Any(a => extention.EndsWith(a));
                if (!result)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }

        private   string GetErrorMessage()
        {
            return "Allow extention only jpg and pnj";
        }
    }
}
