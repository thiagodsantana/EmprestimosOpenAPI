using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosOpenAPI;

[ApiExplorerSettings(GroupName = "v2")]
public static class EndpointsV2
{
    public static RouteGroupBuilder MapV2Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/emprestimos");

        group.MapPost("/", ([FromServices] EmprestimoServiceV2 service, Emprestimo emprestimo) =>
        {
            var criado = service.Criar(emprestimo);
            return Results.Created($"/emprestimos/{criado.Id}", criado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Cria um novo empréstimo com juros")
        .WithOpenApi();

        group.MapGet("/", ([FromServices] EmprestimoServiceV2 service) =>
        {
            var lista = service.ListarTodos();
            return Results.Ok(lista);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Lista todos os empréstimos")
        .WithOpenApi();

        group.MapGet("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id) =>
        {
            var item = service.ObterPorId(id);
            return item is null ? Results.NotFound() : Results.Ok(item);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Consulta empréstimo por ID")
        .WithOpenApi();

        group.MapPut("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id, Emprestimo novo) =>
        {
            var atualizado = service.Atualizar(id, novo);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Atualiza empréstimo")
        .WithOpenApi();

        group.MapPatch("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id, AtualizacaoEmprestimoDto patch) =>
        {
            var atualizado = service.AtualizarParcial(id, patch);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Atualiza parcialmente")
        .WithOpenApi();

        group.MapDelete("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id) =>
        {
            var sucesso = service.Remover(id);
            return sucesso ? Results.NoContent() : Results.NotFound();
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Remove empréstimo")
        .WithOpenApi();

        return group;
    }
}
