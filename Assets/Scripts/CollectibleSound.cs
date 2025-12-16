using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CollectibleSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private AudioSource audioSource;

    public void Play()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAndDie());
    }

    private IEnumerator PlayAndDie()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }

}
