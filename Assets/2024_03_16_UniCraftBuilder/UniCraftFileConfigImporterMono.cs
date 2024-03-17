using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UniCraftDirectSettablePathMono : AbstractMetaAbsolutePathDirectoryMono
{
    public string m_path;
    public void SetPath(string path) {
        m_path = path;
    }
    public override void GetPath(out string path)
    {
        path = m_path;
    }

    public override string GetPath()
    {
        return m_path;
    }
}

public class UniCraftFileConfigImporterMono : MonoBehaviour
{
    public Eloi.AbstractMetaAbsolutePathFileMono m_configurationFile;
    public UnityEvent<string> m_fromPath;
    public UnityEvent<string> m_toPath;
    public UnityEvent<bool> m_generateImages;

    public string m_importedJson;
    public ConfigUniCraft m_importedConfig;
  

    [ContextMenu("Import")]
    public void Import()
    {
        Eloi.E_FileAndFolderUtility.CreateFolderIfNotThere(m_configurationFile);
        Eloi.E_FileAndFolderUtility.ImportOrCreateThenImport(out m_importedJson, m_configurationFile, FetchDefault);
        m_importedConfig= JsonUtility.FromJson< ConfigUniCraft>(m_importedJson);
        m_fromPath.Invoke(m_importedConfig.m_folderPathToMirror);
        m_toPath.Invoke(m_importedConfig.m_folderPathWhereToMirror);
        m_generateImages.Invoke(m_importedConfig.m_generateImage);
    }

    private void FetchDefault(out string fetch)
    {
        fetch =JsonUtility.ToJson(new ConfigUniCraft());
    }

    [System.Serializable]
    public class ConfigUniCraft
    {
        public string m_folderPathToMirror;
        public string m_folderPathWhereToMirror;
        public bool m_generateImage=true;
    }
}
