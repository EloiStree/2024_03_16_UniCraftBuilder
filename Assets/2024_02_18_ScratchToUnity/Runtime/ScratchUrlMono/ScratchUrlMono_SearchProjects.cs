public class ScratchUrlMono_SearchProjects : AbstractScratchUrlMono
{
    public string m_toSearch = "Science";
    public string m_formatUrl = "https://scratch.mit.edu/search/projects?q={0}";

    public override string FetchUrl()
    {
        return string.Format(m_formatUrl, m_toSearch);
    }
}



