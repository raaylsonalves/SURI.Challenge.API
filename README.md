# 🔹 Suri - Integração para Emissão de Segunda Via de Boleto

## 📌 Sobre o Projeto
Este projeto é uma **integração** com o chatbot **Suri by Chatbot Maker**, permitindo que usuários de um **provedor de internet** solicitem a **segunda via de boletos** diretamente via conversa.

A aplicação foi desenvolvida em **.NET 9**, utilizando um arquivo **JSON** para armazenar dados e um **arquivo .http** para facilitar a execução de requisições.

## 🚀 Tecnologias Utilizadas
- **.NET 9** – Plataforma principal da API  
- **ASP.NET Core** – Framework para criação da API  
- **JSON** – Armazenamento de dados dos boletos  
- **.http** – Arquivo para testes de requisição no Visual Studio/Rider  
- **Chatbot Suri** – Plataforma de automação de conversa  

## 🔹 Como Funciona?
✔ O **chatbot Suri** recebe uma solicitação para **segunda via de boleto**  
✔ A **API em .NET 9** consulta um arquivo **JSON** para encontrar o boleto correspondente  
✔ Se encontrado, a API gera uma **URL acessível do boleto**  
✔ O chatbot **retorna o boleto PDF para o usuário via WhatsApp**  

## ⚙️ Instalação e Configuração
### 🔹 1. Clone o Repositório
```sh
git clone https://github.com/raaylsonalves/SURI.Challenge.API.git
cd SURI.Challenge
```
### 🔹 2. Executar o Projeto
Podendo ser executado no Visual Studio por linha de comando:
```sh
dotnet run
```

## 📝 Como Realizar Requisições
Dentro da pasta do projeto ou ao abrir a sln do projeto existe um arquivo .http que pode ser executado diretamente do Visual Studio.

```http
@SURI.Challenge.API_HostAddress = https://localhost:7243

### Get Invoices from json and send flowActionCapture to Suri

GET {{SURI.Challenge.API_HostAddress}}/invoices/79718866086
Accept: application/json

### Send Url PDF to Suri with flowAction

POST {{SURI.Challenge.API_HostAddress}}/invoices/send-pdf
Content-Type: application/json

{
  "id": 1,
  "cpf": "79718866086"
}

### Create Invoices inside invoices.json

POST {{SURI.Challenge.API_HostAddress}}/invoices
Content-Type: application/json

{
  "CPF": "79718866086",
  "Invoices": [
    {
      "Id": 3,
      "Url": "https://storage.googleapis.com/fic-invoice/pdfs/teste-0000001-1748037492.pdf",
      "Amount": "250.00",
      "DueDate": "20/06/2025"
    },
    {
      "Id": 4,
      "Url": "https://storage.googleapis.com/fic-invoice/pdfs/teste-0000001-1748037492.pdf",
      "Amount": "100.00",
      "DueDate": "20/06/2025"
    }
  ]
}
```

## 📂 Estrutura do Projeto
```
📦 SuriIntegration
 ┣ 📂 Controllers     # API para comunicação com o chatbot
 ┣ 📂 Services        # Lógica de recuperação de boletos
 ┣ 📂 Models         # Estruturas de dados (Invoices, CPF)
 ┣ 📂 Data           # Armazenamento em JSON
 ┣ 📜 SURI.Challenge.API.http  # Testes de requisição
 ┗ 📜 README.md      # Documentação do projeto
```
