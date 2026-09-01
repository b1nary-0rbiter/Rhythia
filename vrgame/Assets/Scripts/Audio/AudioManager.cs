// Audio manager for music, beats, and sound effects
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip slashSound;
    public AudioClip zombieDeathSound;
    public AudioClip beatHitSound;
    
    void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        
        Debug.Log("Audio manager initialized");
    }
    
    public void PlaySlash()
    {
        if (sfxSource != null && slashSound != null)
        {
            sfxSource.PlayOneShot(slashSound);
        }
    }
    
    public void PlayDeath()
    {
        if (sfxSource != null && zombieDeathSound != null)
        {
            sfxSource.PlayOneShot(zombieDeathSound);
        }
    }
    
    public void PlayBeat()
    {
        if (sfxSource != null && beatHitSound != null)
        {
            sfxSource.PlayOneShot(beatHitSound);
        }
    }
}