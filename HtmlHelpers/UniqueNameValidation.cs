using FoodStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodStore.HtmlHelpers
{
    public class UniqueNameValidation : ValidationAttribute
    {

        public Type ObjectType { get; private set; }
        public UniqueNameValidation(Type type)
        {
            ObjectType = type;
        }

        public override bool IsValid(object value)
        {            
            if (!(value is string))
            {
                return false;
            }
            var dbContext = new EFDbContext();
            return dbContext.PublicProfiles.FirstOrDefault(e => e.Name == (string)value) == null;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ObjectType == typeof(string))
            {
                var dbContext = new EFDbContext();
                var userId = HttpContext.Current.User.Identity.GetUserId();

                var dbEntry = dbContext.PublicProfiles.FirstOrDefault(e => e.Name == (string)value);

                if(dbEntry == null || String.IsNullOrEmpty(dbEntry.Name))
                {
                    return ValidationResult.Success;
                }

                if(userId == dbEntry.UserId) // the person is changing something else
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Name already exists");

            }

            return new ValidationResult("Generic Public Profile Name Validation Fail");
        }

    }
}