FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj", "Nsu.Contest.Web.HRManager/"]
COPY ["Nsu.Contest.Web.Common/Nsu.Contest.Web.Common.csproj", "Nsu.Contest.Web.Common/"]
RUN dotnet restore "Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj"

COPY . .
WORKDIR "/src/Nsu.Contest.Web.HRManager"
RUN dotnet publish "Nsu.Contest.Web.HRManager.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "run", "--project", "/srs/Nsu.Contest.Web.HRManager"]
