# Estrutura do projeto


## Padrões

**DDD
(Domain-Driven Design)**: O projeto é totalmente estruturado com o DDD. 

**Design Pattern Builder:**
Utilizado padrão de design Builder, padrão de categoria Criacional para elaboração de filtros com mais precisão e código limpo.

**S.O.L.I.D**: aplicado os princípios do SOLID como as injeções de dependências dos serviços e repositórios.

**JWT Role Token**: para validação de permissão das rotas de usuários Adm ou Comum

**IOptions**: captura os valores do appsettings.json implementando os valores nas classes onde pode ser controlado em variáveis de ambientes e secrets

**FluentValidator**: Para realizar as validações dos objetos recebidos na API

**Custom Exception**: Exceções personalizadas para o Domínio


## Testes

**Teste Unitário - FluentAssertions**: Testes unitários criados para validar os serviços e aumento de cobertura para o SonarQube, aplicando o **Fixture** para criação de valores para o objeto