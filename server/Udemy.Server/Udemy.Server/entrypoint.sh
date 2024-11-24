#!/bin/bash

# entrypoint.sh

# Espera o SQL Server ficar disponível
echo "Aguardar o SQL Server iniciar..."
until /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT 1" &> /dev/null
do
  echo "SQL Server ainda não está pronto. Aguardar..."
  sleep 1
done

echo "SQL Server está pronto. Aplicar migrações..."

# Aplica migrações do Entity Framework
dotnet ef database update --no-build

echo "Migrações aplicadas. Iniciar a aplicação..."

# Inicia a aplicação
dotnet Udemy.Server.dll
