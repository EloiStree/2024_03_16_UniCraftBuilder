using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TriLibCore;
using TriLibCore.Samples;
using UnityEngine;
using UnityEngine.Events;

public class LoadModelFromUrlQuickTestListMono : MonoBehaviour
{

    
    public bool m_useImageExport;
    public string [] m_urlToload;

    public Transform m_parent;
    public float m_wait = 5;

    public Coroutine m_lastCoroutine;


    public bool m_autoStart=true;
    public string m_fromPathFolder;
    public string m_toPathFolderImage;
    public string m_toPathFolder;
    public string m_currentFilePath;
    public string m_lastPathSave;
    public MoveUntilCameraHaveAllInViewPortMono[] m_cameraMove;


    public RenderTextureToJpeg[] m_screenshotSource;
    [System.Serializable]
    public class RenderTextureToJpeg {

        public string m_typeName="_512x512";
        public RenderTexture m_texture;
    }
    private void Start()
    {
        if(m_autoStart)
         StartShootingScreenshot();
    }

    [ContextMenu("Stop")]
    public void StopShootingScreenshot() {
        if (m_lastCoroutine != null)
            StopCoroutine(m_lastCoroutine);
    }

    [ContextMenu("Start")]
    public void StartShootingScreenshot() {
        StopShootingScreenshot();
        StartCoroutine(LoadAll());
    }
    public bool m_waitForLoading;
    public bool m_waitForScreenshot;
    public IEnumerator LoadAll()
    {
        if (!m_useImageExport)
            yield return null;
        if (_assetLoaderOptions == null)
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions(false, true);
        }
        GenerateRelativeImageFile();
        for (int i = 0; i < m_urlToload.Length; i++)
        {
            if (!m_useImageExport)
                yield return null;

            m_currentFilePath = m_urlToload[i];

            var webRequest = AssetDownloader.CreateWebRequest(m_urlToload[i]);
            m_waitForScreenshot = true;
            m_waitForLoading = true;
            try {
                AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, m_parent.gameObject, _assetLoaderOptions);
            }
            catch (Exception e) { Debug.Log("E:" + e.StackTrace); }

            
            yield return new WaitWhile(()=> m_waitForLoading);
            yield return TakeScreenshots();
            yield return new WaitWhile(() => m_waitForScreenshot);
            yield return new WaitForSeconds(m_wait);
            yield return new WaitForEndOfFrame();
                for (int j = m_parent.childCount - 1; j >= 0; j--)
                {
                    Destroy(m_parent.GetChild(j).gameObject);
                }
            }
        
    }
    private void GenerateRelativeImageFile()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < m_urlToload.Length; i++)
        {
            string relativePath = m_urlToload[i].Replace(m_fromPathFolder, "").Trim(new char[] { '\\', '/' });
            foreach (var item in m_screenshotSource)
            {
                sb.AppendLine( relativePath + item.m_typeName + ".jpeg");
            }
        }
        Eloi.MetaAbsolutePathFile dir = new Eloi.MetaAbsolutePathFile(Path.Combine(m_toPathFolder, "relativeImagePathList.txt"));
        Eloi.E_FileAndFolderUtility.CreateFolderIfNotThere(dir);
        Eloi.E_FileAndFolderUtility.ExportByOverriding(dir, sb.ToString());
        

    }
    [Range(0,1f)]
    public float m_qualityPercent=0.3f;
    private IEnumerator TakeScreenshots()
    {
        MeshRenderer[] mesh = m_parent.GetComponentsInChildren<MeshRenderer>();

        string relativePath = m_currentFilePath.Replace(m_fromPathFolder, "").Trim(new char[] { '\\', '/' });
        foreach (var item in m_cameraMove)
        {
            item.SetMeshRenderer(mesh);
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        foreach (var item in m_screenshotSource)
        {
            string path = Path.Combine(m_toPathFolderImage, relativePath + item.m_typeName + ".jpeg");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in item.m_texture, out Texture2D image);
            Eloi.MetaAbsolutePathFile pathFile = new Eloi.MetaAbsolutePathFile(path);
            ExportTextureAsJPEG(pathFile, image, true, true, m_qualityPercent); ;
            m_lastPathSave = path;
            
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        m_waitForScreenshot = false;
    }

    /// <summary>
    /// Cached Asset Loader Options instance.
    /// </summary>
    private AssetLoaderOptions _assetLoaderOptions;

   

    /// <summary>
    /// Called when any error occurs.
    /// </summary>
    /// <param name="obj">The contextualized error, containing the original exception and the context passed to the method where the error was thrown.</param>
    private void OnError(IContextualizedError obj)
    {
        Debug.Log($"An error occurred while loading your Model: {obj.GetInnerException()}");
        m_waitForLoading = false;
        m_waitForScreenshot = false;
    }

    /// <summary>
    /// Called when the Model loading progress changes.
    /// </summary>
    /// <param name="assetLoaderContext">The context used to load the Model.</param>
    /// <param name="progress">The loading progress.</param>
    private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
        Debug.Log($"Loading Model. Progress: {progress:P}");
    }

    /// <summary>
    /// Called when the Model (including Textures and Materials) has been fully loaded.
    /// </summary>
    /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
    /// <param name="assetLoaderContext">The context used to load the Model.</param>
    private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {
        Debug.Log("Materials loaded. Model fully loaded.");
    }

    /// <summary>
    /// Called when the Model Meshes and hierarchy are loaded.
    /// </summary>
    /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
    /// <param name="assetLoaderContext">The context used to load the Model.</param>
    private void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        Debug.Log("Model loaded. Loading materials.");
        
        m_waitForLoading = false;


        //ScreenCapture.CaptureScreenshot(path, 1);



    }

    public static void ExportTextureAsJPEG(in IMetaAbsolutePathFileGet filePath, Texture2D t,
          bool mipmap, bool linear, float compressionPercent)
    {
       E_FileAndFolderUtility. CreateFolderIfNotThere(in filePath);
        byte[] _bytes = t.EncodeToJPG((int)(100f * compressionPercent));
        System.IO.File.WriteAllBytes(filePath.GetPath(), _bytes);
        //Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + filePath.GetPath());

    }
}