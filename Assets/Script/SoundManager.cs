using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance {  get { return instance; } }

    [SerializeField]
    private AudioClip sword;
    public AudioClip Sword { get { return sword; } }
    [SerializeField]
    private AudioClip fire;
    public AudioClip Fire { get { return fire; } }
    [SerializeField]
    private AudioClip skull;
    public AudioClip Skull { get { return skull; } }
    [SerializeField]
    private AudioClip recovery;
    public AudioClip Recovery { get { return recovery; } }
    [SerializeField]
    private AudioClip coin;
    public AudioClip Coin { get { return coin; } }


    private void Awake()
    {
        instance = this;
    }

    public void Play(AudioClip clip)
    {
        AudioSource m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_AudioSource.clip = clip;
        StartCoroutine(PlayC(m_AudioSource));
    }

    IEnumerator PlayC(AudioSource audioSource)
    {
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Destroy(audioSource);
    }
}
