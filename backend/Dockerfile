FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY ["src/FileProcessor.API/FileProcessor.API.csproj", "src/FileProcessor.API/"]
COPY ["src/FileProcessor.Application/FileProcessor.Application.csproj", "src/FileProcessor.Application/"]
COPY ["src/FileProcessor.Domain/FileProcessor.Domain.csproj", "src/FileProcessor.Domain/"]
COPY ["src/FileProcessor.Infra/FileProcessor.Infra.csproj", "src/FileProcessor.Infra/"]
COPY ["FileProcessor.sln", "./"]

RUN dotnet restore "FileProcessor.sln"

COPY . .

WORKDIR "/src/src/FileProcessor.API"
RUN dotnet publish "FileProcessor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FileProcessor.API.dll"]
