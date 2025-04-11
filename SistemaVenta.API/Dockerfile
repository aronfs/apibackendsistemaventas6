# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ./APISistemaVentas.sln .
COPY ./SistemaVenta.API/SistemaVenta.API.csproj ./SistemaVenta.API/
COPY ./SistemaVenta.BLL/SistemaVenta.BLL.csproj ./SistemaVenta.BLL/
COPY ./SistemaVenta.DAL/SistemaVenta.DAL.csproj ./SistemaVenta.DAL/
COPY ./SistemaVenta.DTO/SistemaVenta.DTO.csproj ./SistemaVenta.DTO/
COPY ./SistemaVenta.IOC/SistemaVenta.IOC.csproj ./SistemaVenta.IOC/
COPY ./SistemaVenta.Model/SistemaVenta.Model.csproj ./SistemaVenta.Model/
COPY ./SistemaVenta.Utility/SistemaVenta.Utility.csproj ./SistemaVenta.Utility/

RUN dotnet restore
COPY . .

RUN dotnet publish SistemaVenta.API/SistemaVenta.API.csproj -c Release -o /app/out

# Etapa 2: Producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80

# Este es el secreto: usar sh para ejecutar el comando desde /app
CMD ["sh", "-c", "cd /app && dotnet SistemaVenta.API.dll"]
