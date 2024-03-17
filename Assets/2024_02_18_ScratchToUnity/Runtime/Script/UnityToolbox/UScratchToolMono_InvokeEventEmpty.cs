using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UScratchToolMono_InvokeEventEmpty : A_FunctionScratchBlockableMono
{

    public UnityEvent m_unityEvent;
    public bool m_ignoreException;
    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        if (m_ignoreException)
        {
            try
            {
                m_unityEvent.Invoke();
            }
            catch (Exception) { }
        }
        else {
            m_unityEvent.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
