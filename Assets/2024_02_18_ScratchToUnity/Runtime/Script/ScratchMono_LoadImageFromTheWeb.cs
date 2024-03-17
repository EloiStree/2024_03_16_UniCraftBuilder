using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;



public class ScratchMono_LoadImageFromTheWeb : AbstractScratchMono_LoadImageFrom
{
    public AbstractScratchMono_Texture2DImageHolder m_storeIn;
    public UnityEvent<Texture2D> m_onImageLoaded;
    public string m_imageUrl = "https://example.com/image.jpg";

    public bool m_isDownloading;
    public Texture m_debugDownloaded;

    public void SetImageToDownload(string imageUrl) {
        m_imageUrl = imageUrl;
    }
    public override IEnumerator LoadIfRequired()
    {
        yield return DownloadImage();
    }

    public override void LoadIfRequiredWithoutCoroutine()
    {
        //Not fan of that b
        StartCoroutine(DownloadImage());
    }
    public bool m_errorHappened=false;
    public bool m_showErrorAndException;
    IEnumerator DownloadImage()
    {
        if (string.IsNullOrWhiteSpace(m_imageUrl))
            yield return null;
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(m_imageUrl))
        {
            m_isDownloading = true;
            yield return www.SendWebRequest();
            m_isDownloading = false;

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                if (m_showErrorAndException) { 
                    Debug.LogError("Error: " + www.error);
                }
                    m_errorHappened = true;
                PushTexture(null);
            }
            else
            {
                PushTexture(DownloadHandlerTexture.GetContent(www));
            }
        }
    }

    private void PushTexture(Texture2D t)
    {
        if (m_storeIn)
            m_storeIn.SetWithTexture2D(t);
        m_onImageLoaded.Invoke(t);
        m_debugDownloaded = t;
    }
}

