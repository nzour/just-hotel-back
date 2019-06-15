FROM ubuntu:latest

ARG HOST_UID=1000
ARG HOST_GID=1000
ARG HOST_USER=host-user
ARG HOST_GROUP=host-group

RUN apt-get update && apt-get install -y gpg wget curl
RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
RUN mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
RUN sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-bionic-prod bionic main" > /etc/apt/sources.list.d/dotnetdev.list'
RUN wget -q https://packages.microsoft.com/config/ubuntu/19.04/prod.list
RUN mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
RUN apt-get install -y apt-transport-https
RUN apt-get update

RUN apt-get update && apt-get install -y dotnet-sdk-2.2

RUN groupadd -g $HOST_GID $HOST_GROUP && groupadd sudonopswd && \
    useradd -m -l -g $HOST_GROUP -u $HOST_UID $HOST_USER && \
    usermod -aG sudo $HOST_USER && usermod -aG sudonopswd $HOST_USER

USER host-user

WORKDIR /app

CMD ["dotnet", "run"]