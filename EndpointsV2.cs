using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace EmprestimosOpenAPI
{
    /// <summary>
    /// Define os endpoints da versão 2 (v2) da API de Empréstimos.
    /// Esta versão inclui funcionalidades aprimoradas com suporte a juros nos empréstimos.
    /// </summary>
    [ApiExplorerSettings(GroupName = "v2")]
    public static class EndpointsV2
    {
        /// <summary>
        /// Mapeia todos os endpoints relacionados a empréstimos da versão 2 da API.
        /// </summary>
        /// <param name="group">O grupo de rotas para agrupar os endpoints.</param>
        /// <returns>O grupo de rotas atualizado com os endpoints mapeados.</returns>
        public static RouteGroupBuilder MapEndpointsV2(this RouteGroupBuilder group)
        {
            /// <summary>
            /// Cria um novo empréstimo com juros.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="emprestimo">Dados do empréstimo a ser criado.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Retorna o recurso criado com código HTTP 201 e os dados do empréstimo criado.</returns>
            group.MapPost("/emprestimos", ([FromServices] EmprestimoServiceV2 service, [FromBody] EmprestimoV2 emprestimo, CancellationToken cancellationToken) =>
            {
                var criado = service.Criar(emprestimo);
                return Results.Created<EmprestimoV2>($"/emprestimos/{criado.Id}", criado);
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Cria um novo empréstimo com juros")
            .WithDescription("Cria um novo empréstimo com juros, com os dados fornecidos no corpo da requisição.")
            .Produces<EmprestimoV2>(201, "application/json")
            .Produces<ValidationProblemDetails>(400, "application/problem+json")
            .Produces<ProblemDetails>(500, "application/problem+json")
            .WithOpenApi()
            .WithGroupName("v2");

            /// <summary>
            /// Obtém a lista completa de empréstimos cadastrados, incluindo juros.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Lista de empréstimos cadastrados com código HTTP 200.</returns>
            group.MapGet("/emprestimos", ([FromServices] EmprestimoServiceV2 service, CancellationToken cancellationToken) =>
            {
                var lista = service.ListarTodos();
                return Results.Ok(lista);
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Lista todos os empréstimos")
            .WithDescription("Retorna a lista completa de empréstimos cadastrados no sistema, incluindo juros.")
            .Produces<List<EmprestimoV2>>(200, "application/json")
            .WithOpenApi()
            .WithGroupName("v2");

            /// <summary>
            /// Consulta um empréstimo específico pelo seu identificador único (GUID).
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="id">Identificador único do empréstimo a ser consultado.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Dados do empréstimo encontrado ou código 404 caso não exista.</returns>
            group.MapGet("/emprestimos/{id:guid}", ([FromServices] EmprestimoServiceV2 service, [FromRoute] Guid id, CancellationToken cancellationToken) =>
            {
                var item = service.ObterPorId(id);
                return item is null ? Results.NotFound() : Results.Ok(item);
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Consulta empréstimo por ID")
            .WithDescription("Retorna um empréstimo específico identificado pelo GUID, incluindo informações de juros.")
            .Produces<EmprestimoV2>(200, "application/json")
            .Produces(404)
            .WithOpenApi()
            .WithGroupName("v2");

            /// <summary>
            /// Atualiza completamente os dados de um empréstimo existente.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="id">Identificador único do empréstimo a ser atualizado.</param>
            /// <param name="novo">Novos dados para substituir o empréstimo existente.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Empréstimo atualizado ou código 404 se não encontrado.</returns>
            group.MapPut("/emprestimos/{id:guid}", ([FromServices] EmprestimoServiceV2 service, [FromRoute] Guid id, [FromBody] EmprestimoV2 novo, CancellationToken cancellationToken) =>
            {
                var atualizado = service.Atualizar(id, novo);
                return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Atualiza empréstimo")
            .WithDescription("Atualiza completamente os dados de um empréstimo existente, incluindo juros.")
            .Produces<EmprestimoV2>(200, "application/json")
            .Produces(404)
            .WithOpenApi()
            .WithGroupName("v2");

            /// <summary>
            /// Atualiza parcialmente os dados de um empréstimo, como valor, prazo e juros.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="id">Identificador único do empréstimo a ser atualizado.</param>
            /// <param name="patch">Dados parciais para atualização.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Empréstimo atualizado ou código 404 se não encontrado.</returns>
            group.MapPatch("/emprestimos/{id:guid}", ([FromServices] EmprestimoServiceV2 service, [FromRoute] Guid id, [FromBody] AtualizacaoEmprestimoDto patch, CancellationToken cancellationToken) =>
            {
                var atualizado = service.AtualizarParcial(id, patch);
                return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Atualiza parcialmente")
            .WithDescription("Atualiza parcialmente os dados de um empréstimo, como valor, prazo e juros.")
            .Produces<EmprestimoV2>(200, "application/json")
            .Produces(404)
            .WithOpenApi()
            .WithGroupName("v2");

            /// <summary>
            /// Remove um empréstimo do sistema pelo seu identificador único.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de negócio dos empréstimos V2.</param>
            /// <param name="id">Identificador único do empréstimo a ser removido.</param>
            /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
            /// <returns>Código 204 em caso de sucesso ou 404 se o empréstimo não for encontrado.</returns>
            group.MapDelete("/emprestimos/{id:guid}", ([FromServices] EmprestimoServiceV2 service, [FromRoute] Guid id, CancellationToken cancellationToken) =>
            {
                var sucesso = service.Remover(id);
                return sucesso ? Results.NoContent() : Results.NotFound();
            })
            .WithTags("Empréstimos V2")
            .WithSummary("Remove empréstimo")
            .WithDescription("Remove um empréstimo do sistema pelo seu identificador único.")
            .Produces(204)
            .Produces(404)
            .WithOpenApi()
            .WithGroupName("v2");

            return group;
        }
    }
}
