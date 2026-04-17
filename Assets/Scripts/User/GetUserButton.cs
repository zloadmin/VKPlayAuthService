using System;
using Steamworks;
using UnityEngine;

public class GetUserButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickByButton()
    {
        // 1 Get Steam Auth Ticket
        // Если мы получаем пустой - то 2 варианта
        // 1 игра не может законектится с вкплей
        // 2 юзер запустил игру на прямую через экзешник  
        string authTicket = SteamManager.Instance.RequestAuthTicket();
        
        
    }
    
}
