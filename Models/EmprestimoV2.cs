using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models
{
    /// <summary>
    /// Representa um contrato de empréstimo com dados estendidos (versão 2 da API).
    /// </summary>
    public class EmprestimoV2 : Emprestimo
    {
        [SwaggerSchema(Description = "Data e hora em que o empréstimo foi criado", Format = "date-time", Nullable = true)]
        public DateTime? DataCriacao { get; set; }

        [SwaggerSchema(Description = "Status atual do empréstimo. Exemplos: Pendente, Aprovado, Rejeitado, Quitado", Nullable = true)]
        [StringLength(50, ErrorMessage = "O status não pode exceder 50 caracteres.")]
        public string? Status { get; set; }

        [SwaggerSchema(Description = "Valor total a pagar ao final do contrato, incluindo juros", Format = "decimal", Nullable = true)]
        [Range(0, double.MaxValue, ErrorMessage = "O valor total a pagar deve ser positivo.")]
        public decimal? TotalAPagar { get; set; }
    }
}
