using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EmprestimosOpenAPI.Examples;

public class ValidationProblemDetailsExample : IExamplesProvider<ValidationProblemDetails>
{
    public ValidationProblemDetails GetExamples()
    {
        return new ValidationProblemDetails
        {
            Title = "One or more validation errors occurred.",
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                ["Cliente"] = new[] { "O campo Cliente é obrigatório." },
                ["Valor"] = new[] { "O valor deve ser entre 100 e 100000." }
            }
        };
    }
}
