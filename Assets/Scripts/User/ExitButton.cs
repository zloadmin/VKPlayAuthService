using System;
using Steamworks;
using UnityEngine;

public class ExitButtton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickByButton()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
    }
    
    
}
