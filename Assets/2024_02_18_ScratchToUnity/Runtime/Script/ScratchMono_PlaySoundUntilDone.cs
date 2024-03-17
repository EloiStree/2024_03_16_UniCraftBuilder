using System;
using System.Collections;
using UnityEngine;

public class ScratchMono_PlaySoundUntilDone : A_ScratchBlockableMono
{
    public AudioClip m_sound;
    public override IEnumerator DoTheScratchableStuff()
    {
        ScratchProcessDoneFeedBack feedBack = new ScratchProcessDoneFeedBack();
        feedBack.SetAsRunning();
        ScratchAudioStaticSingleton.PlaySoundUntilDone(m_sound, feedBack);
         while (feedBack!=null && !feedBack.IsProcessFinished())
            yield return new WaitForEndOfFrame();
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        NotifyTheActionCantBeDoneInNonePlayMode();
    }
}
