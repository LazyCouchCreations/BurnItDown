using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

    public GameObject hexPrefab;
    public GameObject waterPrefab;
    public GameObject playerPrefab;
    public NavMeshSurface navMesh;
    public NavMeshAgent agent;
    public Camera cam;

    //hex grid size
    int width = 20;
    int height = 20;

    float zModifier = 0.75f;
    float xModifier = 1.0f;
    float xOffset = 0.5f;
    int waterPadding = 5;

    private void Awake()
    {
        Vector3 center = new Vector3(width * 0.5f, -0.05f, height * 0.5f * zModifier);
        GameObject water = Instantiate(waterPrefab, center, Quaternion.Euler(90, 0, 0));
        water.transform.localScale = new Vector3(width + waterPadding, height * zModifier + waterPadding, 1f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float yRand = Random.Range(-1, 7) * .1f;
                if (z % 2 == 0)
                {
                    Instantiate(hexPrefab, new Vector3(x * xModifier, yRand, (z * zModifier)), Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(hexPrefab, new Vector3((x * xModifier) + xOffset, yRand, (z * zModifier)), Quaternion.identity, transform);
                }
            }
        }

        navMesh.BuildNavMesh();

    }

    // Use this for initialization
    void Start () {
        agent = Instantiate(playerPrefab, new Vector3(width * 0.5f, 0.4f, height * 0.5f * zModifier), Quaternion.identity).GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }            
        }
	}
}
