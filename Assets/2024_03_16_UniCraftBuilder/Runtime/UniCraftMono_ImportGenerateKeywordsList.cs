using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eloi;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.IO;

public class UniCraftMono_ImportGenerateKeywordsList : MonoBehaviour
{

    public AbstractMetaAbsolutePathDirectoryMono m_absolutePathToMirrorFolder;
    public AbstractMetaAbsolutePathDirectoryMono m_absolutePathOfMirrorFolder;

    public string[] m_filesFound;
    public string[] m_filesFoundRelative;
    public string[] m_filesFoundRelativeBroken;
    public List<string> m_fileExtensions= new List<string>();

    public string[] m_replaceBySpaceToGenerateBroken = new string[] { "_", "\\", "/", "-", "." };

    [TextArea(0, 10)]
    public string m_allWordsInFilesRelativePath;
    [TextArea(0, 10)]
    public string m_fileExtensionsList="";

    [ContextMenu("Process and Reload Files")]
    public void ReloadFileAndProcess()
    {
        E_FileAndFolderUtility.GetAllfilesInAndInChildren(m_absolutePathToMirrorFolder, out m_filesFound);
        m_filesFoundRelative = new string[m_filesFound.Length];
        m_filesFoundRelativeBroken = new string[m_filesFound.Length];
        Process();
    }
    [ContextMenu("Process")]
    public void Process()
    {

        if (m_absolutePathOfMirrorFolder == null || m_absolutePathOfMirrorFolder.GetPath().Length < 1)
            return;
        if (m_absolutePathToMirrorFolder == null || m_absolutePathToMirrorFolder.GetPath().Length < 1)
            return;

        string pathFolder = m_absolutePathToMirrorFolder.GetPath();
        StringBuilder relativePathBuilder = new StringBuilder();
        for (int i = 0; i < m_filesFound.Length; i++)
        {
            m_filesFoundRelative[i] = m_filesFound[i].Replace(pathFolder, "").Trim(new char[] { '\\', '/', ' ' });
        }

        string relativeFolderPath = string.Join("\n", m_filesFoundRelative);
        for (int i = 0; i < m_filesFound.Length; i++)
        {
            m_filesFoundRelativeBroken[i] = m_filesFoundRelative[i];

            for (int j = 0; j < m_replaceBySpaceToGenerateBroken.Length; j++)
            {
                m_filesFoundRelativeBroken[i] = m_filesFoundRelativeBroken[i].Replace(m_replaceBySpaceToGenerateBroken[j], " ");
            }
            m_filesFoundRelativeBroken[i] = SplitCamelCase(m_filesFoundRelativeBroken[i]);
        }
        string allWordTextSplit = string.Join("\n", m_filesFoundRelativeBroken);
        GenerateListOfWord(allWordTextSplit, out m_allWordsInFilesRelativePath);
        GenerateListOfFileExtension(m_filesFoundRelative, out m_fileExtensionsList);
        OverrideText("relativeFolderPathList", relativeFolderPath);
        OverrideText("allWordsInRelativeFolderPathList", m_allWordsInFilesRelativePath);
        OverrideText("allFileExtensionsList", m_fileExtensionsList);
    }

    private void GenerateListOfFileExtension(string[] m_filesFoundRelative, out string m_fileExtensions)
    {
        Dictionary<string, int> fileExtensionCount = new Dictionary<string, int>();
        foreach (var item in m_filesFoundRelative)
        {
            int lastIndex = item.LastIndexOf(".");
            if (lastIndex < 0) continue;
            string ext = item.Substring(lastIndex);
            if (ext.IndexOf("\\") > -1 || ext.IndexOf("/") > -1)
                continue;
            //if (ext[0]=='.')
            //    continue;

            if (!fileExtensionCount.ContainsKey(ext))
            {
                fileExtensionCount.Add(ext, 1);

            }
            else fileExtensionCount[ext]++;
        }

        StringBuilder sb = new StringBuilder();
        fileExtensionCount = fileExtensionCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        foreach (var item in fileExtensionCount.Keys)
        {
            sb.Append(item);
            sb.Append(":");
            sb.Append(fileExtensionCount[item]);
            sb.AppendLine();
        }
        m_fileExtensions = sb.ToString();

    }

    private void OverrideText(string nameOfFile, string text)
    {
        IMetaAbsolutePathFileGet pathFile = Eloi.E_FileAndFolderUtility.Combine(m_absolutePathOfMirrorFolder, new MetaFileNameWithExtension(nameOfFile, "txt"));
        E_FileAndFolderUtility.ExportByOverriding(pathFile, text);
    }
    public static string RemoveNonAlphaNumeric(string input)
    {
        return Regex.Replace(input, @"[^a-zA-Z0-9]", " ");
    }
    private void GenerateListOfWord(string allWordTextSplit, out string allWords)
    {
        Dictionary<string, int>  wordsCount = new Dictionary<string, int>();
        allWordTextSplit = allWordTextSplit.Replace("\n", " ").ToLower();
        allWordTextSplit = RemoveNonAlphaNumeric(allWordTextSplit);
        while (allWordTextSplit.IndexOf("  ") > -1) {
            allWordTextSplit= allWordTextSplit.Replace("  ", " ");
        }
        foreach (var item in allWordTextSplit.Split(' ').Where(k => !int.TryParse(k, out _)))
        {

            if (!wordsCount.ContainsKey(item))
            {
                wordsCount.Add(item, 1);
            }
            else wordsCount[item]++;
        }

        StringBuilder sb = new StringBuilder();
        wordsCount = wordsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        foreach (var item in wordsCount.Keys)
        {
            sb.Append(item);
            sb.Append(":");
            sb.Append(wordsCount[item]);
            sb.AppendLine();
        }

        allWords = sb.ToString();

    }

    public static string SplitCamelCase( string str)
    {
        return Regex.Replace(
            Regex.Replace(
                str,
                @"(\P{Ll})(\P{Ll}\p{Ll})",
                "$1 $2"
            ),
            @"(\p{Ll})(\P{Ll})",
            "$1 $2"
        );
    }
}
