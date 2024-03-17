using UnityEngine;
using UnityEngine.Events;

public class Scratch3DMono_OnClickOrTouched : MonoBehaviour
{
    public UnityEvent m_onMouseDown;
    public UnityEvent m_onMouseUp;
    private void OnMouseDown()
    {
        m_onMouseDown.Invoke();
    }
    private void OnMouseUp()
    {
        m_onMouseUp.Invoke();
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
