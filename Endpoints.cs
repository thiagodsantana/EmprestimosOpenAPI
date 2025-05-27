using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosOpenAPI;

/// <summary>
/// Define os endpoints da versão 1 (v1) da API de Empréstimos.
/// Essa versão é mais simples e não inclui cálculo de juros ou status.
/// </summary>
[ApiExplorerSettings(GroupName = "v1")]
public static class Endpoints
{
    /// <summary>
    /// Mapeia os endpoints da versão 1 da API de Empréstimos.
    /// Permite operações CRUD básicas: criação, listagem, consulta, atualização e remoção.
    /// </summary>
    /// <param name="group">Grupo de rotas base para a API.</param>
    /// <returns>Grupo de rotas configurado.</returns>
    public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
    {
        /// <summary>
        /// Cria um novo empréstimo com os dados fornecidos.
        /// </summary>
        group.MapPost("/emprestimos", ([FromServices] EmprestimoService service, Emprestimo emprestimo) =>
        {
            var criado = service.Criar(emprestimo);
            return Results.Created($"/emprestimos/{criado.Id}", criado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Cria um novo empréstimo")
        .Produces<Emprestimo>(201)
        .Produces<ValidationProblemDetails>(400)
        .Produces<ProblemDetails>(500)
        .WithOpenApi();

        /// <summary>
        /// Retorna a lista de todos os empréstimos cadastrados.
        /// </summary>
        group.MapGet("/emprestimos", ([FromServices] EmprestimoService service) =>
        {
            var lista = service.ListarTodos();
            return Results.Ok(lista);
        })
        .WithTags("Empréstimos")
        .WithSummary("Lista todos os empréstimos")
        .Produces<List<Emprestimo>>(200)
        .WithOpenApi();

        /// <summary>
        /// Consulta um empréstimo específico pelo identificador único (GUID).
        /// </summary>
        group.MapGet("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id) =>
        {
            var item = service.ObterPorId(id);
            return item is null ? Results.NotFound() : Results.Ok(item);
        })
        .WithTags("Empréstimos")
        .WithSummary("Consulta empréstimo por ID")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithOpenApi();

        /// <summary>
        /// Atualiza completamente os dados de um empréstimo existente.
        /// </summary>
        group.MapPut("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id, Emprestimo novo) =>
        {
            var atualizado = service.Atualizar(id, novo);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Atualiza empréstimo")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithOpenApi();

        /// <summary>
        /// Atualiza parcialmente os dados de um empréstimo (por exemplo, valor ou prazo).
        /// </summary>
        group.MapPatch("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id, AtualizacaoEmprestimoDto patch) =>
        {
            var atualizado = service.AtualizarParcial(id, patch);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Atualiza parcialmente")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithOpenApi();

        /// <summary>
        /// Remove um empréstimo do sistema com base no seu identificador único.
        /// </summary>
        group.MapDelete("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id) =>
        {
            var sucesso = service.Remover(id);
            return sucesso ? Results.NoContent() : Results.NotFound();
        })
        .WithTags("Empréstimos")
        .WithSummary("Remove empréstimo")
        .Produces(204)
        .Produces(404)
        .WithOpenApi();

        return group;
    }
}
