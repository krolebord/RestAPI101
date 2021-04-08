FROM mcr.microsoft.com/dotnet/sdk AS build
RUN mkdir app
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet AS runtime
WORKDIR /source/app
COPY --from=build /app .
ENTRYPOINT ["ls", "."]