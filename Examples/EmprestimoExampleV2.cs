using EmprestimosOpenAPI.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EmprestimosOpenAPI.Examples
{
    public class EmprestimoExampleV2 : IExamplesProvider<EmprestimoV2>
    {
        public EmprestimoV2 GetExamples()
        {
            return new EmprestimoV2
            {
                Id = Guid.Parse("c6b7c0f9-b462-4b0e-bd27-01f9a6d0e024"),
                Cliente = "Ana Clara Monteiro",
                Valor = 20000.00m,
                PrazoMeses = 36,
                TaxaJurosMensal = 1.2,
                DataContrato = new DateTime(2024, 10, 10),
                DataCriacao = DateTime.UtcNow,
                Status = "Aprovado",
                TotalAPagar = 24496.00m
            };
        }
    }
}
