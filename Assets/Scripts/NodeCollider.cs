using UnityEngine;

public class NodeCollider : MonoBehaviour {

    public GameObject node;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            node.GetComponent<Node>().StartBurning();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            node.GetComponent<Node>().StopBurning();
        }
    }
}
