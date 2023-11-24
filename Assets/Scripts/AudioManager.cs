using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        Debug.Log("Played click sound");
        _audioSource.PlayOneShot(clip);
    }
}