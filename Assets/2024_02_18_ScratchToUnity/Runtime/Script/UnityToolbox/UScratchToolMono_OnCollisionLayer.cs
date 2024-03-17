using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UScratchToolMono_OnCollisionLayer : MonoBehaviour
{

    public bool m_isActive;
    public LayerMask m_allowCollision = ~1;
    public UnityEvent m_onCollisionFound;
    public void OnCollisionEnter(Collision collision)
    {

        if (Contains(m_allowCollision, collision.collider.gameObject)){
            NotifyCollisionDetected();
        }
    }

    private void NotifyCollisionDetected()
    {
        if (!m_isActive)
            return;
        m_onCollisionFound.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(m_allowCollision,other.gameObject ))
        {
            NotifyCollisionDetected();
        }
    }
    public static bool Contains( LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public static bool Contains( LayerMask mask, GameObject objectToCheck)
    {
        return mask == (mask | (1 << objectToCheck.layer));
    }
    
}
