using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_PointInDirection : MonoBehaviour
{

    public float m_scratchClockwiseDirection = 90;
    public float m_verticalAxisAdjustement = 90;
    public Camera m_cameraToUse;
    public Transform m_whatToRotate;


    public Vector3 m_testEuler;
    [ContextMenu("Point in Direction")]
    public void PointInDirectionWithInspectorValue()
    {

        PointInDirectionWithGivenValue(m_scratchClockwiseDirection);
    }
    public void PointInDirectionWithGivenValue(float direction0To360Clockwise)
    {
        Quaternion cameraDirection = m_cameraToUse.transform.rotation;
        m_whatToRotate.rotation = (cameraDirection * Quaternion.Euler(0, m_verticalAxisAdjustement, 0));
        m_whatToRotate.RotateAround(m_whatToRotate.position, m_cameraToUse.transform.forward, -(direction0To360Clockwise-90));
    }
    private void Reset()
    {
        m_cameraToUse = Camera.main;
    }
}
