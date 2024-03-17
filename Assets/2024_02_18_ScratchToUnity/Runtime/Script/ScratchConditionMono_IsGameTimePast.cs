using UnityEngine;

public class ScratchConditionMono_IsGameTimePast: AbstractScratchMono_ConditionHolder {

    public float m_gameTime = 5;

    public override bool IsConditionTrue()
    {
        return Time.time > m_gameTime;
    }
}