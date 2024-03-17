using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchBroadcasterMono_UnityListener : MonoBehaviour, I_ScratchBroadcastListener
{
    
    public string m_listenToLabel;

    [Header("Debug")]

    public string m_lastReceivedBroadcast;
    public UnityEvent m_onReceived;
    public bool m_useTrim = true;
    public bool m_ignoreCase = true;


    private void OnEnable()
    {
        ScratchBroadcasterSingleton.AddBroadcastListener(this);

    }
    private void OnDisable()
    {
        ScratchBroadcasterSingleton.RemoveBroadcastListener(this);
    }
    private void OnDestroy()
    {
        ScratchBroadcasterSingleton.RemoveBroadcastListener(this);
    }

   

    private bool CheckValidityOfMessageAndTrigger(string broadcastMessage)
    {
        m_lastReceivedBroadcast = broadcastMessage;
        string label = m_listenToLabel;
        if (m_useTrim)
        {
            broadcastMessage = broadcastMessage.Trim();
            label = label.Trim();
        }
        if (m_ignoreCase)
        {
            broadcastMessage = broadcastMessage.ToLower();
            label = label.ToLower();
        }

        if (label == broadcastMessage)
        {
            LaunchSingleInstanceAction();
            return true;
        }
        return false;

    }

    public void LaunchSingleInstanceAction()
    {
        if(this.gameObject.activeInHierarchy)
            m_onReceived.Invoke();
    }

    public void OnBroadcastMessage(string broadcastMessage)
    {
        CheckValidityOfMessageAndTrigger(broadcastMessage);

    }
    public void OnBroadcastMessageWithStateRequest(string broadcastMessage, out I_BroadcastFeedBack isConsquenceStillActive)
    {
        if (CheckValidityOfMessageAndTrigger(broadcastMessage))
            isConsquenceStillActive = null;
        else isConsquenceStillActive = null;
    }


}