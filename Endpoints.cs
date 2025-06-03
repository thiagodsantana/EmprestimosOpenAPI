using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace EmprestimosOpenAPI
{
    /// <summary>
    /// Define os endpoints da versão 1 (v1) da API de Empréstimos.
    /// Esta versão fornece operações básicas para gerenciamento de empréstimos.
    /// </summary>
    public static class Endpoints
    {
        /// <summary>
        /// Mapeia todos os endpoints relacionados a empréstimos da versão 1 da API.
        /// </summary>
        /// <param name="group">Grupo de rotas para agrupar os endpoints.</param>
        /// <returns>O grupo de rotas atualizado com os endpoints mapeados.</returns>
        public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            /// <summary>
            /// Cria um novo empréstimo.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="emprestimo">Dados do empréstimo a ser criado.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>O recurso criado com código HTTP 201 e dados do empréstimo criado.</returns>
            group.MapPost("/emprestimos", ([FromServices] EmprestimoService service, [FromBody] Emprestimo emprestimo, CancellationToken cancellationToken) =>
            {
                var criado = service.Criar(emprestimo);
                return Results.Created<Emprestimo>($"/emprestimos/{criado.Id}", criado);
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Cria um novo empréstimo")
            .WithDescription("Cria um novo empréstimo com os dados fornecidos no corpo da requisição.")
            .Produces<Emprestimo>(201, "application/json")
            .Produces<ValidationProblemDetails>(400, "application/problem+json")
            .Produces<ProblemDetails>(500, "application/problem+json")
            .WithOpenApi();

            /// <summary>
            /// Lista todos os empréstimos cadastrados.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>Lista completa de empréstimos com código HTTP 200.</returns>
            group.MapGet("/emprestimos", ([FromServices] EmprestimoService service, CancellationToken cancellationToken) =>
            {
                var lista = service.ListarTodos();
                return Results.Ok(lista);
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Lista todos os empréstimos")
            .WithDescription("Retorna a lista completa de empréstimos cadastrados no sistema.")
            .Produces<List<Emprestimo>>(200, "application/json")
            .WithOpenApi();

            /// <summary>
            /// Consulta um empréstimo pelo seu identificador único (GUID).
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="id">Identificador único do empréstimo.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>Empréstimo encontrado com código HTTP 200 ou 404 se não existir.</returns>
            group.MapGet("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, [FromRoute] Guid id, CancellationToken cancellationToken) =>
            {
                var item = service.ObterPorId(id);
                return item is null ? Results.NotFound() : Results.Ok(item);
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Consulta empréstimo por ID")
            .WithDescription("Retorna um empréstimo específico identificado pelo GUID.")
            .Produces<Emprestimo>(200, "application/json")
            .Produces(404)
            .WithOpenApi();

            /// <summary>
            /// Atualiza completamente os dados de um empréstimo existente.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="id">Identificador único do empréstimo a ser atualizado.</param>
            /// <param name="novo">Novos dados para substituir o empréstimo existente.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>Empréstimo atualizado com código HTTP 200 ou 404 se não encontrado.</returns>
            group.MapPut("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, [FromRoute] Guid id, [FromBody] Emprestimo novo, CancellationToken cancellationToken) =>
            {
                var atualizado = service.Atualizar(id, novo);
                return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Atualiza empréstimo")
            .WithDescription("Atualiza completamente os dados de um empréstimo existente.")
            .Produces<Emprestimo>(200, "application/json")
            .Produces(404)
            .WithOpenApi();

            /// <summary>
            /// Atualiza parcialmente os dados de um empréstimo.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="id">Identificador único do empréstimo a ser atualizado.</param>
            /// <param name="patch">Dados parciais para atualização, como valor ou prazo.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>Empréstimo atualizado com código HTTP 200 ou 404 se não encontrado.</returns>
            group.MapPatch("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, [FromRoute] Guid id, [FromBody] AtualizacaoEmprestimoDto patch, CancellationToken cancellationToken) =>
            {
                var atualizado = service.AtualizarParcial(id, patch);
                return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Atualiza parcialmente")
            .WithDescription("Atualiza parcialmente os dados de um empréstimo, como valor ou prazo.")
            .Produces<Emprestimo>(200, "application/json")
            .Produces(404)
            .WithOpenApi();

            /// <summary>
            /// Remove um empréstimo do sistema pelo seu identificador único.
            /// </summary>
            /// <param name="service">Serviço responsável pela lógica de empréstimos.</param>
            /// <param name="id">Identificador único do empréstimo a ser removido.</param>
            /// <param name="cancellationToken">Token para cancelamento da operação.</param>
            /// <returns>Código HTTP 204 em caso de sucesso ou 404 se não encontrado.</returns>
            group.MapDelete("/emprestimos/{id:guid}", ([FromServices] EmprestimoService service, [FromRoute] Guid id, CancellationToken cancellationToken) =>
            {
                var sucesso = service.Remover(id);
                return sucesso ? Results.NoContent() : Results.NotFound();
            })
            .WithTags("Empréstimos V1")
            .WithSummary("Remove empréstimo")
            .WithDescription("Remove um empréstimo do sistema pelo seu identificador único.")
            .Produces(204)
            .Produces(404)
            .WithOpenApi();

            return group;
        }
    }
}
