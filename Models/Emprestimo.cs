using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

public class Emprestimo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [SwaggerSchema("Nome completo do cliente")]
    [StringLength(100)]
    public string Cliente { get; set; } = default!;

    [Required]
    [SwaggerSchema("Valor do empréstimo")]
    [Range(100, 100000)]
    public decimal Valor { get; set; }

    [Range(1, 120)]
    [SwaggerSchema("Prazo do empréstimo")]
    public int PrazoMeses { get; set; }

    [SwaggerSchema("Taxa juros do empréstimo")]
    public double TaxaJurosMensal { get; set; } = 1.0;

    [SwaggerSchema("Data do contrato do empréstimo")]
    public DateTime DataContrato { get; set; } = DateTime.UtcNow;

    //V2
    public DateTime? DataCriacao { get; set; }
    public string? Status { get; set; }
    public decimal? TotalAPagar { get; set; }
}
