using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TextDebugManager : MonoBehaviour
{
    
    private Text errorText;
    private Text statusText;

    private string _splitter = Environment.NewLine;


    protected static TextDebugManager s_instance;

    public static TextDebugManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                return new GameObject("UserManager").AddComponent<TextDebugManager>();
            }
            else
            {
                return s_instance;
            }
        }
    }

    void Awake()
    {
        if (s_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this;
        s_instance.errorText = GameObject.Find("ErrorText").GetComponent<Text>();
        s_instance.statusText =  GameObject.Find("StatusText").GetComponent<Text>();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        errorText =  GameObject.Find("ErrorText").GetComponent<Text>();
        statusText =  GameObject.Find("StatusText").GetComponent<Text>();
        // if (errorText is not null) errorText.text = "";
        // if (statusText is not null) statusText.text = "";
    }

    public void addError(string msg, Object context)
    {
        Debug.LogError("User manager " + msg, context);
        if (errorText is not null)
        {
            errorText.text += _splitter + msg;
        }
    }

    public void addError(string msg)
    {
        Debug.LogError("User manager " + msg);
        if (errorText is not null)
        {
            errorText.text += _splitter + msg;
        }
    }

    public void addStatus(string msg)
    {
        Debug.Log("User manager " + msg);
        if (statusText is not null)
        {
            statusText.text += _splitter + msg;
        }
    }

    
}