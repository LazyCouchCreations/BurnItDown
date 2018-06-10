using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject[] Nodes;
    public Collider nodeCollider;
    public int myNodeID;
    private GameObject myNode;
    public int nodeMaxHP;
    public int nodeCurrentHP;
    public float burnRate = 0.1f;
    public float maxHealRate = 7f;
    public float minHealRate = 5f;
    public float healRate;
    public GameObject player;

	// Use this for initialization
	void Start () {

        //is the tile below water?
        if (transform.position.y == -0.1f)
        {
            //spawn a beach
            myNodeID = 0;
        }
        else
        {
            //spawn random nonbeach tile
            myNodeID = Random.Range(1, Nodes.Length);
        }

        SpawnNode(myNodeID);
        nodeCurrentHP = 1;
        healRate = Random.Range(minHealRate, maxHealRate);
        StartCoroutine(Healing());
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void SpawnNode(int nodeID)
    {
        myNode = Instantiate(Nodes[nodeID], transform.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0), transform);
    }

    private void Update()
    {
        if (nodeCurrentHP <= 0)
        {
            nodeCurrentHP = nodeMaxHP-1;
            Destroy(myNode);
            myNodeID--;
            SpawnNode(myNodeID);
        }

        if (myNodeID > 1 && myNodeID < Nodes.Length - 1)
        {
            if (nodeCurrentHP >= 10)
            {
                nodeCurrentHP = 1;
                Destroy(myNode);
                myNodeID++;
                SpawnNode(myNodeID);
            }
        }        
    }

    public void StartBurning()
    {
        StopAllCoroutines();
        StartCoroutine(Burn());
    }

    IEnumerator Burn()
    {
        if (myNodeID > 1)
        {
            nodeCurrentHP--;
            player.GetComponent<Player>().Heal();
        }
        else if (myNodeID == 0)
        {
            player.GetComponent<Player>().Water();
        }

        yield return new WaitForSeconds(burnRate);
        StartCoroutine(Burn());
    }

    public void StopBurning()
    {
        StopAllCoroutines();
        StartCoroutine(Healing());
    }

    IEnumerator Healing()
    {
        if(myNodeID > 1)
        {
            yield return new WaitForSeconds(healRate);
            if(nodeCurrentHP < nodeMaxHP)
            {
                nodeCurrentHP++;
            }            
            StartCoroutine(Healing());
        }
    }
}
