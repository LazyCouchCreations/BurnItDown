using UnityEngine;

public class CameraBob : MonoBehaviour {

    public Camera cam;
    public int maxFOV;
    public int minFOV;
    public float bobTimeMod;

	// Use this for initialization
	void Start () {
        bobTimeMod = 2f;

    }
	
	// Update is called once per frame
	void Update () {
        float lerp = Mathf.PingPong(Time.time, bobTimeMod) / bobTimeMod;
        cam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, lerp);
	}
}
