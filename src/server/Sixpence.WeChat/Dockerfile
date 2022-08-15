#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Sixpence.WeChat/Sixpence.WeChat.csproj", "Sixpence.WeChat/"]
COPY ["Sixpence.Core/Sixpence.Core.csproj", "Sixpence.Core/"]
RUN dotnet restore "Sixpence.WeChat/Sixpence.WeChat.csproj"
COPY . .
WORKDIR "/src/Sixpence.WeChat"
RUN dotnet build "Sixpence.WeChat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sixpence.WeChat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sixpence.WeChat.dll"]