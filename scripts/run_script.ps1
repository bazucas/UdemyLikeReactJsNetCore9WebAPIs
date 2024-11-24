#Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
#Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

# rebuild-containers.ps1
# Script PowerShell para remover e recriar contêineres Docker Compose

# Define o nome do projeto Docker Compose
$projectName = "udemy"

# Caminho para o arquivo docker-compose.yml
$composeFilePath = "..\docker-compose.yml"

# Verifica se o arquivo docker-compose.yml existe
if (-Not (Test-Path $composeFilePath)) {
    Write-Error "Arquivo docker-compose.yml não encontrado no diretório atual."
    exit 1
}

# Passo 1: Parar e remover os contêineres, redes e volumes (sem remover imagens)
Write-Output "Parando e removendo os contêineres do projeto '$projectName'..."
docker-compose -p $projectName -f $composeFilePath down

# Passo 2: Recriar e iniciar os contêineres em modo destacado (detached)
Write-Output "Recriando e iniciando os contêineres do projeto '$projectName'..."
docker-compose -p $projectName -f $composeFilePath up --build -d

# Passo 3: Verificar o status dos contêineres
Write-Output "Verificando o status dos contêineres..."
docker-compose -p $projectName -f $composeFilePath ps

# Passo 4 (Opcional): Exibir os logs dos contêineres
# Uncomment the line abaixo se desejar ver os logs após a reinicialização
# docker-compose -p $projectName -f $composeFilePath logs -f
