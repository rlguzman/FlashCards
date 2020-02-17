# FlashCard.Service :: service_dockerfile
## arguments
ARG DOTNET_VERSION=3.1

## stage - restore
FROM mcr.microsoft.com/dotnet/core/sdk:${DOTNET_VERSION} as restore
WORKDIR /src
COPY FlashCard.Data/*.csproj FlashCard.Data/
COPY FlashCard.Service/*.csproj FlashCard.Service/
RUN dotnet restore *.Service

## stage - publish
FROM restore as publish
RUN ls
COPY FlashCard.Data/ FlashCard.Data/
COPY FlashCard.Service/ FlashCard.Service/
RUN dotnet publish *.Service --configuration Release --no-restore --output /src/dist

## stage - deploy
FROM mcr.microsoft.com/dotnet/core/aspnet as deploy
WORKDIR /api
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
COPY --from=publish /src/dist/ ./
CMD ["dotnet", "FlashCard.Service.dll"]
