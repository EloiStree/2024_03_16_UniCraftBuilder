using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UScratchConditionMono_AreTagInRadiusZone : AbstractScratchMono_ConditionHolder
{
    public string[] m_tagsName;
    public LayerMask m_allowCollision = ~1;
    public float m_radius = 0.5f;
    public Transform m_castCenter;
    public bool m_checkInRigidbodyChildrens;
    public bool m_lastCheck;
    public void Reset() { m_castCenter = this.transform; }


    public override bool IsConditionTrue()
    {
        Collider [] collider = Physics.OverlapSphere(m_castCenter.position, m_radius, m_allowCollision);

        if (collider.Length == 0)
            return false;

        List<ScratchTagsMono> tags = new List<ScratchTagsMono>();
        foreach (var item in collider)
        {
            item.GetComponents(tags);
        }
        foreach (var item in tags.Distinct())
        {
            if (item != null)
                if (item.ContainsOneTag(m_tagsName))
                    return true;

        }
        if (m_checkInRigidbodyChildrens) { 
            foreach (var item in collider)
            {
                if(item!=null && item.attachedRigidbody!=null)
                item.attachedRigidbody.GetComponentsInChildren(tags);
            }
            foreach (var item in tags.Distinct())
            {
                if (item != null)
                    if (item.ContainsOneTag(m_tagsName))
                        return true;

            }
        }
        
        return false;
    }
}