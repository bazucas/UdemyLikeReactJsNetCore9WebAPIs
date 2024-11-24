# Etapa de build
FROM node:18-alpine AS build
WORKDIR /app

# Definir o ARG para receber a variável
ARG REACT_APP_API_URL

# Passar o valor da variável para o ambiente no momento da build
ENV REACT_APP_API_URL=$REACT_APP_API_URL

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

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
