using UnityEngine;

public class ScratchAudioStaticSingleton
{

    private static AbstractScratchAudioPlayer m_audioPlayer;
    public static void SetAudioPlayer(AbstractScratchAudioPlayer player) { m_audioPlayer = player; }

    public static void PlaySound(AudioClip audio)
    {
        CheckAndCreateIfNull();
        m_audioPlayer.PlaySound(audio);

    }
    public static void PlaySoundUntilDone(AudioClip audio, I_ScratchProcessDoneFeedBackSettable audioPlayed)
    {
        CheckAndCreateIfNull();
        m_audioPlayer.PlaySoundUntilDone(audio, audioPlayed);

    }
    public static void StopAllSound()
    {
        CheckAndCreateIfNull();
        m_audioPlayer.StopAllSoundCreated();

    }

    private static void CheckAndCreateIfNull()
    {
        if (m_audioPlayer == null)
        {
            GameObject g = new GameObject();
            m_audioPlayer = g.AddComponent<DefaultScratchAudioPlayer>();
        }
    }
}
