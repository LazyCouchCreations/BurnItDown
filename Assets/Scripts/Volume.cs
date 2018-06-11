using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour {

    public AudioMixer mixer;
    public Slider slider;

    public void UpdateVolume()
    {
        mixer.SetFloat("MasterVolume", slider.value);
    }
}
