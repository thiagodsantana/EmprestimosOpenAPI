using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

/// <summary>
/// Representa um contrato de empréstimo (versão 1 da API).
/// Contém dados do cliente, valor, prazo e informações contratuais.
/// </summary>
public class Emprestimo
{
    /// <summary>
    /// Identificador único do empréstimo.
    /// Gerado automaticamente no momento da criação.
    /// </summary>
    [SwaggerSchema(ReadOnly = true, Description = "Identificador único do empréstimo", Format = "uuid", Nullable = false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Nome completo do cliente solicitante do empréstimo.
    /// Obrigatório e limitado a 100 caracteres.
    /// </summary>
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
    [SwaggerSchema(Description = "Nome completo do cliente", Nullable = false)]
    public string Cliente { get; set; } = default!;

    /// <summary>
    /// Valor solicitado pelo cliente para o empréstimo.
    /// Deve estar entre R$100 e R$100.000.
    /// </summary>
    [Required(ErrorMessage = "O valor do empréstimo é obrigatório.")]
    [Range(100, 100000, ErrorMessage = "O valor deve estar entre R$100 e R$100.000.")]
    [SwaggerSchema(Description = "Valor solicitado para empréstimo", Format = "decimal", Nullable = false)]
    public decimal Valor { get; set; }

    /// <summary>
    /// Prazo para pagamento do empréstimo em meses.
    /// Deve estar entre 1 e 120 meses.
    /// </summary>
    [Range(1, 120, ErrorMessage = "O prazo deve estar entre 1 e 120 meses.")]
    [SwaggerSchema(Description = "Quantidade de meses para pagamento", Nullable = false)]
    public int PrazoMeses { get; set; }

    /// <summary>
    /// Taxa de juros mensal aplicada ao valor do empréstimo.
    /// Valor padrão é 1.0 (%).
    /// </summary>
    [SwaggerSchema(Description = "Taxa de juros mensal aplicada", Format = "double", Nullable = false)]
    public double TaxaJurosMensal { get; set; } = 1.0;

    /// <summary>
    /// Data e hora da assinatura do contrato de empréstimo.
    /// Registrada automaticamente no momento da criação.
    /// </summary>
    [SwaggerSchema(Description = "Data de assinatura do contrato", Format = "date-time", Nullable = false)]
    public DateTime DataContrato { get; set; } = DateTime.UtcNow;
}
