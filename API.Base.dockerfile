FROM mcr.microsoft.com/dotnet/core/sdk:5.0.300-alpine3.13 AS build_env
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0.6-alpine3.13 AS run_env

FROM build_env
ENV PROJECT_NAME="API.Base.Api"
WORKDIR /app
COPY . .
RUN dotnet restore src/$PROJECT_NAME/$PROJECT_NAME.csproj
RUN dotnet publish src/$PROJECT_NAME/$PROJECT_NAME.csproj -c Release -o /app/publish

FROM run_env
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.Base.Api.dll"]