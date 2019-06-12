FROM ubuntu:latest

RUN apt-get update && apt-get install -y nodejs npm curl wget

# Нужен чтобы обновлять nodejs
RUN npm install -g n && n stable
# Установка ng-cli
RUN npm install -g @angular/cli@latest

USER root

WORKDIR /app

#CMD ["ng", "serve", "--port", "4201"]