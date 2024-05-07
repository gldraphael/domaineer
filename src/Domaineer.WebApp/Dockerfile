#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM alpine AS wget
#
#RUN mkdir /doge \
    #&& cd /doge \
    #&& wget -O doge https://github.com/Dj-Codeman/doge/releases/download/latest/doge_linux_64bit \
    #&& chmod +x doge

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# COPY --from=wget /doge/doge /doge/doge
EXPOSE 8080
EXPOSE 8081

FROM node as css
WORKDIR /stage
COPY ./src/Domaineer.WebApp/ .
RUN npm install \
    && npm run build:css \
    && mkdir -p /css \
    && cp wwwroot/dist/*.css /css

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /stage
COPY ["src/Domaineer.WebApp/Domaineer.WebApp.csproj", "stage/Domaineer.WebApp/"]
RUN dotnet restore "./stage/Domaineer.WebApp/Domaineer.WebApp.csproj"
COPY . .
WORKDIR "/stage/src/Domaineer.WebApp"
RUN dotnet build "./Domaineer.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Domaineer.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
COPY --from=css /css/* /app/publish/wwwroot/dist

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Domaineer.WebApp.dll"]
