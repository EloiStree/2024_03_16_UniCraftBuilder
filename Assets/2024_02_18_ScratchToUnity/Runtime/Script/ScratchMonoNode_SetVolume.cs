using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMonoNode_SetVolume : A_ScratchBlockableMono
{

    public AbstractScratchMono_VariableHolderAsString m_volumePercent;

    public override IEnumerator DoTheScratchableStuff()
    {
        DoTheScratchableStuffWithoutCoroutine();
           yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        AudioListener.volume = Mathf.Clamp(m_volumePercent.GetValueAsFloat(), 0.0f, 1.0f);
    }
}
