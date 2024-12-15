FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Nsu.Contest.Web.Employee/Nsu.Contest.Web.Employee.csproj", "Nsu.Contest.Web.Employee/"]
COPY ["Nsu.Contest.Web.Common/Nsu.Contest.Web.Common.csproj", "Nsu.Contest.Web.Common/"]
COPY ["Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj", "Nsu.Contest.Web.HRManager/"]
RUN dotnet restore "Nsu.Contest.Web.Employee/Nsu.Contest.Web.Employee.csproj"
COPY . .
WORKDIR "/src/Nsu.Contest.Web.Employee"
RUN dotnet build "Nsu.Contest.Web.Employee.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nsu.Contest.Web.Employee.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Nsu.Contest.Web.Employee/Resources ./Resources
ENTRYPOINT ["dotnet", "Nsu.Contest.Web.Employee.dll"]
