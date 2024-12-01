В проекте используется локальная база данных для сохранения результатов расчетов и их просмотра.
Поэтому для корректной работы приложения на пк необходимо установить Microsoft SQL Server с официального сайта Microsoft SQL Server и SQL Server Management Studio SQL также с  официального сайта Microsoft.

После установки ПО:
1) Подключитесь к серверу с помощью SQL Server Management Studio (SSMS);
2) В папке программы откройте файл конфигурации (appsettings.json) и обновите строку подключения к базе данных:

"ConnectionStrings": { "KnapsackDBConnectionString": "Server=your-server-name;Database=backpack;Trusted_Connection=True;TrustServerCertificate=True;" }

Замените YOUR_SERVER_NAME на соответствующие значения вашего сервера базы данных и сохраните файл.

Кроме того, необходимо скачать .NET Framework 4.7.2 официальной страницы .NET и .NET 8.0 с официальной страницы .NET Core для корректной работы приложения.