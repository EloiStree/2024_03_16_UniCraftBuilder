using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BuildMarkdownMono_LearnUrlMono : MonoBehaviour
{

    public List<ScratchMono_PostItHolder> m_postIt = new List<ScratchMono_PostItHolder>();
    public List<LearnUrlMono_WebsiteURL> m_urls = new List<LearnUrlMono_WebsiteURL>();
    public List<LearnUrlMono_Keywords> m_keywords = new List<LearnUrlMono_Keywords>();

    public LabelToUrl[] m_labelsToUrl = new LabelToUrl[]{
        new LabelToUrl("Brave Video","https://search.brave.com/videos?q={0}&source=web"),
        new LabelToUrl("Youtube","https://www.youtube.com/results?search_query={0}"),
        new LabelToUrl("Odysee","https://odysee.com/$/search?q={0}"),
        new LabelToUrl("Google","https://www.google.com/search?q={0}"),
        new LabelToUrl("Brave","https://search.brave.com/search?q={0}&source=web"),
        new LabelToUrl("Brave Image","https://search.brave.com/images?q={0}"),
        new LabelToUrl("Udemy","https://www.udemy.com/courses/search/?src=ukw&q={0}  ")

    };

    [System.Serializable]
    public class LabelToUrl
    {
        public string m_keyLabel;
        public string m_urlLinked;

        public LabelToUrl(string keyLabel, string urlLinked)
        {
            m_keyLabel = keyLabel;
            m_urlLinked = urlLinked;
        }
    }


    public void Reset()
    {
        Refresh();
    }

    private void Refresh()
    {
        m_urls.Clear();
        this.gameObject.GetComponentsInChildren(m_postIt);
        this.gameObject.GetComponentsInChildren(m_urls);
        this.gameObject.GetComponentsInChildren(m_keywords);
    }
    [ContextMenu("Set Clipboard with Markdown")]
    public void SetClipboardFromMarkdown()
    {
        Refresh();

        GUIUtility.systemCopyBuffer = GetMarkdown();
    }

    public string GetMarkdown()
    {
        Refresh();
        StringBuilder sb = new StringBuilder();

        if (m_postIt.Count > 0 && m_postIt[0].m_postItText.Trim().Length > 0)
        {
            {
                sb.Append("\n## Post-it \n\n");
                foreach (var item in m_postIt)
                {
                    if (item != null)
                    {
                        if (item.m_postItText != null && item.m_postItText.Trim().Length > 0) { 

                            //sb.Append("### Content \n");
                            sb.Append($"{item.GetVariableAsString()}\n");
                        }
                    }

                }
            }
        }
        if (m_urls.Count > 0 && m_urls[0].m_urlToOpen.Trim().Length > 0)
        {
            sb.Append("\n## Urls \n\n");
            foreach (var item in m_urls)
            {
                if (item != null)
                {
                    if (item.m_urlToOpen != null && item.m_urlToOpen.Trim().Length > 0) {
                        if (item.m_urlDescription.Trim().Length > 0)
                        {
                            sb.Append($"\n- [{item.m_urlDescription}]({item.m_urlToOpen})" +
                                $"\n  - [{item.m_urlToOpen}]({item.m_urlToOpen})");
                        }
                        else {
                            sb.Append($"\n- [{item.m_urlToOpen}]({item.m_urlToOpen})");

                        }
                    }
                }

            }
        }
        if (m_keywords.Count > 0 && m_keywords[0].m_keywordsToOpen.Length > 0)
        {
            sb.Append("\n## Keywords \n\n");
            foreach (var item in m_keywords)
            {
                if (item != null)
                {
                    foreach (var itemKeys in item.m_keywordsToOpen)
                    {
                        if (itemKeys != null && itemKeys.Trim().Length > 0)
                        {
                            sb.Append($"\n- {itemKeys}:\n  - ");

                            foreach (var itemLabUrl in m_labelsToUrl)
                            {
                                GetUrlTextFor(ref sb, itemLabUrl.m_keyLabel, itemKeys, itemLabUrl.m_urlLinked);

                            }
                        }
                    }
                }
            }
        }
        return sb.ToString();

    }
    public void GetUrlTextFor(ref StringBuilder sb, string label, string words, params string[] urlFormatArray)
    {
        foreach (var item in urlFormatArray)
        {
            GetTextForUrlFormat(ref sb, label, item, words);
        }
    }
    public void GetTextForUrlFormat(ref StringBuilder sb, string label, string urlFormat, string words)
    {
        sb.Append($" [{label}]({string.Format(urlFormat, words.Replace(" ", "+"))}) ");
    }
}
