using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirdHouseProject.Presenters.Common
{
    public class ModelDataValidation
    {
        /// <summary>
        /// Validates the provided model object using data annotations.
        /// </summary>
        /// <param name="model"></param>
        public void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results, true);

            if (isValid == false)
            {
                // Concatenates error messages for each validation failure.
                foreach (var item in results)
                    errorMessage += "- " + item.ErrorMessage + "\n";

                // Throws an exception with the concatenated error messages.
                throw new Exception(errorMessage);
            }
        }
    }
}
