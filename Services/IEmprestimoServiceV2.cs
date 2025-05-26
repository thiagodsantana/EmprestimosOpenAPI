using EmprestimosOpenAPI.Models;

namespace EmprestimosOpenAPI.Services
{
    public interface IEmprestimoServiceV2
    {
        EmprestimoV2 Criar(EmprestimoV2 emprestimo);
        List<EmprestimoV2> ListarTodos();
        EmprestimoV2? ObterPorId(Guid id);
        EmprestimoV2? Atualizar(Guid id, EmprestimoV2 novo);
        EmprestimoV2? AtualizarParcial(Guid id, AtualizacaoEmprestimoDto patch);
        bool Remover(Guid id);
    }
}
