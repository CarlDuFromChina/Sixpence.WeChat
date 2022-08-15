#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Sixpence.Core/Sixpence.Core.csproj", "Sixpence.Core/"]
COPY ["Sixpence.WeChat/Sixpence.WeChat.csproj", "Sixpence.WeChat/"]
RUN dotnet restore "Sixpence.WeChat/Sixpence.WeChat.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Sixpence.WeChat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sixpence.WeChat.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sixpence.WeChat.dll", "--server.urls","http://*:5000"]