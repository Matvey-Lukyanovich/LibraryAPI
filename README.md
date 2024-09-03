1. Запускаем base.sql скрипт(MySQL. При тестировании использовал MySQL Workbench 8.0 ). Создается база данных.

2. Далее запуская VS(2022, .NET 7.0) и LibraryAPI.sln. 

!!!!LibraryAPI/appsettings.json 
-- Указываем данные для подключение к БД(адресс, название_БД, Имя_пользователя, пароль)
-- Тестировалось на IIS Express

Открытие командной строки  PowerShell из Visual Studio:
В Visual Studio откройте "Панель инструментов".
Выберите "Tools" (Инструменты) > "Command Line" (Командная строка) > "Developer PowerShell".


3. Делаем миграцию:
PS C:\....\Test-Task\LibraryAPI> dotnet ef migrations add InitialCreate
PS C:\....\Test-Task\LibraryAPI> dotnet ef database update

*если не сработало то возможно поставить Entity Framework Core
*вписываем:
*dotnet tool install --global dotnet-ef --version 7.*
*устанавливает Entity Framework Core*
*dotnet add package Microsoft.EntityFrameworkCore.Design*


4. После этого в БД у нас появляются таблицы и поля.


5. Запускаем проект (http://localhost:5000/). Запускается интерфейс Swagger


6. Далее запускаем Postman и импортируем "AuthorAPI.postman_collection.json" и "BookAPI.postman_collection.json". Там написанные обращение к API и описание запросов (см."Postman.png")


7. Так же написан небольшой клиент на React. запускаем из католога "library-client" команду "npm start" (LibraryAPI.sln тоже должен быть запущен)

PS C:\...\Test-Task> cd library-client
PS C:\...\Test-Task\library-client> npm start        

--в клиенте: формы добавление автора и книг. Отобржение и удалене с диалоговым окном


-----------
для Связи
https://t.me/matveylukyanovich