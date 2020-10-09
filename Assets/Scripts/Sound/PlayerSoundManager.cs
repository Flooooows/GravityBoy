using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    private Transform mytransform;

    private void Start()
    {
        mytransform = transform;
    }

    [SerializeField]
    AudioClip onDeathSound;

    [SerializeField]
    AudioClip onCollectSound;

    [SerializeField]
    AudioClip onJumpSound;

    public void playDeathSound()
    {
        PlaySound(onDeathSound);
    }

    public void playCollectSound()
    {
        PlaySound(onCollectSound);
    }

    public void playJumpSound()
    {
        PlaySound(onJumpSound);
    }

    private void PlaySound(AudioClip clip)
    {
        mytransform.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
