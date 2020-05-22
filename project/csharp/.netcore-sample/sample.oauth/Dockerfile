FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /sln

# Copy csproj and restore as distinct layers
COPY foundation.core/foundation.core.* ./foundation.core/

COPY sample.oauth/sample.oauth.* ./sample.oauth/

RUN dotnet restore "./sample.oauth/sample.oauth.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet publish "./sample.oauth/sample.oauth.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app

COPY --from=build-env /sln/sample.oauth/out .

EXPOSE 5000
EXPOSE 80
ENTRYPOINT ["dotnet", "sample.oauth.dll"]
