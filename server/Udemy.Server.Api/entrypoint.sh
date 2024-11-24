#!/bin/bash

# entrypoint.sh

# Espera o SQL Server ficar disponível
echo "Aguardando o SQL Server iniciar..."
until /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "Udemy2025#$" -Q "SELECT 1" &> /dev/null
do
  echo "SQL Server ainda não está pronto. Aguardando..."
  sleep 1
done

echo "SQL Server está pronto. Aplicando migrações..."

# Aplica migrações do Entity Framework
dotnet ef database update --no-build

echo "Migrações aplicadas. Iniciando a aplicação..."

# Inicia a aplicação
dotnet Udemy.Server.Api.dll
