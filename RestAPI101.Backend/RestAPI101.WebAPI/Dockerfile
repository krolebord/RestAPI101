FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RestAPI101.WebAPI/RestAPI101.WebAPI.csproj", "RestAPI101.WebAPI/"]
RUN dotnet restore "RestAPI101.WebAPI/RestAPI101.WebAPI.csproj"
COPY . .
WORKDIR "/src/RestAPI101.WebAPI"
RUN dotnet build "RestAPI101.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestAPI101.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestAPI101.WebAPI.dll"]
