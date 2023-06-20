#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/DigitalBank.Grpc/DigitalBank.Grpc.csproj", "src/DigitalBank.Grpc/"]
COPY ["src/DigitalBank.Core/DigitalBank.Core.csproj", "src/DigitalBank.Core/"]
COPY ["src/DigitalBank.Infraestructure/DigitalBank.Infraestructure.csproj", "src/DigitalBank.Infraestructure/"]
RUN dotnet restore "src/DigitalBank.Grpc/DigitalBank.Grpc.csproj"
COPY . .
WORKDIR "/src/src/DigitalBank.Grpc"
RUN dotnet build "DigitalBank.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigitalBank.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalBank.Grpc.dll"]
