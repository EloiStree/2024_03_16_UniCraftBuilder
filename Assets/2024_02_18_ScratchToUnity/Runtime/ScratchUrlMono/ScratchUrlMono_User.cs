public class ScratchUrlMono_User : AbstractScratchUrlMono
{
    public string m_userName = "jaimelesfrites2501";
     string m_formatUrl = "https://scratch.mit.edu/users/{0}/";

   

    public override string FetchUrl()
    {
        return string.Format(m_formatUrl, m_userName);
    }
}



