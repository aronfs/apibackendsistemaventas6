# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar solución y proyectos para restaurar dependencias
COPY *.sln ./
COPY SistemaVenta.API/*.csproj ./SistemaVenta.API/
COPY SistemaVenta.BLL/*.csproj ./SistemaVenta.BLL/
COPY SistemaVenta.DAL/*.csproj ./SistemaVenta.DAL/
COPY SistemaVenta.DTO/*.csproj ./SistemaVenta.DTO/
COPY SistemaVenta.IOC/*.csproj ./SistemaVenta.IOC/
COPY SistemaVenta.Model/*.csproj ./SistemaVenta.Model/
COPY SistemaVenta.Utility/*.csproj ./SistemaVenta.Utility/

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
WORKDIR /app/SistemaVenta.API
RUN dotnet publish -c Release -o /app/out

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Exponer el puerto que se usará (ajustar si necesario)
EXPOSE 5185

# Variables de entorno (ajustar según tus necesidades)
ENV ASPNETCORE_URLS=http://+:5185
ENV ASPNETCORE_ENVIRONMENT=Production

# Copiar archivos publicados desde la etapa anterior
COPY --from=build /app/out ./

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]

