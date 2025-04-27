# 📘 Avaliação Técnica – Clean Architecture + Azure SQL

Este repositório contém minha entrega referente à avaliação técnica baseada no repositório original do professor:  
[https://github.com/victoricoma/avaliacao-tp2-helpapp](https://github.com/victoricoma/avaliacao-tp2-helpapp)

---

## ✅ Objetivo

Implementar os repositórios `Category` e `Product` seguindo os padrões da Clean Architecture, aplicar a migration `Initial` e conectar a aplicação com uma instância de SQL Server no Azure.

---

## 🚀 Funcionalidades implementadas

- [x] Repositórios `CategoryRepository` e `ProductRepository`
- [x] Configurações com `EntityTypeConfiguration` para `Category` e `Product`
- [x] Injeção de dependência configurada (`DependencyInjectionAPI`)
- [x] Migration `Initial` criada com `HasData()` para categorias
- [x] Banco de dados SQL Server criado no Azure
- [x] Migration aplicada com sucesso no Azure via `dotnet ef database update`

---

# Criação da Branch

![minhabranch](https://github.com/user-attachments/assets/b109a744-8253-4d8c-b987-85d312e97035)


# 🔧 Comandos utilizados
## Criação da migration
dotnet ef migrations add Initial --project Infra.Data --startup-project WebAPI

## Aplicação no banco de dados (Azure)
dotnet ef database update --project Infra.Data --startup-project WebAPI

![comandomigration](https://github.com/user-attachments/assets/ab4bde1a-f3c1-4936-b607-db4190a6ed60)


# 🔗 String de conexão (mascarada)  Usando User Secrets (recomendado para desenvolvimento) Demonstração via API para ver a string sendo mascarada

  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR_AQUI;Database=NOME_BANCO_AQUI;iser ID=SEU_USUARIO_AQUI;password=SUA_SENHA_AQUI;Encrypt=True;TrustServerCertificate=False;ConnectionTimeout=30"},
    
![conexaomascarada](https://github.com/user-attachments/assets/35835852-dd03-4df6-bb6c-a722799cd010)

# ☁️ Configuração no Azure
SQL Server criado no portal Azure

Banco de dados nomeado: avaliacao_tp2_pedrobachiega

IP local autorizado no firewall

![meuip](https://github.com/user-attachments/assets/1dc4b695-3519-4b1f-b6f4-3b3a0944fbdd)


Tabelas e dados populados

![resultadossql](https://github.com/user-attachments/assets/5cfbe988-bb5c-4fef-a5f6-56c02026ced6)


# 👨‍💻 Dados do aluno
Nome: Pedro Henrique Bachiega
Curso: Desenvolvimento de Sistemas – 3º Semestre

Professor: Victor Icoma

Branch da entrega: avaliacao-PedroHBachiega

## 🧱 Estrutura da aplicação

```bash
📦 src
 ┣ 📂 Domain
 ┣ 📂 Application
 ┣ 📂 Infra
 ┃ ┣ 📂 Data
 ┃ ┃ ┣ 📂 Migrations
 ┃ ┃ ┣ 📂 Repositories
 ┃ ┃ ┗ 📂 EntityConfiguration
 ┗ 📂 WebAPI

