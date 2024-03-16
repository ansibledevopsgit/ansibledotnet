FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /dotnet

# copy csproj and restore as distinct layers
COPY  *.sln .

COPY  WebApi/*.csproj ./WebApi/
RUN dotnet restore

# copy everything else and build app
COPY  WebApi/.   ./WebApi/
WORKDIR /dotnet/WebApi
RUN dotnet  publish  WebApi.csproj -c release     -o /out/



# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /out
COPY --from=build /out ./

EXPOSE 4000

ENTRYPOINT ["dotnet", "WebApi.dll"]
