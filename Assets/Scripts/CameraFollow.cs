using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    //public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.one;
    public float maxSpeed;
    
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        transform.position = target.position + offset;
    }

    //update after all movement
    private void LateUpdate()
    {
        if(target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, Time.deltaTime, maxSpeed);
            transform.position = smoothedPosition;
        }
    }
}
