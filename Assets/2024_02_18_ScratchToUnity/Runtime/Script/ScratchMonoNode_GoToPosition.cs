using UnityEngine;

public class ScratchMonoNode_GoToPosition : MonoBehaviour {
    public ScratchInputGroup_Vector3 m_position;
    public Transform m_whatToMove;

    [ContextMenu("Go To")]
    public void GoToValueInInspector()
    {

        GoToGivenValue(m_position.GetVector3());
    }
    public void GoToGivenValue(Vector3 position)
    {
        m_whatToMove.position = position;
    }

    private void Reset()
    {
        m_whatToMove = this.transform;
    }
}
