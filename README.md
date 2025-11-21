# CreditoService
 API RESTful em .NET 6 para gerenciar créditos constituídos. 
 O projeto contempla:
 - API com endpoints para CRUD básico (GET all, GET by id, POST)
 - Polling/consumo de mensagens Kafka (a cada 500ms) inserindo créditos recebidos
 - Persistência em PostgreSQL (container)
 - Containerização com Docker
 - Testes unitários com xUnit
 - Padrões: Repository, Service (apoiando SOLID)
 
 
## Como rodar (Docker)
 1. Tenha Docker/Docker Compose instalado.
 2. Na raiz do repositório rode:
 
 ```bash
 docker-compose up --build