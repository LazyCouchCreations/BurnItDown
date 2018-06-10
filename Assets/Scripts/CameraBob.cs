using UnityEngine;

public class CameraBob : MonoBehaviour {

    public Camera cam;
    public int maxFOV;
    public int minFOV;
    public float bobTimeMod;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, Time.deltaTime * bobTimeMod);
	}
}
