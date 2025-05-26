using EmprestimosOpenAPI.Models;

namespace EmprestimosOpenAPI.Services;
public class EmprestimoService : IEmprestimoService
{
    private readonly List<Emprestimo> _emprestimos = [];

    public Emprestimo Criar(Emprestimo emprestimo)
    {
        _emprestimos.Add(emprestimo);
        return emprestimo;
    }

    public List<Emprestimo> ListarTodos() => _emprestimos;

    public Emprestimo? ObterPorId(Guid id) =>
        _emprestimos.FirstOrDefault(e => e.Id == id);

    public Emprestimo? Atualizar(Guid id, Emprestimo novo)
    {
        var index = _emprestimos.FindIndex(e => e.Id == id);
        if (index == -1) return null;

        novo.Id = id;
        _emprestimos[index] = novo;
        return novo;
    }

    public Emprestimo? AtualizarParcial(Guid id, AtualizacaoEmprestimoDto patch)
    {
        var item = _emprestimos.FirstOrDefault(e => e.Id == id);
        if (item is null) return null;

        if (patch.Valor.HasValue) item.Valor = patch.Valor.Value;
        if (patch.PrazoMeses.HasValue) item.PrazoMeses = patch.PrazoMeses.Value;

        return item;
    }

    public bool Remover(Guid id)
    {
        var item = _emprestimos.FirstOrDefault(e => e.Id == id);
        if (item is null) return false;

        _emprestimos.Remove(item);
        return true;
    }
}
