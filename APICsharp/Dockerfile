# Use a imagem base do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie o csproj e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o restante do código e compile a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Use a imagem base do .NET Runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Defina a variável de ambiente para o ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

# Exponha a porta na qual a aplicação será executada
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "APICSharpToDoList.dll"]
