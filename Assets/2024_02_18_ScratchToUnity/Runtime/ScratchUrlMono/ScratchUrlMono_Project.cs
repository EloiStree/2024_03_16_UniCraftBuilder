using UnityEngine;

public class ScratchUrlMono_Project : AbstractScratchUrlMono
{
    public string m_label = "Project Name";
    public string m_projectId = "968405721";
    public bool m_openInsideCode = true;
    string m_formatUrl = "https://scratch.mit.edu/projects/{0}/";
    string m_formatUrlEditor = "https://scratch.mit.edu/projects/{0}/editor";

    public override string FetchUrl()
    {
        if(m_openInsideCode)
            return string.Format(m_formatUrlEditor, m_projectId);
        else 
            return string.Format(m_formatUrl, m_projectId);
    }

    [ContextMenu("Set GameObject Name with lable")]
    public void SetGameObjectNameWithLabel() {

        this.gameObject.name = m_label.Replace(":","|");
    }
}



