using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_TurnLeftRight :  A_ScratchBlockableMono
{
        
    public float m_valueInAngle;
    public RotationType m_rotationWanted = RotationType.Right;
    public enum RotationType { Left, Right}
    public Transform m_whatToRotate;

    [ContextMenu("Rotate")]
    public void RotateWithInspectorValue()
    {
        RotateWithGivenValue(m_valueInAngle);
    }
    public void RotateWithGivenValue(float valueInAngle)
    {
        if (m_rotationWanted == RotationType.Right)
            m_whatToRotate.Rotate(new Vector3(0, valueInAngle, 0), Space.Self);
        if (m_rotationWanted == RotationType.Left)
            m_whatToRotate.Rotate(new Vector3(0, -valueInAngle, 0), Space.Self);
    }
    public void Reset()
    {
        m_whatToRotate = this.transform;
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        RotateWithInspectorValue();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        RotateWithInspectorValue();
    }
}
