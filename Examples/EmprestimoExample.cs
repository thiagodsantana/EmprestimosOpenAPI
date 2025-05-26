using EmprestimosOpenAPI.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EmprestimosOpenAPI.Examples;

public class EmprestimoExample : IExamplesProvider<Emprestimo>
{
    public Emprestimo GetExamples() => new()
    {
        Cliente = "João da Silva",
        Valor = 15000,
        PrazoMeses = 36,
        TaxaJurosMensal = 1.2,
        DataContrato = DateTime.UtcNow
    };
}
