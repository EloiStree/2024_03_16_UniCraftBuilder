using UnityEngine;

public class ScratchConditionMono_IsKeyPressed : AbstractScratchMono_ConditionHolder
{

    public KeyCode m_keyObserved;
    public override bool IsConditionTrue()
    {
        return Input.GetKey(m_keyObserved);
    }
}
