using System;
using Steamworks;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;

public class GetUserButton : MonoBehaviour
{
    
    [System.Serializable]
    public class AuthToken {
        public string token;
    }
    public class ErrorResponse {
        public string error;
    }

    private string accessToken = "JwbYv4WYnr8cGBBMot1UaGg8EQvrKkdyG6uhjj5dlhzXu7RcTzACPf9X0SRJa6L04bUzhbMydYUCklj7dlOzVsSrgCKskuypY0ttfDdfDDfcW0KUee1sHemofz1HJ2fO3x80SDwgb6TgjnziThBnhqy7oTcUY15YQp2m0b3dTX8GCp0uPFhY8KkuwOISgrMCZdVtlMWSUw2YWy4Q3cphSgb04XfwwLYlyyVTrO9SMe0WGakihUGTqVk0adbVJW5";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public async void OnClickByButton()
    {
        // 1 Get Steam Auth Ticket
        // Если мы получаем пустой - то 2 варианта
        // 1 игра не может законектиться с вкплей
        // 2 юзер запустил игру на прямую через экзешник 
        string authTicket = SteamManager.Instance.RequestAuthTicket();
        if (authTicket == string.Empty)
        {
            // Show error cannot connect to vkplay or user run game by exe file (not from vkplay)
            return;
        }
        string userName = SteamManager.Instance.GetPersonaName(); 
        
        string token = await GetAuthToken(authTicket, userName);
        if (token != string.Empty)
        {
            TextDebugManager.Instance.addStatus("Received token " + token);
            string profileString = await GetProfile(token);
            TextDebugManager.Instance.addStatus("Profile data " + profileString);
        }
        else
        {
            TextDebugManager.Instance.addStatus("Received token empty");
        }
        
        
    }

    public async Task<string> GetAuthToken(string authTicket, string userName)
    {
        
        using var client = new HttpClient();
        TextDebugManager.Instance.addStatus("Send request to server");
        var request = new HttpRequestMessage(HttpMethod.Post, "https://test.platform-bpla.ru/api/v1/register_vk_play");
        request.Headers.Add("x-app-access-token", accessToken);
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(authTicket), "auth_ticket");
        content.Add(new StringContent(userName), "user_name");
        request.Content = content;

        try 
        {
            TextDebugManager.Instance.addStatus("Waiting response from server");
            var response = await client.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                
                TextDebugManager.Instance.addStatus("Response from server: " + jsonString.Substring(0, 5) + "...");
                AuthToken authToken = JsonUtility.FromJson<AuthToken>(jsonString);
                return authToken.token;
            }
            else
            {
                ErrorResponse error = JsonUtility.FromJson<ErrorResponse>(jsonString);
                TextDebugManager.Instance.addError(error.error);
                TextDebugManager.Instance.addError("Response code: " + response.StatusCode);
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            
            TextDebugManager.Instance.addError("Error: " + ex.Message);
        }

        return string.Empty;
    }
    public async Task<string> GetProfile(string authToken)
    {
        
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://test.platform-bpla.ru/api/v1/profile");
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("x-app-access-token", accessToken);
        request.Headers.Add("Authorization", "Bearer " + authToken);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    
    
}
