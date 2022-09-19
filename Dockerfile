# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Test/Test.csproj" --disable-parallel
RUN dotnet publish "./Test/Test.csproj" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Test.dll"]