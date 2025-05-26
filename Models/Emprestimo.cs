using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

/// <summary>
/// Representa um contrato de empréstimo (versão 1 da API).
/// </summary>

public class Emprestimo
{
    [SwaggerSchema(ReadOnly = true, Description = "Identificador único do empréstimo", Format = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
    [SwaggerSchema(Description = "Nome completo do cliente", Nullable = false)]
    public string Cliente { get; set; } = default!;

    [Required(ErrorMessage = "O valor do empréstimo é obrigatório.")]
    [Range(100, 100000, ErrorMessage = "O valor deve estar entre R$100 e R$100.000.")]
    [SwaggerSchema(Description = "Valor solicitado para empréstimo", Format = "decimal", Nullable = false)]
    public decimal Valor { get; set; }

    [Range(1, 120, ErrorMessage = "O prazo deve estar entre 1 e 120 meses.")]
    [SwaggerSchema(Description = "Quantidade de meses para pagamento", Nullable = false)]
    public int PrazoMeses { get; set; }

    [SwaggerSchema(Description = "Taxa de juros mensal aplicada", Format = "double", Nullable = false)]
    public double TaxaJurosMensal { get; set; } = 1.0;

    [SwaggerSchema(Description = "Data de assinatura do contrato", Format = "date-time", Nullable = false)]
    public DateTime DataContrato { get; set; } = DateTime.UtcNow;
}
