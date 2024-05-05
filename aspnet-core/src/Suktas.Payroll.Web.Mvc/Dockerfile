#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Suktas.Payroll.Web.Mvc/Suktas.Payroll.Web.Mvc.csproj", "src/Suktas.Payroll.Web.Mvc/"]
COPY ["src/Suktas.Payroll.Web.Core/Suktas.Payroll.Web.Core.csproj", "src/Suktas.Payroll.Web.Core/"]
COPY ["src/Suktas.Payroll.Application/Suktas.Payroll.Application.csproj", "src/Suktas.Payroll.Application/"]
COPY ["src/Suktas.Payroll.Application.Shared/Suktas.Payroll.Application.Shared.csproj", "src/Suktas.Payroll.Application.Shared/"]
COPY ["src/Suktas.Payroll.Core.Shared/Suktas.Payroll.Core.Shared.csproj", "src/Suktas.Payroll.Core.Shared/"]
COPY ["src/Suktas.Payroll.Core/Suktas.Payroll.Core.csproj", "src/Suktas.Payroll.Core/"]
COPY ["src/Suktas.Payroll.EntityFrameworkCore/Suktas.Payroll.EntityFrameworkCore.csproj", "src/Suktas.Payroll.EntityFrameworkCore/"]
COPY ["src/Suktas.Payroll.GraphQL/Suktas.Payroll.GraphQL.csproj", "src/Suktas.Payroll.GraphQL/"]
RUN dotnet restore "src/Suktas.Payroll.Web.Mvc/Suktas.Payroll.Web.Mvc.csproj"
COPY . .
WORKDIR "/src/src/Suktas.Payroll.Web.Mvc"
RUN dotnet build "Suktas.Payroll.Web.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Suktas.Payroll.Web.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Suktas.Payroll.Web.Mvc.dll"]
