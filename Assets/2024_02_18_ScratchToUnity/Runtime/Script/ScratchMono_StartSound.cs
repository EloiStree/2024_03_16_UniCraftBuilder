using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_StartSound : A_ScratchBlockableMono
{
    public AudioClip m_sound;
    public  override IEnumerator DoTheScratchableStuff()
    {
        
        ScratchAudioStaticSingleton.PlaySound(m_sound);
        yield return null;
        
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        ScratchAudioStaticSingleton.PlaySound(m_sound);
    }
}
public abstract class AbstractScratchAudioPlayer : MonoBehaviour
{

    public abstract void PlaySoundUntilDone(AudioClip audio, I_ScratchProcessDoneFeedBackSettable audioPlayed);
    public abstract void PlaySound(AudioClip audio);

    public abstract void StopAllSoundCreated();
}
