using EmprestimosOpenAPI.Models;

namespace EmprestimosOpenAPI.Services;

public class EmprestimoServiceV2 : IEmprestimoServiceV2
{
    private static readonly List<EmprestimoV2> emprestimos = [];

    private const decimal JurosMensal = 0.01m;

    public EmprestimoV2 Criar(EmprestimoV2 emprestimo)
    {
        emprestimo.Id = Guid.NewGuid();
        emprestimo.DataContrato = DateTime.UtcNow;
        emprestimo.Status = "Pendente";
        emprestimo.TotalAPagar = CalcularTotalComJuros(emprestimo.Valor, emprestimo.PrazoMeses);

        emprestimos.Add(emprestimo);
        return emprestimo;
    }

    public List<EmprestimoV2> ListarTodos() => emprestimos;

    public EmprestimoV2? ObterPorId(Guid id) => emprestimos.FirstOrDefault(e => e.Id == id);

    public EmprestimoV2? Atualizar(Guid id, EmprestimoV2 novo)
    {
        var index = emprestimos.FindIndex(e => e.Id == id);
        if (index == -1) return null;

        novo.Id = id;
        novo.TotalAPagar = CalcularTotalComJuros(novo.Valor, novo.PrazoMeses);
        emprestimos[index] = novo;

        return novo;
    }

    public EmprestimoV2? AtualizarParcial(Guid id, AtualizacaoEmprestimoDto patch)
    {
        var emprestimo = emprestimos.FirstOrDefault(e => e.Id == id);
        if (emprestimo is null) return null;

        if (patch.Valor.HasValue)
            emprestimo.Valor = patch.Valor.Value;

        if (patch.PrazoMeses.HasValue)
            emprestimo.PrazoMeses = patch.PrazoMeses.Value;

        emprestimo.TotalAPagar = CalcularTotalComJuros(emprestimo.Valor, emprestimo.PrazoMeses);
        return emprestimo;
    }

    public bool Remover(Guid id)
    {
        var item = emprestimos.FirstOrDefault(e => e.Id == id);
        if (item is null) return false;

        emprestimos.Remove(item);
        return true;
    }

    private static decimal CalcularTotalComJuros(decimal valor, int prazoMeses)
    {
        return Math.Round(valor * (decimal)Math.Pow(1 + (double)JurosMensal, prazoMeses), 2);
    }
}
