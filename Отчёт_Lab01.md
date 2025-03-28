# Отчёт по лабораторной работе #0, #1 и #2

## Scripts API

* [Контекст БД](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Database/ScriptsContext.cs).

* [Модели](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Models/ProcessScript.cs) сущностей БД.

* Взаимодействие с БД postgres в сервисе Scripts API через [репозитории](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Repositories/ProcessScriptRepository.cs).

* Слой [сервисов](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Services/ProcessScriptService.cs) (пока кроме вызовов репозитория почти ничего не происходит, но потом появится логика, поэтому этот слой нужен).

* [Контроллеры](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Controllers/ProcessScriptController.cs).

* [Профили автомапперов](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Profiles/ProcessScriptProfile.cs).

* Регистрация зависимостей DI в [методах расширения](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Extensions/ServicesExtensions.cs).

* Подключение Swagger в [методах расширения](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts/Extensions/SwaggerExtensions.cs).

* [Unit-тесты](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts.Tests/Repositories/ProcessScriptRepositoryTests.cs) для репозиториев.

* Проект с [моделями ViewModel](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Scripts/LuaEngine.Scripts.Models/Script/ProcessScriptViewModel.cs) (чтобы собирать как Nuget-пакет, чтобы можно было использовать модельки в других микросервисах при взаимодействии).

## Prefilter API

* Настроено взаимодействие с сервисом Scripts API с помощью Refit:
    * [Refit клиенты](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/LuaEngine.Prefilter/Repositories/Abstractions/RefitClients/IProcessScriptClient.cs).
    * [Репозитории](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/LuaEngine.Prefilter/Repositories/ProcessScriptRepository.cs).

* Настроено взаимодействие с RabbitMq:
    * Настроено подключение к очереди сообщений RabbitMq в [методах расширения](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/LuaEngine.Prefilter/Extensions/RabbitMqExtensions.cs). 
    * Добавлен [консьюмер](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/LuaEngine.Prefilter/Services/DataConsumer.cs).
    * [Контроллер-продюсер](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/LuaEngine.Prefilter/Controllers/DataController.cs) - временный, только для проверки, что работает очередь, потом будет удалён.

## Docker

* Настроен [docker-compose](https://github.com/MikoGH/DSS/blob/Lab01/LuaEngine.Prefilter/docker-compose.yml) с 4 контейнерами:
    * PostgreSQL
    * RabbitMq
    * Scripts API
    * Prefilter API
