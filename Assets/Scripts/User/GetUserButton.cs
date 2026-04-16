using Steamworks;
using UnityEngine;

public class GetUserButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickByButton()
    {
        TextDebugManager.Instance.addStatus("Start getting user");
        // 1 get auth ticket from steam (vkplay dll)
        /*
         * тут думаю просто отлавливать все ошибки и возвращать HAuthTicket.Invalid
         * и писать юзеру что не возможно подключится к вкплей попробуйте перезапустить игру или вкплей
         * что бы не городить проверки для каждого случая
         * как я понимаю их может быть много, ниже примеры
         * [*] The Steam client isn't running. A running Steam client is required to provide implementations of the various Steamworks interfaces.
         * [*] The Steam client couldn't determine the App ID of game. If you're running your application from the executable or debugger directly then you must have a [code-inline]steam_appid.txt[/code-inline] in your game directory next to the executable, with your app ID in it and nothing else. Steam will look for this file in the current working directory. If you are running your executable from a different directory you may need to relocate the [code-inline]steam_appid.txt[/code-inline] file.
         * [*] Your application is not running under the same OS user context as the Steam client, such as a different user or administration access level.
         * [*] Ensure that you own a license for the App ID on the currently active Steam account. Your game must show up in your Steam library.
         * [*] Your App ID is not completely set up, i.e. in Release State: Unavailable, or it's missing default packages.
         */
        byte[] pTicket = new byte[1024];
        
        HAuthTicket ticket = SteamManager.Instance.RequestAuthTicket(pTicket);
        if (ticket == HAuthTicket.Invalid)
        {
            TextDebugManager.Instance.addError("Invalid ticket");
            return;
        }

        string hex = System.BitConverter.ToString(pTicket, 0, pTicket.Length).Replace("-", "");
        TextDebugManager.Instance.addStatus("Steam ticket received " + hex);

        // 2 send to api tiket
        // тут думаю сделать так же, если не 200 код, то писать что ошибка подключения к серверу 
    }
}
