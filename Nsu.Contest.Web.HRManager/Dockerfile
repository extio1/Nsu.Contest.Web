FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Nsu.Contest.Web.HRManager.csproj", "Nsu.Contest.Web.HRManager/"]
RUN dotnet restore "Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj"

COPY . .
WORKDIR "/src/Nsu.Contest.Web.HRManager"
RUN dotnet publish "Nsu.Contest.Web.HRManager.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Nsu.Contest.Web.HRManager.dll"]
