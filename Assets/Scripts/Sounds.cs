using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    AudioSource[] playerSounds;
    AudioSource boostSound;
    AudioSource deathSound;
    AudioSource trampolineSound;

    private void Start()
    {
        
        if (gameObject.name == "Player")
        {
            playerSounds = GetComponents<AudioSource>();
            boostSound = playerSounds[0];
            trampolineSound = playerSounds[1];
        }
        if (gameObject.name == "DeathBorder")
        {
            deathSound = GetComponent<AudioSource>();
        }
    }

    public void PlayBoostSound()
    {
        if (boostSound.isPlaying != true)
        {
            boostSound.Play();
        }
    }

    public void PlayTrampolineSound()
    {
        if (trampolineSound.isPlaying != true)
        {
            trampolineSound.Play();
        }
    }

    public void PlayLevelClearSound()
    {

    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
}
