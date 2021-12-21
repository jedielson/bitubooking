# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .

# Copy the main source project files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copy the test project files
COPY tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file tests/${file%.*}/; done

RUN dotnet restore

# copy everything else and build app
COPY src/. ./src/
COPY tests/. ./tests/
RUN dotnet build BituBooking.sln

# publish api
FROM build AS publish-api
RUN dotnet publish ./src/BituBooking.Api/BituBooking.Api.csproj -c release -o /app --no-restore


# FROM build AS migrator
# COPY .config/. ./.config/
# RUN dotnet tool restore
# CMD ["make", "migration-database-update"]

# FROM build AS unit-tests
# ENV ASPNETCORE_ENVIRONMENT=e2e
# CMD ["make", "test-unit"]

# FROM build AS integration-tests
# ENV ASPNETCORE_ENVIRONMENT=e2e
# CMD ["dotnet", "test", "--no-restore", "./tests/BituBooking.IntegrationTests/BituBooking.IntegrationTests.csproj", "--configuration", "Release"]

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish-api /app ./
RUN rm appsettings.Development.json
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "BituBooking.Api.dll"]