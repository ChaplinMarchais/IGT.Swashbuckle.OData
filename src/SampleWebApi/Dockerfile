FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SampleWebApi/IGT.Swashbuckle.OData.SampleWebApi.csproj", "src/SampleWebApi/"]
RUN dotnet restore "src/SampleWebApi/IGT.Swashbuckle.OData.SampleWebApi.csproj"
COPY . .
WORKDIR "/src/src/SampleWebApi"
RUN dotnet build "IGT.Swashbuckle.OData.SampleWebApi.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet run "IGT.Swashbuckle.OData.SampleWebApi.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IGT.Swashbuckle.OData.SampleWebApi.dll"]
