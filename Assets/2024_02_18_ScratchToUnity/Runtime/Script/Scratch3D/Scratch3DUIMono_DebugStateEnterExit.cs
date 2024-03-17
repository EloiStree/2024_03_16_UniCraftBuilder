using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scratch3DUIMono_DebugStateEnterExit : MonoBehaviour
{

    public bool m_notifyEnterNextUpdate;
    public bool m_notifyExitNextUpdate;
    public UnityEvent m_onEnterState;
    public UnityEvent m_onExitState;

    void Update()
    {
        if (m_notifyEnterNextUpdate) {
            m_notifyEnterNextUpdate = false;
            m_onEnterState.Invoke();
        }
        if (m_notifyExitNextUpdate) {
            m_notifyExitNextUpdate = false;
            m_onExitState.Invoke();
        }



    }

    public void NotifyEnterNextUpdate()
    {
        m_notifyEnterNextUpdate = true;
    }

    public void NotifyExitNextUpdate()
    {
        m_notifyExitNextUpdate = true;
    }
}
