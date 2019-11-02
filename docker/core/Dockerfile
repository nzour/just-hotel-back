FROM mcr.microsoft.com/dotnet/core/sdk:3.0-bionic

WORKDIR /app/src/root

RUN apt-get update && apt-get -y install \
    htop nano sudo

CMD ["dotnet", "run"]
