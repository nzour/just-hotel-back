### Инструкция для додиков (таких как я)

#### Стек разработчика:
* Рекомендуемая ОС - Linux
* Рекомендуемая IDE - Rider

#### Стэк технологий:
* JetBrains Rider
* Docker и Docker Compose
* .Net SDK (Если необходимо запускать приложение локально)
P.S. Для Rider это является крайней необходимостью на Linux, иначе не будет корректно работатть поддержка синтаксиса.

#### Встроенная конфигурация:
* Run ASPNET LOCAL - запустить .Net Core приложение локально (Требуется .Net SDK). Для корректной работы необходимо сначала запустить другие сервисы (смотреть ниже). P.S. Debugger будет работать.
* Run Other Services - запустить все остальные сервисы (БД и прочие) в докер контейнерах
* Run Full App REMOTE - запустить все приложение (со всеми сервисами) в докер контейнерах. P.S. Debugger умрет.

##### ВАЖНО: перед коммитом, необходимо сделать следующее:
* Выбрать главную директорию со всеми файлами.
* Нажать Ctrl + Alt + A, либо ПКМ -> Git -> Add. А еще лучше в настройках IDE выставить добавление под Git всех файлов по умолчанию.
* Можно спокойно комитить. Это было сделано для того, чтобы ни один файл не был упущен. .Net приложение живет своей жизнью и переодически плодит различные файла. Что некоторые из них из себя представляют я понятия не имею, но должно быть важные конфиги.

#### Переда началом работы необходимо:
* Скопировать скопировать файл `docker-compose.yml.dist` и под названием `docker-compose.yml`
* Провернуть то же самое с `docker-compose.override.yml.dist` -> `docker-compose.override.yml`
* Запустить сервисы, т.е. запустить конфигурацию `Run Other Services`
* Запустить pgadmin. localhost:8080 -> авторизуемся.
* По умолчанию логин - pgadmin@mail.db, пароль - 1234. Можно изменить в docker-compose.yml.
* Далее устанавливаем соединение с базой, которая в другом докер контейнере. Добавить новый сервер.
* Указываем любое имя. Далее -> Соединение: имя/адрес сервера - posgres, порт - 5432, служебная БД - zobor, пользователь и пароль - root. (Можно изменить, зависит от данных в docker-compose.yml)