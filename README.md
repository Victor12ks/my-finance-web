# MyFinanceWeb

Este é um projeto para o trabalho final da disciplina de PRÁTICAS DE IMPLEMENTAÇÃO E EVOLUÇÃO DE SOFTWARE - Online - ESFW - O6 - T1 que utiliza React com Vite para o frontend e ASP.NET Core 8 para o backend

## Pré-requisitos

Certifique-se de ter instalado as seguintes ferramentas:

- [Node.js](https://nodejs.org/) (versão 14 ou superior)
- [npm](https://www.npmjs.com/) (geralmente instalado com o Node.js)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Estrutura do Projeto

O projeto possui a seguinte estrutura de diretórios:

MyFinanceWeb
│
├── app
│ └── myfinanceweb.client # Diretório do frontend (React + Vite)
│
└── api
└── MyFinanceWeb.Server # Diretório do backend (ASP.NET Core 8)

## Configuração da Aplicação

1. Navegue até o diretório do frontend:

   ```bash
   cd app/myfinanceweb.client
   ```
   Instale as depêndencias node:
   ```bash
   npm install
   ```

3. Navegue até o diretório da API:

   ```bash
   cd api/MyFinanceWeb.Server
   ```
   Instale as depêndencias .net:
   ```bash
   dotnet restore
   ```
   Depois realize um build

   ```bash
   dotnet build
   ```
   Inicie a Aplicação:

   ```bash
   dotnet run dev
   ```

### Uma alternativa para execução é carregar a solution no Visual Studio, realizar um build, Configurar a solution para realizar o start com 2 projetos diferentes (.MyFinanceWeb.Server e myfinanceweb.client) e iniciar a execução

## Execução

A aplicação está configurada para executar nas seguintes portas https://localhost:7293 e http://localhost:5139

## Banco de dados

- Banco de dados foi criado em Sql Server

O script de execução do banco de dados está na pasta raiz desse repositório

Caso necessite alterar a connection string vá até o arquivo: ..\api\MyFinanceWeb.Server\appsettings.json

