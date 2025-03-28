# Отчёт по лабораторной работе #3

## Redis

* Обновлён [docker-compose](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Prefilter/docker-compose.yml) для добавления Redis.
* Добавлен [сервис](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Scripts/LuaEngine.Scripts/Services/Caching/RedisConnectionService.cs) подключения к Redis.
* Добавлен [сервис](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Scripts/LuaEngine.Scripts/Services/Caching/CacheService.cs) для базовых операций над кешем.
* Добавлен [репозиторий](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Scripts/LuaEngine.Scripts/Repositories/ScriptVersionCacheRepository.cs) с кешированием.
* Добавлен [метод расширения](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Scripts/LuaEngine.Scripts/Extensions/ServicesExtensions.cs) для регистрации сервисов для работы с Redis.

## GrayLog

* Обновлён [docker-compose](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Prefilter/docker-compose.yml) для добавления graylog, datanode и mongodb.
* В файле [Program.cs](https://github.com/MikoGH/DSS/blob/main/LuaEngine.Scripts/LuaEngine.Scripts/Program.cs) изменён способ логирования на GrayLog.

## CI-CD

Настроен [CI-CD](https://github.com/MikoGH/DSS/blob/main/.github/workflows/CI-CD.yml) процесс.
Включен автоматический прогон unit-тестов.
Автоматическое развёртывание происходит в DockerHub.

P.S. В docker-compose часть сервисов временно закомментирована, т.к. в них используются локальные NuGet пакеты, из-за чего в git сборка в CI-CD не может пройти, а разобраться что делать с локальными пакетами пока не получилось.
