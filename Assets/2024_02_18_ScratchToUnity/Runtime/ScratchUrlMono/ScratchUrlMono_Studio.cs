public class ScratchUrlMono_Studio : AbstractScratchUrlMono
    {
        public string m_label = "Studio Name";
        public string m_projectId = "34580905";

        public Category m_whatToOpen;
        public enum Category { StudioInfo, CommentaryOnStudio, CuratorOfStudio, WhatNewsOnStudio }
        string m_formatUrl = "https://scratch.mit.edu/studios/{0}/";
        string m_format_commentary = "https://scratch.mit.edu/studios/{0}/comments";
        string m_format_curators = "https://scratch.mit.edu/studios/{0}/curators";
        string m_format_activity = "https://scratch.mit.edu/studios/{0}/activity";

    public override string FetchUrl()
        {
        switch (m_whatToOpen)
        {
            case Category.CommentaryOnStudio:
                return string.Format(m_format_commentary, m_projectId);
            case Category.CuratorOfStudio:
                return string.Format(m_format_curators, m_projectId);
            case Category.WhatNewsOnStudio:
                return string.Format(m_format_activity, m_projectId);
            case Category.StudioInfo:
            default:
                return string.Format(m_formatUrl, m_projectId);
        }
  
        }

   
}



