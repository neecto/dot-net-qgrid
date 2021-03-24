FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /
COPY QGrid.sln /
COPY QGrid/ /QGrid
COPY QGrid.EntityFrameworkCore/ /QGrid.EntityFrameworkCore
COPY QGrid.Tests/ /QGrid.Tests

RUN dotnet restore QGrid.Tests/QGrid.Tests.csproj
RUN dotnet build QGrid.Tests/QGrid.Tests.csproj --no-restore -c Release

FROM build AS test
WORKDIR /QGrid.Tests