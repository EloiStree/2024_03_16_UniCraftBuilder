using UnityEngine;

public class ScratchMono_OnCollisionSphereOverlayer :   AbstractScratchMono_ConditionHolder
{

    public LayerMask m_allowCollision = ~1;
    public float m_radius = 0.5f;
    public Transform m_castCenter ;

    public bool m_lastCheck;
    public void Reset() { m_castCenter = this.transform; }


    public override bool IsConditionTrue()
    {
        m_lastCheck = Physics.OverlapSphere(m_castCenter.position, m_radius, m_allowCollision).Length > 0; ;
        return m_lastCheck;
    }
}