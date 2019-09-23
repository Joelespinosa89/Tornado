using Padron.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Padron.Validation
{
    public class CheckOnlyCedula: ValidationAttribute
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public CheckOnlyCedula()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cedula = (string)value;
            cedula = cedula.Replace("-", "").Trim();
            var existPerson = _db.ContactForms.Any(x => x.Cedula == cedula);

            if (existPerson)
            {
                return new ValidationResult("Ya existe una persona registrada con esta cédula, favor registrar otra.",new string[] {"Cedula" });
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}