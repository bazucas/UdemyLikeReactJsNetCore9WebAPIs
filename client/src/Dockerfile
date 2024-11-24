# frontend/Dockerfile

# Etapa de build
FROM node:18-alpine AS build
WORKDIR /app

# Copiar package.json e package-lock.json
COPY package*.json ./

# Instalar dependências
RUN npm install

# Copiar todo o código e construir o aplicativo
COPY . ./
RUN npm run build

# Etapa de runtime
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html

# Copiar arquivo de configuração do Nginx, se necessário
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
