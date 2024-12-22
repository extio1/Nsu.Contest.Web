FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj", "Nsu.Contest.Web.HRManager/"]
COPY ["Nsu.Contest.Web.Common/Nsu.Contest.Web.Common.csproj", "Nsu.Contest.Web.Common/"]
RUN dotnet restore "Nsu.Contest.Web.HRManager/Nsu.Contest.Web.HRManager.csproj"

COPY . .
WORKDIR "/src/Nsu.Contest.Web.HRManager"
RUN dotnet build "Nsu.Contest.Web.HRManager.csproj" -c Release -o /app/build

RUN dotnet tool install --global dotnet-ef --version 7.0.10
ENV PATH="$PATH:/root/.dotnet/tools"


FROM build AS publish
RUN dotnet publish "Nsu.Contest.Web.HRManager.csproj" -c Release -o /app/publish


FROM base AS final
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt update
RUN apt install -y curl
ENTRYPOINT ["dotnet", "Nsu.Contest.Web.HRManager.dll"]