using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadTriLibCompatibleFilesInfolderMono : MonoBehaviour
{

    public LoadModelFromUrlQuickTestListMono m_screenshotMono;
    public AbstractMetaAbsolutePathDirectoryMono m_absolutePathFromMirrorFolder;
    public AbstractMetaAbsolutePathDirectoryMono m_absolutePathToMirrorFolder;

    public string[] files;
    [ContextMenu("Load files")]
    public void LoadFiles() {

         files = Directory.GetFiles( m_absolutePathFromMirrorFolder.GetPath(),"*",SearchOption.AllDirectories);
        string[] supportedExtensions = { ".fbx", ".obj", ".gltf", ".glb", ".stl", ".ply", ".3mf", ".dae" };

        List<string> list = new List<string>();
        foreach (var file in files)
        {
            foreach (var item in supportedExtensions)
            {
                if (file.ToLower().EndsWith(item.ToLower()))
                {
                    list.Add(file);
                    break;
                }
               
            }
        }
        m_screenshotMono.m_fromPathFolder = m_absolutePathFromMirrorFolder.GetPath();
        m_screenshotMono.m_toPathFolderImage = Path.Combine(m_absolutePathToMirrorFolder.GetPath(), "Image");
        m_screenshotMono.m_toPathFolder= (m_absolutePathToMirrorFolder.GetPath());
        m_screenshotMono.m_urlToload = list.ToArray();
      //  m_screenshotMono.StartShootingScreenshot();

    }
}
