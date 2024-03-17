using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BuildMarkdownMono_WeekFromScratchPostIt : MonoBehaviour
{


    public BuildMarkdownMono_LearnUrlMono m_day1Topic;
    public BuildMarkdownMono_LearnUrlMono[] m_day1Blocks;
    public BuildMarkdownMono_LearnUrlMono m_day2Topic;
    public BuildMarkdownMono_LearnUrlMono[] m_day2Blocks;
    public BuildMarkdownMono_LearnUrlMono m_day3Topic;
    public BuildMarkdownMono_LearnUrlMono[] m_day3Blocks;
    public BuildMarkdownMono_LearnUrlMono m_day4Topic;
    public BuildMarkdownMono_LearnUrlMono[] m_day4Blocks;
    public BuildMarkdownMono_LearnUrlMono m_day5Topic;
    public BuildMarkdownMono_LearnUrlMono[] m_day5Blocks;


    [ContextMenu("Set Clipboard with Markdown")]
    public void SetClipboardFromMarkdown() {


        GUIUtility.systemCopyBuffer = GetMarkdown();
    }

    public string GetMarkdown()
    {

        StringBuilder sb = new StringBuilder();
        GetMarkdown(ref sb, m_day1Topic, m_day1Blocks);
        GetMarkdown(ref sb, m_day2Topic, m_day2Blocks);
        GetMarkdown(ref sb, m_day3Topic, m_day3Blocks);
        GetMarkdown(ref sb, m_day4Topic, m_day4Blocks);
        GetMarkdown(ref sb, m_day5Topic, m_day5Blocks);
        return sb.ToString();
    }

    private void GetMarkdown(ref StringBuilder sb, BuildMarkdownMono_LearnUrlMono topic, params BuildMarkdownMono_LearnUrlMono[] blocks)
    {

        sb.Append("\n### Day topic\n\n");
        sb.Append(topic.GetMarkdown());
        sb.Append("\n\n");

        sb.Append("\n#### Blocks\n\n");
        sb.Append("\n\n");

        foreach (var item in blocks)
        {
            if (item != null) { 
                sb.Append(item.GetMarkdown());
                sb.Append("\n\n");
            }
        }
        sb.Append("\n-----------------\n");

    }
}
