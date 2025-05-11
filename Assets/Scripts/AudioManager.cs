using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("PickUp")]
    [SerializeField] AudioClip pickupClip;
    [SerializeField] [Range(0f, 1f)] float pickupVolume = 1f;

    public void PlayPickupClip()
    {
        PlayClip(pickupClip, pickupVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
