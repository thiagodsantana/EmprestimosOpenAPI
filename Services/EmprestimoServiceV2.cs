using EmprestimosOpenAPI.Models;

namespace EmprestimosOpenAPI.Services;

public class EmprestimoServiceV2 : IEmprestimoService
{
    private static readonly List<Emprestimo> emprestimos = [];

    private const decimal JurosMensal = 0.01m;

    public Emprestimo Criar(Emprestimo emprestimo)
    {
        emprestimo.Id = Guid.NewGuid();
        emprestimo.DataContrato = DateTime.UtcNow;
        emprestimo.Status = "Pendente";
        emprestimo.TotalAPagar = CalcularTotalComJuros(emprestimo.Valor, emprestimo.PrazoMeses);

        emprestimos.Add(emprestimo);
        return emprestimo;
    }

    public List<Emprestimo> ListarTodos() => emprestimos;

    public Emprestimo? ObterPorId(Guid id) => emprestimos.FirstOrDefault(e => e.Id == id);

    public Emprestimo? Atualizar(Guid id, Emprestimo novo)
    {
        var index = emprestimos.FindIndex(e => e.Id == id);
        if (index == -1) return null;

        novo.Id = id;
        novo.TotalAPagar = CalcularTotalComJuros(novo.Valor, novo.PrazoMeses);
        emprestimos[index] = novo;

        return novo;
    }

    public Emprestimo? AtualizarParcial(Guid id, AtualizacaoEmprestimoDto patch)
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

    private decimal CalcularTotalComJuros(decimal valor, int prazoMeses)
        => Math.Round(valor * (decimal)Math.Pow(1 + (double)JurosMensal, prazoMeses), 2);
}
