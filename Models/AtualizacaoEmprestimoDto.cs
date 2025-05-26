using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

public class AtualizacaoEmprestimoDto
{
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [SwaggerSchema(Description = "Valor do Empréstimo")]
    [Range(100, 100000, ErrorMessage = "O valor do empréstimo deve ser entre 100 e 100000")]
    public decimal? Valor { get; set; }

    [Required(ErrorMessage = "O prazo em meses é obrigatório.")]
    [SwaggerSchema(Description = "Prazo em meses")]
    [Range(2, 120, ErrorMessage = "O prazo em meses do empréstimo deve ser entre 2 e 120")]
    public int? PrazoMeses { get; set; }
}
