using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosOpenAPI;

[ApiExplorerSettings(GroupName = "v1")]
public static class Endpoints
{
    public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
    {
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
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        group.MapGet("/emprestimos", ([FromServices] EmprestimoService service) =>
        {
            var lista = service.ListarTodos();
            return Results.Ok(lista);
        })
        .WithTags("Empréstimos")
        .WithSummary("Lista todos os empréstimos")
        .Produces<List<Emprestimo>>(200)
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        group.MapGet("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id) =>
        {
            var item = service.ObterPorId(id);
            return item is null ? Results.NotFound() : Results.Ok(item);
        })
        .WithTags("Empréstimos")
        .WithSummary("Consulta empréstimo por ID")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        group.MapPut("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id, Emprestimo novo) =>
        {
            var atualizado = service.Atualizar(id, novo);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Atualiza empréstimo")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        group.MapPatch("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id, AtualizacaoEmprestimoDto patch) =>
        {
            var atualizado = service.AtualizarParcial(id, patch);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos")
        .WithSummary("Atualiza parcialmente")
        .Produces<Emprestimo>(200)
        .Produces(404)
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        group.MapDelete("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, Guid id) =>
        {
            var sucesso = service.Remover(id);
            return sucesso ? Results.NoContent() : Results.NotFound();
        })
        .WithTags("Empréstimos")
        .WithSummary("Remove empréstimo")
        .Produces(204)
        .Produces(404)
        .WithMetadata(new ApiExplorerSettingsAttribute { GroupName = "v1" })
        .WithOpenApi();

        return group;
    }
}