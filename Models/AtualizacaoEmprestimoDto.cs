using System.ComponentModel.DataAnnotations;

namespace EmprestimosOpenAPI.Models;

public class AtualizacaoEmprestimoDto
{
    [Range(100, 100000)]
    public decimal? Valor { get; set; }

    [Range(1, 120)]
    public int? PrazoMeses { get; set; }
}
