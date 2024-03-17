using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchConditionMono_IsInUnityEditor : AbstractScratchMono_ConditionHolder
{
    public override bool IsConditionTrue()
    {
        #if UNITY_EDITOR
                return true;
        #else
                return false;
        #endif
    }
}
