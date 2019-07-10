FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

COPY . .

RUN apt-get update && apt-get -y install openssh-server supervisor
RUN sed -i 's/PermitRootLogin without-password/PermitRootLogin yes/' /etc/ssh/sshd_config
RUN sed 's@session\s*required\s*pam_loginuid.so@session optional pam_loginuid.so@g' -i /etc/pam.d/sshd

COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf

EXPOSE 5000/tcp
EXPOSE 5001/tcp

ENV ASPNETCORE_ENVIRONMENT=Development

CMD ["/usr/bin/supervisord"]