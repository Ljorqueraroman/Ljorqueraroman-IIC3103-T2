
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY AirportsApi/*.csproj ./AirportsApi/
RUN dotnet restore

# copy everything else and build app
COPY AirportsApi/. ./AirportsApi/
WORKDIR /app/AirportsApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime
WORKDIR /app
COPY --from=build /app/AirportsApi/out .
EXPOSE 80
CMD ["dotnet", "AirportsApi.dll"]