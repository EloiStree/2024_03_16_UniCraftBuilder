using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchScriptInputMono_ScreenPositionVector3 : AbstractScratchMono_VariableHolderVector3
{
    public Camera m_cameraToUse;
    public float m_depthDistanceOfCamera;
    [Header("Debug")]
    public Vector3 m_screenPosition;
    public int m_touchCount;

  

    public override Vector3 GetVector3()
    {
        m_screenPosition = Input.mousePosition;
        m_touchCount = Input.touchCount;
        if (Input.touchCount > 0)
            m_screenPosition = Input.touches[0].position;
        m_screenPosition.z = m_depthDistanceOfCamera;

        Vector3 worldPosition = m_cameraToUse.ScreenToWorldPoint(m_screenPosition);
        //worldPosition += m_cameraToUse.transform.forward * m_depthDistanceOfCamera;
        return worldPosition;

    }

    private void Reset()
    {
        m_cameraToUse = Camera.main;
    }
}
