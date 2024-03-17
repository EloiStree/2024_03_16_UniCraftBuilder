using UnityEngine;

public class ScratchConditionMono_IsMouseDown : AbstractScratchMono_ConditionHolder
{
    public MouseCode m_mouseKeyObserved;
    public enum MouseCode: int  { Left=0, Right = 1, Middle =2  }

    public override bool IsConditionTrue()
    {

        return Input.GetMouseButton((int)m_mouseKeyObserved);
    }
}