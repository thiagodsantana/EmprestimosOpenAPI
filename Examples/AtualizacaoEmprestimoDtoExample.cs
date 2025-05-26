using EmprestimosOpenAPI.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EmprestimosOpenAPI.Examples;

public class AtualizacaoEmprestimoDtoExample : IExamplesProvider<AtualizacaoEmprestimoDto>
{
    public AtualizacaoEmprestimoDto GetExamples()
    {
        return new AtualizacaoEmprestimoDto
        {
            Valor = 18000,
            PrazoMeses = 30
        };
    }
}
