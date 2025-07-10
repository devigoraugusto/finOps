# finOps
Implementar um serviço onde uma empresa pode solicitar a antecipação de recebíveis, calculando o valor a ser antecipado com base nas notas fiscais cadastradas e no limite de crédito, que varia de acordo com o faturamento mensal e o ramo da empresa.

## Tecnologias
- .NET 8.0
- ASP.NET Core MinimalAPI
- Entity Framework Core
- MediatR
- FluentValidation
- Docker

## Organização
finOps/
│
├── finOps.Api/ → Camada de apresentação (MinimalAPI)
├── finOps.Application/ → Casos de uso, handlers, validações
├── finOps.Domain/ → Entidades e regras de domínio
└── finOps.Infra/ → Acesso a dados com EF Core

## Como rodar
### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com) (opcional para execução containerizada)

---

### Clone o repositório

```bash
git clone https://github.com/devigoraugusto/finOps.git
cd finOps
```

### Como rodar o projeto localmente
```bash
dotnet restore
```

#### Crie ou atualize o DB
```bash
dotnet ef database update --project finOps.Infra
```

#### Execute a aplicação
```bash
dotnet run --project finOps.Api
```

### Rodando com Docker
```bash
docker compose up -d --build # Cria e inicia os containers

docker compose down # Para e remove os containers
```


--- 
## Melhoreias a serem implementadas

- [ ] Implementar autenticação JWT
- [ ] Implementar testes unitários e de integração
- [ ] Melhorar a documentação da API Swagger
- [ ] Modularizar Inversão de Dependências por camadas
- [ ] Adicionar seed de dados iniciais
- [ ] Configurar migrations automáticas no startup
- [ ] Usar Options Pattern para configurações
- [ ] Adicionar CI/CD com GitHub Actions
- [ ] Adicionar Health Checks
- [ ] Adicionar Logging estruturado
- [ ] Adicionar exemplos de request/response na documentação Swagger


