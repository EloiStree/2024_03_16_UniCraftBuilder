using System.Collections;
using UnityEngine;

public class ScratchMonoNode_Wait : A_ScratchBlockableMono
{
    public AbstractScratchMono_VariableHolderAsString m_waitInSeconds;

    public override IEnumerator DoTheScratchableStuff()
    {
        yield return new WaitForSeconds(m_waitInSeconds.GetValueAsFloat());
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
    }
}
