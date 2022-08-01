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
 
![alt text](https://github.com/asssis/LojasIntegrada.Challenge.Api/blob/main/docs/imagens/Imagem%20API.png?raw=true)

 
