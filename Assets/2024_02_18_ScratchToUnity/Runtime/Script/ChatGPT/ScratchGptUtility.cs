using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScratchGptUtility 
{
    // Replace 'YOUR_API_KEY' with your actual OpenAI GPT-3 API key
     static string m_apiKey = "";
    private string openAIEndpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";

    internal static void SetTokenApiKey(string apKey)
    {
        m_apiKey = apKey;
    }

    public static string GetToken()
    {
        return m_apiKey;
    }
}
