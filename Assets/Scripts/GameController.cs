using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hexPrefab;
    public GameObject waterPrefab;
    public GameObject playerPrefab;
    public NavMeshSurface navMesh;
    
    //hex grid size
    int width = 10;
    int height = 6;
    int maxTileHeight = 5;

    float zModifier = 0.75f;
    float xModifier = 1.0f;
    float xOffset = 0.5f;
    int waterPadding = 10;

    //UI
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1;

        Vector3 center = new Vector3(width * 0.5f, -0.05f, height * 0.5f * zModifier);
        GameObject water = Instantiate(waterPrefab, center, Quaternion.Euler(90, 0, 0));
        water.transform.localScale = new Vector3(width + waterPadding, height * zModifier + waterPadding, 1f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float yRand = Random.Range(-1, maxTileHeight) * .1f;
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
        Instantiate(playerPrefab, new Vector3(width * 0.5f, 0.4f, height * 0.5f * zModifier), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
