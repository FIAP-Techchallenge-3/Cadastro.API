FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Cadastro.API/Cadastro.API.csproj", "Cadastro.API/"]
RUN dotnet restore "Cadastro.API/Cadastro.API.csproj"
COPY . .
WORKDIR "/src/Cadastro.API"
RUN dotnet build "Cadastro.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cadastro.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cadastro.API.dll"]
