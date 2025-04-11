#!/bin/bash
# Forzar cambio a /app y ejecutar correctamente el DLL

cd /app
echo "📦 Entrando a /app y lanzando SistemaVenta.API.dll"
exec dotnet SistemaVenta.API.dll
