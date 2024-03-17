public class ScratchUrlMono_MyStuff : AbstractScratchUrlMono
{

    public WhatStuffToOpen m_whatToOpen;
    public enum WhatStuffToOpen { YourProjects, YourSharedProject, YourUnsharedProject, YourStudio, MyStuff}
    public string m_format_projects = "https://scratch.mit.edu/mystuff/#projects";
    public string m_format_shared = "https://scratch.mit.edu/mystuff/#shared";
    public string m_format_unshared = "https://scratch.mit.edu/mystuff/#unshared";
    public string m_format_studio = "https://scratch.mit.edu/mystuff/#galleries";
    public string m_format_mystuff = "https://scratch.mit.edu/mystuff/";

    public override string FetchUrl()
    {
        switch (m_whatToOpen)
        {
            case WhatStuffToOpen.YourProjects:
                return m_format_projects;
            case WhatStuffToOpen.YourSharedProject:
                return m_format_shared;
            case WhatStuffToOpen.YourUnsharedProject:
                return m_format_unshared;
            case WhatStuffToOpen.YourStudio:
                return m_format_studio;
            default:
                return m_format_mystuff;
        }
    }
}



