using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_PointTowards : A_ScratchBlockableMono
{

    public Transform m_whatToPointAt;
    public Transform m_whatToRotate;


    [ContextMenu("Point towards")]
    public void PointTowardsValueInInspector()
    {
        PointTowardsGivenValue(m_whatToPointAt);
    }
    public void PointTowardsGivenValue(Transform target)
    {

        PointTowardsGivenValue(target.position);
    }
    public void PointTowardsGivenValue(Vector3 point)
    {
        m_whatToRotate.rotation = Quaternion.LookRotation(point - m_whatToRotate.position, Vector3.up);
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        PointTowardsValueInInspector();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        PointTowardsValueInInspector();
    }
}
