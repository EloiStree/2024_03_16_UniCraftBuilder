using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScratchBroadcasterSingletonFlagStartStop
{
     static StartActionListener m_onFlagStartTriggeredEvent;
     static StartActionListener m_onRedStopTriggeredEvent;


    public static void TriggerStartFlag() { if (m_onFlagStartTriggeredEvent!=null) m_onFlagStartTriggeredEvent(); }
    public static void TriggerRedStop() { if (m_onRedStopTriggeredEvent != null) m_onRedStopTriggeredEvent(); }

    public static void AddListenerStartFlag(StartActionListener toCallAtStartlag)
    {

        try
        {
            m_onFlagStartTriggeredEvent -= toCallAtStartlag;
        }
        catch (Exception) { }
        m_onFlagStartTriggeredEvent += toCallAtStartlag;
    }
    public static void RemoveListenerStartFlag(StartActionListener toCallAtStartlag)
    {

        try
        {
            m_onFlagStartTriggeredEvent -= toCallAtStartlag;
        }
        catch (Exception) { }
    }
    public static void AddListenerRedStop(StartActionListener toCallAtStartlag)
    {

        try
        {
            m_onRedStopTriggeredEvent -= toCallAtStartlag;
        }
        catch (Exception) { }
        m_onRedStopTriggeredEvent += toCallAtStartlag;
    }
    public static void RemoveListenerRedStop(StartActionListener toCallAtStartlag)
    {

        try
        {
            m_onRedStopTriggeredEvent -= toCallAtStartlag;
        }
        catch (Exception) { }
    }

    public delegate void StartActionListener();
}
