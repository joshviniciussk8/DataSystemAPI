# Sistema Web de Gerenciamento de Tarefas

## Descrição
Este projeto é um sistema web de gerenciamento de tarefas que se comunica com uma API RESTful para realizar operações CRUD (Criar, Ler, Atualizar, Deletar) de tarefas. A API foi construída utilizando **ASP.NET Core** com **Dapper** ou **Entity Framework Core** para a persistência de dados em um banco de dados **SQL Server**. O front-end da aplicação foi implementado com **ASP.NET MVC**, utilizando Razor views para renderizar as páginas.

## Funcionalidades da API

A API permite as seguintes operações para gerenciar tarefas:
- **Criar uma nova tarefa**.
- **Ler todas as tarefas ou uma tarefa específica** pelo `Id`.
- **Atualizar uma tarefa existente**.
- **Deletar uma tarefa**.

## Métodos
Requisições para a API devem seguir os padrões:
| Método | URL | Descrição |
|---|---|---|
| `GET` | https://localhost:7138/api/Tarefas/TodasTarefas  | Retorna informações de um ou mais Tarefas. |
| `GET` | https://localhost:7138/api/Tarefas/TarefaPorID/{id}  | Retorna informações de uma Tarefa pelo ID. |
| `POST`| https://localhost:7138/api/Tarefas/NovaTarefas | Utilizado para criar uma nova Tarefa através de um objeto Json. |
| `PUT` | https://localhost:7138/api/Tarefas/AlteraTarefa?Id={id} | Atualiza dados de uma tarefa ou altera sua situação através de um objeto Json e informando o id da tarefa. |
| `DELETE` |https://localhost:7138/api/Tarefas/DeletaTarefaPorID/1 | Remove uma Tarefa do sistema através do id. |

### Campos das Tarefas
Cada tarefa possui os seguintes campos:
- `Id` (int, auto-incrementado)
- `Título` (string, obrigatório, máximo 100 caracteres)
- `Descrição` (string, opcional)
- `Data de Criação` (DateTime, gerado automaticamente)
- `Data de Conclusão` (DateTime?, opcional)
- `Status` (enum: **Pendente**, **EmProgresso**, **Concluída**)

### Validações
- O campo **Título** é obrigatório e deve ter no máximo 100 caracteres.
- A **Data de Conclusão** não pode ser anterior à **Data de Criação**.

## Tecnologias Utilizadas
- **Backend/API**:
  - ASP.NET Core
  - Dapper
  - SQL Server
- **Frontend**:
  - ASP.NET MVC (Razor Pages)
  - HTML/CSS para layout
  - JavaScript para interações dinâmicas

## FrontEnd
- O sistema tem uma interface minimalista conforme imagem abaixo
- A tela principal lista todas as Tarefas, dando opção de edição, criação, deletar e Detalhar as tarefas
- Você também tem a opção de filtrar as tarefas através do status 

![image](https://github.com/user-attachments/assets/44a92ae9-dc4b-40b3-9a5b-bd475519aa5c)



## Requisitos

- .NET 8 ou superior
- SQL Server

## Configuração do Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/usuario/projeto-gerenciamento-tarefas.git
   cd projeto-gerenciamento-tarefas
2. Configuração do Banco de dados SQL Server
   ```Create Database DataSystem;
   use DataSystem;
   create table Tarefa (Id int not null Identity(1,1) primary key, Titulo nvarchar(100) NOT NULL, Descricao nvarchar(100) null ,DataCriacao DateTime, DataConclusao DateTime null, Status nvarchar(13));   
