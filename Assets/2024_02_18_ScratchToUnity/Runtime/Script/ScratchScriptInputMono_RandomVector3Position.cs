using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchScriptInputMono_RandomVector3Position : AbstractScratchMono_VariableHolderVector3
{

    public Transform m_center;
    public float m_radius = 0.5f;
    public RandomZoneType m_randomZonetype;

    public override Vector3 GetVector3()
    {
        if (m_randomZonetype == RandomZoneType.Cube)
            return m_center.position 
                + (m_center.right * GetRandomDistance())
                + (m_center.up * GetRandomDistance())
                + (m_center.forward * GetRandomDistance());

        if (m_randomZonetype == RandomZoneType.Sphere)
            return m_center.position +
                new Vector3(
                GetRandomDistance()
                , GetRandomDistance()
                , GetRandomDistance()).normalized * GetRandomDistance();

        return m_center.position;
    }

    private float GetRandomDistance()
    {
        return UnityEngine.Random.Range(-m_radius, m_radius);
    }

    public enum RandomZoneType { Cube, Sphere}



}
