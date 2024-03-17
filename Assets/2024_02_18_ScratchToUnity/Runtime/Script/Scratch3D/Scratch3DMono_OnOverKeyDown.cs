using UnityEngine;
using UnityEngine.Events;

public class Scratch3DMono_OnOverKeyDown : MonoBehaviour
{
    public KeyCode m_keyToUse = KeyCode.Return;
    public UnityEvent m_onMouseOverAndKeyDown;
    public bool m_isOver;
    public void OnMouseEnter()
    {
        m_isOver = true;
    }
    private void Update()
    {
        if (m_isOver && Input.GetKeyDown(m_keyToUse)) {
            m_onMouseOverAndKeyDown.Invoke();
        }
    }
    private void OnMouseExit()
    {

        m_isOver = false;
    }
}


//public class Scratch3DMono_3DStackHolderWithDebug : A_ScratchBlockableMono
//{
//    public A_ScratchBlockableMono m_startBlockableOnAction;
//    public UnityEvent m_onBlockLogicStartRunning;
//    public UnityEvent m_onBlockLogicEndRunning;

//    public bool m_notifyNextUpdateStartRunning;
//    public bool m_notifyNextUpdateStopRunning;

//    private void Update()
//    {
//        if (m_notifyNextUpdateStartRunning)
//        {
//            m_notifyNextUpdateStartRunning = false;
//            m_onBlockLogicStartRunning.Invoke();
//        }
//        if (m_notifyNextUpdateStopRunning)
//        {
//            m_notifyNextUpdateStopRunning = false;
//            m_onBlockLogicEndRunning.Invoke();
//        }
//    }

//    public override IEnumerator DoTheScratchableStuff()
//    {
//        m_notifyNextUpdateStartRunning = true;
//        yield return m_startBlockableOnAction.DoTheScratchableStuff();
//        m_notifyNextUpdateStopRunning = false;

//    }
//}

//public class Scratch3DNextCastMono_CastableBlockStartEnd : A_ScratchBlockableMono
//{
//    public A_ScratchBlockableMono m_stackStart;
//    public Scratch3DMono_AbstractNextCastBlocks m_

//    public override IEnumerator DoTheScratchableStuff()
//    {
//        throw new System.NotImplementedException();
//    }
//}
