# YPermitin.ExternalDevices

Набор сервисов и приложений для управления различными устройствами.


## Обратная связь и новости

Вопросы, предложения и любую другую информацию [отправляйте на электронную почту](mailto:i.need.ypermitin@yandex.ru).

Новости по проектам или новым материалам в [Telegram-канале](https://t.me/TinyDevVault).

## Функциональность

В текущей версии доступны мобильное приложение и сервис для него.

Все компоненты пока в экспериментальном режиме и в разработке.

## Окружение для разработки

Для окружение разработчика необходимы:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/ru/vs/) - для разработки клиентского приложения на MAUI.
* [JetBrains Rider](https://www.jetbrains.com/ru-ru/rider/) - IDE для разработки составляющих библиотек, сервисов и приложений.

Visual Studio обязательна только для приложений на MAUI, остальные части можно разрабатывать как в Rider, так и в Visual Studio. При этом разработка приложения MAUI возможна только в Windows / MacOS.

## Состав проекта

Проекты и библиотеки в составе решения:

* Apps - клиентские приложения.
	* YPermitin.ExternalDevices.YPED - клиентское кросплатформенное приложение на [.NET MAUI](https://learn.microsoft.com/ru-ru/dotnet/maui/what-is-maui?view=net-maui-8.0) для Android / iOS, MacOS, Windows для управления устройствами и сервисами из проекта.
* Web - проекты API или других видов веб-приложений.
	* YPermitin.ExternalDevices.ManagementService - сервис управления устройством и других функций.
* Libs - библиотеки и вспомогательные проекты.
    * YPermitin.ExternalDevices.ManagementService.Client - клиентская библиотека для сервиса **YPermitin.ExternalDevices.ManagementService**.
    * YPermitin.ExternalDevices.NetworkUtils - вспомогательная библиотека для работы с сетью.
* Tests - модульные тесты и связанные проверки.
    * YPermitin.ExternalDevices.NetworkUtils.Tests - модульные тесты для библиотеки работы с сетью.
* Docs - документация и другая сопутствующая информация о проекте.

## Развертывание проекта

Развертывание приложения необходимо выполнять в среде *.nix. Для сервиса "YPermitin.ExternalDevices.ManagementService" необходим файл с настройками **appsettings.json**:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "DiscoveryInfo": {
    "ClientName": "YY-PI.local",
    "ClientPort": 5026 
  }
}
```

Раздел "DiscoveryInfo" предназначен для настройки параметров обнаружения, которые будут отображены в клиентских приложениях:

* **ClientName** - имя клиента.
* **ClientPort** - порт, по которому нужно обращаться к сервису. Вручную необходимо указывать, т.к. сам сервис может находится за прокси.

В остальном шаги публикации можно узнать в статье [Развертывание ASP.NET Core приложений на Ubuntu Linux](https://ypermitin.github.io/blog/.NET/2024-03/deploy-asp-net-core-on-linux/).

## Лицензия, благодарности и послесловие

Проект делается на чистом интересе и не преследует коммерческих целей. 

Публикуется под лицензией MIT, поэтому Вы можете использовать его у себя полностью или частично без каких-либо гарантий и полностью под Вашу ответственность.
