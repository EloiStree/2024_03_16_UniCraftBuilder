using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScratchMono_GoToPosition : A_ScratchBlockableMono
{

    public Vector3 m_position;
    public Transform m_whatToMove;

    public void GoToValueInInspector() {
        
        GoToGivenValue(m_position);    
    }
    public void GoToGivenValue(Vector3 position) {
        m_whatToMove.position = position;
    }

    private void Reset()
    {
        m_whatToMove = this.transform;
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
