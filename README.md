---

````markdown
# 📘 API de Gestão de Empréstimos

Esta API permite o gerenciamento de contratos de empréstimos, com suporte a versionamento, cálculo de juros e status de aprovação. A documentação está disponível via Swagger UI com suporte multilíngue e exemplos práticos.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8 (Minimal API)
- Swagger / OpenAPI (Swashbuckle)
- Localization (pt-BR)
- FluentValidation / DataAnnotations
- Swashbuckle.AspNetCore.Filters (Exemplos no Swagger)

---

## 🔧 Executando o Projeto

### Pré-requisitos

- .NET 8 SDK ou superior
- Visual Studio 2022+ ou VS Code
- Terminal com suporte a comandos `dotnet`

### Clonar e Executar

```bash
git clone https://github.com/seu-usuario/emprestimos-api.git
cd emprestimos-api
dotnet run
````

A API estará disponível em:
➡️ `http://localhost:5000`
📘 Documentação Swagger: `http://localhost:5000/index.html`

---

## 📂 Estrutura do Projeto

```text
📁 EmprestimosOpenAPI
 ┣ 📂 Models
 ┣ 📂 Services
 ┣ 📂 Examples
 ┣ 📄 Program.cs
 ┣ 📄 Endpoints.cs
 ┣ 📄 EndpointsV2.cs
 ┣ 📄 EmprestimoService.cs
 ┣ 📄 EmprestimoServiceV2.cs
```

---

## 🌐 Versionamento da API

| Versão | Caminho Base | Descrição                                    |
| ------ | ------------ | -------------------------------------------- |
| v1     | `/v1/`       | CRUD básico de empréstimos                   |
| v2     | `/v2/`       | Inclui cálculo de juros e status do contrato |

---

## 📑 Endpoints Principais

### 📌 v1 - Básico

| Método | Endpoint               | Descrição                  |
| ------ | ---------------------- | -------------------------- |
| POST   | `/v1/emprestimos`      | Cria um novo empréstimo    |
| GET    | `/v1/emprestimos`      | Lista todos os empréstimos |
| GET    | `/v1/emprestimos/{id}` | Consulta empréstimo por ID |
| PUT    | `/v1/emprestimos/{id}` | Atualiza completamente     |
| PATCH  | `/v1/emprestimos/{id}` | Atualiza parcialmente      |
| DELETE | `/v1/emprestimos/{id}` | Remove o empréstimo        |

### 📌 v2 - Avançado

Mesmo padrão da v1, porém com os seguintes adicionais:

* Campos extras: `status`, `dataCriacao`, `totalAPagar`
* Cálculo de juros baseado em taxa fixa de 1% ao mês

---

## 🧪 Exemplos no Swagger

Esta API exibe exemplos reais diretamente no Swagger UI usando:

```csharp
options.ExampleFilters();
builder.Services.AddSwaggerExamplesFromAssemblyOf<EmprestimoExample>();
```

Isso facilita a compreensão dos formatos esperados no corpo da requisição e da resposta.

---

## 🌍 Localização

Todas as mensagens de erro e validações são exibidas em **português (pt-BR)** usando:

```csharp
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = new[] { new CultureInfo("pt-BR") },
    SupportedUICultures = new[] { new CultureInfo("pt-BR") }
});
```

---

## ❗ Tratamento de Erros

Respostas de erro seguem o padrão RFC 7807 (`application/problem+json`) com mensagens amigáveis:

```json
{
  "title": "Erro de validação nos dados fornecidos.",
  "status": 400,
  "detail": "Verifique os campos e tente novamente.",
  "errors": {
    "valor": ["O campo Valor é obrigatório."]
  }
}
```

---

## 🔐 Segurança

Para APIs públicas, o projeto pode ser facilmente estendido com:

* Autenticação JWT
* Autorização baseada em claims
* Proteção de endpoints por política (em breve)

---

## 📌 Como Contribuir

1. Fork este repositório
2. Crie uma branch (`git checkout -b feature/minha-feature`)
3. Commit suas alterações (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/minha-feature`)
5. Abra um Pull Request

---

## 📞 Suporte

Entre em contato pelo e-mail:
📨 **[suporte@emprestimos.com](mailto:suporte@emprestimos.com)**

---

## 📝 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.
