using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour {

    //public AudioMixer mixer;
    public AudioSource source;
    public Slider slider;

    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void UpdateVolume()
    {
        //mixer.SetFloat("MasterVolume", slider.value);
        source.volume = slider.value;
    }
}
