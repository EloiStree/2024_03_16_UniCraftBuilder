using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LearnUrlMono_Keywords : A_ScratchBlockableMono
{
    [Tooltip("Allows to open several page of research with the keywords fill in it")]
    public string[] m_keywordsToOpen;

    public enum WhatToOpen { All, Google, Brave, Youtube, Udemy, Odysee}
    static string m_languageToLookFor = " FR ";

    [ContextMenu("Set Language to French")]
    public void SetLanguageToLookForFrench() { m_languageToLookFor = "FR"; }
    [ContextMenu("Set Language to English")]
    public void SetLanguageToLookForEnglish() { m_languageToLookFor = ""; }

    [ContextMenu("Open Page")]
    public void OpenPage()
    {
        GetUrlLists(out List<string> urls);
        foreach (var item in urls)
        {
            Application.OpenURL(item);
        }
    }

    public void GetUrlLists(out List<string> result) {
        result = new List<string>();
        foreach (var item in m_keywordsToOpen)
        {
            string w = item;
            w = (w + " " + m_languageToLookFor).Trim();
            AppendUrlToList(ref result, w);
        }

    }

    public void AppendUrlToList(ref List<string> result, string words) {

        result.Add(GetUrl("https://search.brave.com/videos?q={0}&source=web", words));
        result.Add(GetUrl("https://www.youtube.com/results?search_query={0}", words));
        result.Add(GetUrl("https://odysee.com/$/search?q={0}", words));
        result.Add(GetUrl("https://www.google.com/search?q={0}", words));
        result.Add(GetUrl("https://search.brave.com/search?q={0}&source=web", words));
        result.Add(GetUrl("https://search.brave.com/images?q={0}", words));
        result.Add(GetUrl("https://www.udemy.com/courses/search/?src=ukw&q={0}", words));
        
    }

    private string GetUrl(string urlFormat, string key)
    {
        return (string.Format(urlFormat, key.Trim().Replace(" ", "+")));
    }
    private void Open(string urlFormat, string key)
    {
        Application.OpenURL(GetUrl(urlFormat, key));
    }
    private void Open(string url)
    {
        Application.OpenURL(url);
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        OpenPage();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        OpenPage();
    }
}


