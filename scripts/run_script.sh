#!/bin/bash
# rebuild-containers.sh
# Script Shell para remover e recriar contêineres Docker Compose

# Define o nome do projeto Docker Compose
PROJECT_NAME="udemy"

# Caminho para o arquivo docker-compose.yml
COMPOSE_FILE="../docker-compose.yml"

# Verifica se o arquivo docker-compose.yml existe
if [ ! -f "$COMPOSE_FILE" ]; then
    echo "Erro: Arquivo docker-compose.yml não encontrado no diretório atual."
    exit 1
fi

# Passo 1: Parar e remover os contêineres, redes e volumes (sem remover imagens)
echo "Parando e removendo os contêineres do projeto '$PROJECT_NAME'..."
docker-compose -p $PROJECT_NAME -f $COMPOSE_FILE down

# Passo 2: Recriar e iniciar os contêineres em modo destacado (detached)
echo "Recriando e iniciando os contêineres do projeto '$PROJECT_NAME'..."
docker-compose -p $PROJECT_NAME -f $COMPOSE_FILE up --build -d

# Passo 3: Verificar o status dos contêineres
echo "Verificando o status dos contêineres..."
docker-compose -p $PROJECT_NAME -f $COMPOSE_FILE ps

# Passo 4 (Opcional): Exibir os logs dos contêineres
# Uncomment the line abaixo se desejar ver os logs após a reinicialização
# docker-compose -p $PROJECT_NAME -f $COMPOSE_FILE logs -f
