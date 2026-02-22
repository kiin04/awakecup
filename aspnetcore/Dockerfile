# Stage 1: Build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app
COPY *.sln ./
COPY aspnetcore/*.csproj ./aspnetcore/
RUN dotnet restore ./aspnetcore/YourProjectName.csproj

COPY . .
WORKDIR /app/aspnetcore
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/aspnetcore/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "aspnetcore.dll"]
