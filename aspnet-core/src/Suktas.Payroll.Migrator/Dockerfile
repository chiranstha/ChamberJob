#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Suktas.Payroll.Migrator/Suktas.Payroll.Migrator.csproj", "src/Suktas.Payroll.Migrator/"]
COPY ["src/Suktas.Payroll.EntityFrameworkCore/Suktas.Payroll.EntityFrameworkCore.csproj", "src/Suktas.Payroll.EntityFrameworkCore/"]
COPY ["src/Suktas.Payroll.Core/Suktas.Payroll.Core.csproj", "src/Suktas.Payroll.Core/"]
COPY ["src/Suktas.Payroll.Core.Shared/Suktas.Payroll.Core.Shared.csproj", "src/Suktas.Payroll.Core.Shared/"]
RUN dotnet restore "src/Suktas.Payroll.Migrator/Suktas.Payroll.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Suktas.Payroll.Migrator"
RUN dotnet build "Suktas.Payroll.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Suktas.Payroll.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Suktas.Payroll.Migrator.dll", "-s"]
