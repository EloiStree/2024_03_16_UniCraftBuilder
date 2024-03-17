using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UScratchMono_SetTransformRotation : A_ScratchBlockableMono
{

    public Vector3 m_eulerRotation;
    public bool m_localRotation;
    public Transform m_whatToRotation
        ;

    public void GoToValueInInspector() {
        
        GoToGivenValue(m_eulerRotation);    
    }
    public void GoToGivenValue(Vector3 eulerRotation) {
        if(m_localRotation)
            m_whatToRotation.localRotation = Quaternion.Euler(eulerRotation);
        else
            m_whatToRotation.rotation = Quaternion.Euler(eulerRotation);
    }

    private void Reset()
    {
        m_whatToRotation = this.transform;
    }

    public  override IEnumerator  DoTheScratchableStuff()
    {
        GoToValueInInspector();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        GoToValueInInspector();
    }
}
