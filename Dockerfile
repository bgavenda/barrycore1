#FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
FROM microsoft/dotnet AS base
WORKDIR /app
EXPOSE 80

#FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
FROM microsoft/dotnet AS build
WORKDIR /src
COPY BarryCore1/BarryCore1.csproj BarryCore1/
RUN dotnet restore BarryCore1/BarryCore1.csproj
COPY . .
WORKDIR /src/BarryCore1
RUN dotnet build BarryCore1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BarryCore1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BarryCore1.dll"]
