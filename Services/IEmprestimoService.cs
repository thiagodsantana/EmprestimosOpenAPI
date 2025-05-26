using EmprestimosOpenAPI.Models;

namespace EmprestimosOpenAPI.Services
{
    public interface IEmprestimoService
    {
        Emprestimo Criar(Emprestimo emprestimo);
        List<Emprestimo> ListarTodos();
        Emprestimo? ObterPorId(Guid id);
        Emprestimo? Atualizar(Guid id, Emprestimo novo);
        Emprestimo? AtualizarParcial(Guid id, AtualizacaoEmprestimoDto patch);
        bool Remover(Guid id);
    }
}
