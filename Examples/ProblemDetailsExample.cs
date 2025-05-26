using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EmprestimosOpenAPI.Examples;

public class ProblemDetailsExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Title = "Erro interno",
            Status = 500,
            Detail = "Ocorreu um erro inesperado ao processar sua solicitação.",
            Instance = "/v1/emprestimos"
        };
    }
}
