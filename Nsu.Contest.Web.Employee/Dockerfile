FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Nsu.Contest.Web.Employee.csproj", "Nsu.Contest.Web.Employee/"]
RUN dotnet restore "Nsu.Contest.Web.Employee/Nsu.Contest.Web.Employee.csproj"
COPY . .
WORKDIR "/src/Nsu.Contest.Web.Employee"
RUN dotnet build "Nsu.Contest.Web.Employee.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nsu.Contest.Web.Employee.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nsu.Contest.Web.Employee.dll"]
