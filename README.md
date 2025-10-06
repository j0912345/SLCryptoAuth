## Readme for kijetesantakalu's SLCryptoAuth fork
MelonLoader client and labAPI server mod for SCP: Secret Laboratory to implement functional and secure authentication that does not require a connection to central servers. This also fixes remote admin.

Written and tested on SCPSL v14.0.3-labapi-beta + MelonLoader v1.0.0-ci.2176 ALPHA Pre-Release.
 - in kijetesantakalu's testing 14.0.3 itself appears to be broken, though that's not SLCryptoAuth's fault.  

Use depot downloader to downolad these v14.0.3 manifests.  
Client manifest: 7041928528288606435  
Server manifest: 1429960657914648293

For each game version you need to replace dependencies and recompile the code.

My current goal with this fork is to port SLCryptoAuth to scopophobia and megapatch 2. Probably also some 13.x version(s), but only because somebody requested it and it's only one major version below 14 so it'll probably be a relatively easy place to start. I might port it to other stuff too but if I don't feel like running it on my server I likely won't bother.

## Acknowledgements

1. Some patches and/or parts of the code in this project were borrowed or based on work from the AxonSL project. The AxonSL project is licensed under the MIT license. The original copyright notice and AxonSL license text can be found here: https://github.com/AxonSL/Axon/blob/master/LICENSE.txt

2. While apparently not used for the original project, kijetesantakalu is using [SCPSL-ModPatch](https://github.com/hopperlopip/SCPSL-ModPatch) to remove the client's anti-cheat, which allows melonloader to run. https://github.com/hopperlopip/SCPSL-ModPatch

(the english version of this readme was machine translated from russian with deepl.com and then largely edited)
<hr>

## Original russian readme (which kijetesantakalu can't read)
MelonLoader модификация на SCP: Secret Laboratory для реализации рабочей и безопасной аутентификации, не требующей подключения к центральным серверам.

Было написано и протестировано на версии SCPSL v14.0.3-labapi-beta + MelonLoader v1.0.0-ci.2176 ALPHA Pre-Release.

SteamDepotsDownloader:
Манифест клиента: 7041928528288606435
Манифест сервера: 1429960657914648293

Для каждой версии игры нужно заменять зависимости и пересобирать код.

В будущем планирую провести рефакторинг кода, написать документацию, оформить репозиторий, настроить GitHub Actions и предоставить готовые к использованию релизу.

## Заимствования и Благодарности (Acknowledgements)

Некоторые патчи и/или части кода в этом проекте были позаимствованы или основаны на работе из проекта [AxonSL](https://github.com/AxonSL/Axon).
Проект AxonSL лицензирован на условиях лицензии MIT. Оригинальное уведомление об авторских правах и текст лицензии AxonSL можно найти здесь:
[https://github.com/AxonSL/Axon/blob/master/LICENSE.txt](https://github.com/AxonSL/Axon/blob/master/LICENSE.txt)
