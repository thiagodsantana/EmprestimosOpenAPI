using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

/// <summary>
/// Representa os dados permitidos para atualização parcial de um empréstimo.
/// Utilizada nas operações HTTP PATCH.
/// </summary>
public class AtualizacaoEmprestimoDto
{
    /// <summary>
    /// Novo valor solicitado para o empréstimo.
    /// Deve estar entre R$100 e R$100.000.
    /// </summary>
    [Required(ErrorMessage = "O valor do empréstimo é obrigatório.")]
    [Range(100, 100000, ErrorMessage = "O valor deve estar entre R$100 e R$100.000.")]
    public decimal? Valor { get; set; }

    /// <summary>
    /// Novo prazo para pagamento em meses.
    /// Deve estar entre 2 e 120 meses.
    /// </summary>
    [Required(ErrorMessage = "O prazo é obrigatório.")]
    [Range(2, 120, ErrorMessage = "O prazo deve estar entre 2 e 120 meses.")]
    public int? PrazoMeses { get; set; }
}
