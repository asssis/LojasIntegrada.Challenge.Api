# LojasIntegrada.Challenge.Api

## Rodando o Projeto

### Requisitos

Antes de iniciar verifique se o software necessários estão instalados e as portas estão livres.

* Docker
* Docker-Compose

### Portas
* 5000 => Porta da API
* 5001 => Porta do FRONTEND
* 1434 => Porta do BANCO DE DADOS

Esse projeto possui o docker compose para facilitar a seu deploy

`docker-compose up`

Para criar as migrations do projeto no banco rodar os comandos abaixo

`docker container exec api_backend dotnet tool install --global dotnet-ef --version 3.1`

`docker container exec api_backend dotnet-ef database update --project /app/LojasIntegrada.Challenge.DataBase`

Teste no Browse a API

![ArrayAllocation](https://github.com//asssis/LojasIntegrada.Challenge.Api/tree/main/docs/imagens/Imagem API.png?raw=true) 
![alt text](https://github.com//asssis/LojasIntegrada.Challenge.Api/tree/main/docs/imagens/Imagem API.png)
