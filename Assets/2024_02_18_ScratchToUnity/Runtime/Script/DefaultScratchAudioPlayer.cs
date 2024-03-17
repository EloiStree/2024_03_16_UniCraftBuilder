using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultScratchAudioPlayer : AbstractScratchAudioPlayer
{

    public GameObject m_audioPlayerPrefab;
    public Transform m_parent;
    public List<GameObject> m_created= new List<GameObject>();


    private void Awake()
    {
        ScratchAudioStaticSingleton.SetAudioPlayer(this);
        InvokeRepeating("FlushEmpty", 1, 1);
    }
    private void FlushEmpty() {
        m_created = m_created.Where(k => k != null).ToList();
    }
    public override void PlaySound(AudioClip audio)
    {
        PlaySoundUntilDone(audio, null);
    }

    public override void PlaySoundUntilDone(AudioClip audio, I_ScratchProcessDoneFeedBackSettable audioPlayed)
    {
        if (audioPlayed != null)
            audioPlayed.SetAsRunning();
        StartCoroutine(StartCoroutineAudio(audio, audioPlayed));
    }
    public IEnumerator StartCoroutineAudio(AudioClip sound, I_ScratchProcessDoneFeedBackSettable finished) {

        if (finished != null)
            finished.SetAsRunning();

       
        GameObject gs = m_audioPlayerPrefab!=null?
            GameObject.Instantiate(m_audioPlayerPrefab):
            new GameObject();
        m_created.Add(gs);
        gs.name = "Audio: " + sound.name;
        gs.transform.parent = m_parent;
        AudioSource source = gs.GetComponent<AudioSource>();
        if (source == null)
            source = gs.AddComponent<AudioSource>();
        if(source!=null)
        {
            source.clip = sound;
            source.Play();
        }
        Destroy(gs, sound.length+1f);

        // Should be split in piece to check every 0.1 seconds if the game was destroyed in case of all stop.
        if (finished != null)
            yield return new WaitForSeconds(sound.length);

     
        
        if (finished != null)
            finished.SetAsStopped();
    }

    public override void StopAllSoundCreated()
    {
        for (int i = m_created.Count-1 ; i >=0; i--)
        {
            if (m_created[i] != null) { Destroy(m_created[i]); }
        }
    }
}