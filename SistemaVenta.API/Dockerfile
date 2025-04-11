# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "APISistemaVentas.sln"
RUN dotnet publish "SistemaVenta.API/SistemaVenta.API.csproj" -c Release -o /app/out

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 5185
ENV ASPNETCORE_URLS=http://+:5185
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SistemaVenta.API.dll"]
