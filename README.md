### Стим
1 Берем юнити проект с версией 6000.2.9 взял версию из сим

2 Качаем в проект стим. Последняя версия dll из вкплей это 09.31.86.04 - это релиз 2025.161.0 
Скачать можно тут https://github.com/rlabrecque/Steamworks.NET/releases/tag/2025.161.0  
Тут как это работает https://documentation.vkplay.ru/p2p_vkp/p2p_steam_vkp

3 Заменяем dll в папке с игрой на вкплей. Скачать можно тут https://static.gc.vkplay.ru/guptools/steam_api_dlls.7z В проекте оригинальные лежат в папке `Assets/com.rlabrecque.steamworks.net/Plugins`

4 Добавляем в проект SteamManager из этого репо. Внизу TODO: что нужно поправить. В текущем проекте все работает. Билд сейчас лежит на вкплей

5 Теперь можно делать вызов `string authTicket = SteamManager.Instance.RequestAuthTicket();`

6 Слать тикет в ендпойнт /register_vk_play (дока тут https://test.platform-bpla.ru/apidoc)

7 Получать тикет авторизации или ошибку.

8 Наверно получить данные профиля

8 Сохранить тикет и при след запуске уже пропускать пункты выше

### Билд

1 Поменять Имя главного исполняемого файла (64 бита) на свой тут https://developers.vkplay.ru/dev/games/45976/distributions/

2 Сбилдить вин версию например в папку Builds/Win64

3 Скачать утилиту для сборки тут https://developers.vkplay.ru/dev/games/45976/distributions/

Я использую darwin
Моя команда для сборки

```
./bstool /Users/zloadmin/Projects/VKPlayTest/Builds/Win64_VK -src=/Users/zloadmin/Projects/VKPlayTest/Builds/Win_64/ -token=[TOKEN] -versioninfo=0.0.[VERSION]
```
1 Папка с билдом  /Users/zloadmin/Projects/VKPlayTest/Builds/Win_64

2 Пустая папка для сборки. (Ее нужно создать) /Users/zloadmin/Projects/VKPlayTest/Builds/Win64_VK

3 [TOKEN] - скопируй здесь https://developers.vkplay.ru/dev/games/45976/distributions/

4 [VERSION] - у меня последняя 11. Но тут думаю брать реальные версии сима


### TODO


* нужно поправить SteamManager
    * вся главная логика в Awake
    * нужно убрать упоминания TextDebugManager
    * нужно решить использовать ли Application.Quit(); или все таки выводить ошибку на подобии "перезапустите игру из вкплей"
    * метод RequestAuthTicket реализован так что он может возвращать пустой тикет когда не может его получить, возможно ты захочешь поменять эту логику
* я не стал писать логику сохранения тикета авторизации, думаю он уже реализован в симе
* я навайбкодил методы апи, думаю что в симе уже есть клиент
