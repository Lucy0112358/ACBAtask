using System.Text.RegularExpressions;
using ACBAbankTask.DataModels;

namespace ACBAbankTask.Helpers.Validations
{
    public class DocumentValidation
    {
        public ValidationCreator ValidateDocument(DocumentDto document)
        {
            var validationResult = new ValidationCreator();

            // title rules
            if (string.IsNullOrWhiteSpace(document.Title))
            {
                validationResult.AddError("Անվանումը պարտադիր դաշտ է");
            }
            if (document.Title.Length > 50)
            {
                validationResult.AddError("Անվանումը պետք է պարունակի մինչև 50 նիշ");
            }
            if (!IsValidTitle(document.Title))
            {
                validationResult.AddError("Անվանումը չպետք է պարունակի նշաններ կամ թվեր");
            }

            // number rules 
            if (string.IsNullOrWhiteSpace(document.Number))
            {
                validationResult.AddError("Այս դաշտը պարտադիր է");
            }
            else if (document.Number.Length > 20)
            {
                validationResult.AddError("Դաշտը չպետք է անցնի 20 նիշից");
            }
            else if (!IsValidNumber(document.Number))
            {
                validationResult.AddError("Դաշտը պետք է պարունակի միայն թվեր և լատինական տառեր");
            }

            return validationResult;
        }

        private bool IsValidNumber(string number)
        {
                return Regex.IsMatch(number, "^[0-9a-zA-Z]+$");
        }
        private bool IsValidTitle(string title)
        {
                return Regex.IsMatch(title, "^[a-zA-Z ]+$");
        }
    }
}
