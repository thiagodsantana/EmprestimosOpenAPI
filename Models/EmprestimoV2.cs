using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

/// <summary>
/// Representa um contrato de empréstimo com dados estendidos (versão 2 da API).
/// Herda as propriedades básicas de <see cref="Emprestimo"/> e adiciona status, data de criação e valor total.
/// </summary>
public class EmprestimoV2 : Emprestimo
{
    /// <summary>
    /// Data e hora em que o empréstimo foi criado.
    /// Pode ser nula em versões antigas ou registros incompletos.
    /// </summary>
    public DateTime? DataCriacao { get; set; }

    /// <summary>
    /// Status atual do empréstimo.
    /// Exemplos de status: Pendente, Aprovado, Rejeitado, Quitado.
    /// </summary>
    [StringLength(50, ErrorMessage = "O status não pode exceder 50 caracteres.")]
    public string? Status { get; set; }

    /// <summary>
    /// Valor total a ser pago ao final do contrato, incluindo juros.
    /// Calculado automaticamente com base no valor, prazo e taxa de juros.
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "O valor total a pagar deve ser positivo.")]
    public decimal? TotalAPagar { get; set; }
}
