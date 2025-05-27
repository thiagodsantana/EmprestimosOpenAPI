using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

/// <summary>
/// Representa os dados utilizados para atualização parcial de um empréstimo.
/// Utilizada em operações PATCH.
/// </summary>
public class AtualizacaoEmprestimoDto
{
    /// <summary>
    /// Novo valor solicitado para o empréstimo.
    /// Deve estar entre R$100 e R$100.000.
    /// </summary>
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [SwaggerSchema(Description = "Valor do Empréstimo")]
    [Range(100, 100000, ErrorMessage = "O valor do empréstimo deve ser entre 100 e 100000")]
    public decimal? Valor { get; set; }

    /// <summary>
    /// Novo prazo de pagamento em meses.
    /// Deve estar entre 2 e 120 meses.
    /// </summary>
    [Required(ErrorMessage = "O prazo em meses é obrigatório.")]
    [SwaggerSchema(Description = "Prazo em meses")]
    [Range(2, 120, ErrorMessage = "O prazo em meses do empréstimo deve ser entre 2 e 120")]
    public int? PrazoMeses { get; set; }
}
