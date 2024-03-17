public class ScratchUrlMono_CreateNewProject : AbstractScratchUrlMono
{
    public string m_formatUrl = "https://scratch.mit.edu/projects/editor/";

    public override string FetchUrl()
    {
        return m_formatUrl;
    }
}



