using System.Collections.Generic;
using System.Linq;

public static class ScratchBroadcasterSingleton {


    public static void BroadcastWithoutWaiting(string triggerLabel)
    {
        for (int i = 0; i < m_listeners.Count; i++)
        {
            if (i< m_listeners.Count   && m_listeners[i] != null)
                m_listeners[i].OnBroadcastMessage(triggerLabel);

        }
       
    }
    public static void BroadcastWithWaiting(string triggerLabel, out List<I_BroadcastFeedBack> feedbackLinks)
    {
        feedbackLinks = new List<I_BroadcastFeedBack>();

        for (int i = 0; i < m_listeners.Count; i++)
        {
            if (i < m_listeners.Count && m_listeners[i] != null)
            {
                m_listeners[i].OnBroadcastMessageWithStateRequest(triggerLabel, out I_BroadcastFeedBack feedbackLink);
                if (feedbackLink != null)
                    feedbackLinks.Add(feedbackLink);
            }

        }
     
    }

    public static List<I_ScratchBroadcastListener> m_listeners = new List<I_ScratchBroadcastListener>();

    public static void AddBroadcastListener(I_ScratchBroadcastListener listener)
    {
        if (m_listeners.Contains(listener))
            m_listeners.Remove(listener);
        m_listeners.Add(listener);
    }
    public static void RemoveBroadcastListener(I_ScratchBroadcastListener listener)
    {
        if (m_listeners.Contains(listener))
            m_listeners.Remove(listener);
    }
    public static void FlushNullListener()
    {
        m_listeners = m_listeners.Where(k => k != null).ToList();
    }
}
