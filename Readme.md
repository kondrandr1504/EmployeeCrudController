## Локальный запуск

Перед локальным запуском убедитесь, что Docker установлен на компьютере.

```
$ docker --version
```

Скачать docker можно [здесь](https://www.docker.com/products/docker-desktop/)

### Запуск зависимостей (Postgres)

В корневой директории проекта запустить команды:

* `docker-compose build` - Сборка образов зависимостей
* `docker-compose up` - Запуск контейнеров
* `docker-compose down` - Остановка контейнеров
