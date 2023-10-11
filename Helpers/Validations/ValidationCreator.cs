namespace ACBAbankTask.Helpers.Validations;
using System.Collections.Generic;

public class ValidationCreator
{
    public bool IsValid { get; private set; }
    public List<string> Errors { get; }

    public ValidationCreator()
    {
        IsValid = true;
        Errors = new List<string>();
    }

    public void AddError(string errorMessage)
    {
        IsValid = false;
        Errors.Add(errorMessage);
    }

    public void AddErrors(IEnumerable<string> errorMessages)
    {
        IsValid = false;
        Errors.AddRange(errorMessages);
    }
}
