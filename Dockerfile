FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy solution and projects
COPY *.sln .
COPY src/PokeSpeare.Service/*.csproj ./src/PokeSpeare.Service/
COPY tests/PokeSpeare.Service.UnitTests/*.csproj ./tests/PokeSpeare.Service.UnitTests/
COPY tests/PokeSpeare.Service.IntegrationTests/*.csproj ./tests/PokeSpeare.Service.IntegrationTests/
RUN dotnet restore

# copy everything
COPY . .
RUN dotnet build

# run unit tests on demand using target flag
FROM build AS testrunner
WORKDIR /source/tests/PokeSpeare.Service.UnitTests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

# run unit tests
FROM build AS test
WORKDIR /source/tests/PokeSpeare.Service.UnitTests
RUN dotnet test

# run unit tests on demand using target flag
FROM build AS integrationtestrunner
WORKDIR /source/tests/PokeSpeare.Service.IntegrationTests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

# publish the app
FROM build as publish
WORKDIR /source/src/PokeSpeare.Service
RUN dotnet publish -c Release -o /out

# run the app
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS runtime
WORKDIR /source
COPY --from=publish /out ./
ENTRYPOINT ["dotnet", "PokeSpeare.Service.dll"]