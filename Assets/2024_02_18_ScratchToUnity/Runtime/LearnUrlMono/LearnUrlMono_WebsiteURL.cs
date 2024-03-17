using System.Collections;
using UnityEngine;

public  class LearnUrlMono_WebsiteURL : A_ScratchBlockableMono
{
    public string m_urlDescription = "";
    [Tooltip("Url that will be open on your browser")]
    public string m_urlToOpen;

    [ContextMenu("Open Page")]
    public void OpenPage()
    {
        if(m_urlToOpen!=null && m_urlToOpen.Trim().Length>0)
        Application.OpenURL(m_urlToOpen);
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


