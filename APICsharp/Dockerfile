# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar csproj e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código da aplicação
COPY . ./

# Criar o diretório para o banco de dados
RUN mkdir -p Data

# Publicar a aplicação
RUN dotnet publish -c Release -o /out

# Etapa 2: Construir a imagem de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /out .

# Criar o diretório para o banco de dados no container
RUN mkdir -p /app/Data

ENV ASPNETCORE_URLS=http://+:8080

# Expor a porta
# Porta precisa ser a mesma que a api esta escutando
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "APICSharpToDoList.dll"]
