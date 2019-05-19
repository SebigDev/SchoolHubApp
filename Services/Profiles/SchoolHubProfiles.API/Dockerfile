#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["Services/Profiles/SchoolHubProfiles.API/SchoolHubProfiles.API.csproj", "Services/Profiles/SchoolHubProfiles.API/"]
COPY ["Services/Profiles/SchoolHubProfiles.Application/SchoolHubProfiles.Application.csproj", "Services/Profiles/SchoolHubProfiles.Application/"]
COPY ["Services/Profiles/SchoolHubProfiles.Core/SchoolHubProfiles.Core.csproj", "Services/Profiles/SchoolHubProfiles.Core/"]
COPY ["BuildingBlocks/SchoolHub.Core/SchoolHub.Core.csproj", "BuildingBlocks/SchoolHub.Core/"]
COPY ["BuildingBlocks/SchoolHub.Data/SchoolHub.Data.csproj", "BuildingBlocks/SchoolHub.Data/"]
COPY ["BuildingBlocks/SchoolHub.Notification/SchoolHub.Notification.csproj", "BuildingBlocks/SchoolHub.Notification/"]
RUN dotnet restore "Services/Profiles/SchoolHubProfiles.API/SchoolHubProfiles.API.csproj"
COPY . .
WORKDIR "/src/Services/Profiles/SchoolHubProfiles.API"
RUN dotnet build "SchoolHubProfiles.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SchoolHubProfiles.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SchoolHubProfiles.API.dll"]