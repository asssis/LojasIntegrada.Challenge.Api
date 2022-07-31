#!/bin/bash

dotnet tool install --global dotnet-ef --version 3.1
dotnet ef database update --project /app/LojasIntegrada.Challenge.Api