FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoFinStrategy.API/GoFinStrategy.API.csproj", "GoFinStrategy.API/"]
COPY ["GoFinStrategy.IoC/GoFinStrategy.IoC.csproj", "GoFinStrategy.IoC/"]
COPY ["GoFinStrategy.Application/GoFinStrategy.Application.csproj", "GoFinStrategy.Application/"]
COPY ["GoFinStrategy.Domain/GoFinStrategy.Domain.csproj", "GoFinStrategy.Domain/"]
COPY ["GoFinStrategy.CrossCutting.Shared/GoFinStrategy.CrossCutting.Shared.csproj", "GoFinStrategy.CrossCutting.Shared/"]
COPY ["GoFinStrategy.Infrastructure.Data.Postgresql/GoFinStrategy.Infrastructure.Data.Postgresql.csproj", "GoFinStrategy.Infrastructure.Data.Postgresql/"]
RUN dotnet restore "GoFinStrategy.API/GoFinStrategy.API.csproj"
COPY . .
WORKDIR "/src/GoFinStrategy.API"
RUN dotnet build "GoFinStrategy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoFinStrategy.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m gofin
USER gofin

CMD ASPNETCORE_ENVIRONMENT="Development" ASPNETCORE_URLS="http://*:$PORT" dotnet GoFinStrategy.API.dll
