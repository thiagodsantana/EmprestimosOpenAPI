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
    /// Mapeia os endpoints da API v1 no grupo fornecido.
    /// </summary>
    public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
    {
        // 🔹 POST /emprestimos
        // Cria um novo empréstimo com os dados fornecidos.
        group.MapPost("/emprestimos", ([FromServices] EmprestimoService service, Emprestimo emprestimo) =>
        {
            var criado = service.Criar(emprestimo);
            return Results.Created($"/emprestimos/{criado.Id}", criado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Cria um novo empréstimo")
        .Produces<Emprestimo>(201) // Retorna o empréstimo criado
        .Produces<ValidationProblemDetails>(400) // Dados inválidos
        .Produces<ProblemDetails>(500) // Erro interno
        .WithOpenApi();

        // 🔹 GET /emprestimos
        // Lista todos os empréstimos cadastrados.
        group.MapGet("/emprestimos", ([FromServices] EmprestimoService service) =>
        {
            var lista = service.ListarTodos();
            return Results.Ok(lista);
        })
        .WithTags("Empréstimos")
        .WithSummary("Lista todos os empréstimos")
        .Produces<List<Emprestimo>>(200)
        .WithOpenApi();

        // 🔹 GET /emprestimos/{id}
        // Consulta um empréstimo específico pelo ID (GUID).
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

        // 🔹 PUT /emprestimos/{id}
        // Atualiza todos os dados de um empréstimo existente.
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

        // 🔹 PATCH /emprestimos/{id}
        // Atualiza parcialmente um empréstimo (por exemplo, alterar apenas o valor ou prazo).
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

        // 🔹 DELETE /emprestimos/{id}
        // Remove um empréstimo da base de dados.
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
