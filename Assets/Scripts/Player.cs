using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //movement
    public NavMeshAgent agent;
    public Camera cam;
    public float stopDistance;
    public bool isMoving = false;
    private Vector3 destination = new Vector3(0, 0, 0);

    //UI
    public Text hitPointText;
    public Text survivedText;

    public GameObject gameController;

    //scoring
    private float timer = 0f;
    public int maxHP;
    public int currentHP;
    public int DmgPerTick;
    public float TickRate;
    public int playerHealAmount;
    public int waterDmg;
    public AudioMixer mixer;

    //lighting
    public Light playerLight;
    public Renderer rend;
    public Material minMaterial;
    public Material maxMaterial;

    private float minSize;
    private float maxSize;

    // Use this for initialization
    void Start () {

        gameController = GameObject.FindGameObjectWithTag("GameController");
        
        maxHP = 100;
        currentHP = maxHP/2;
        //hitPointText = GameObject.FindGameObjectWithTag("HPText").GetComponent<Text>();
        survivedText = GameObject.FindGameObjectWithTag("Survived").GetComponent<Text>();
        DmgPerTick = 1;
        playerHealAmount = 1;
        stopDistance = 0.4f;
        TickRate = 0.2f;
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        waterDmg = 1;
        minSize = 0.2f;
        maxSize = 0.4f;

        StartCoroutine(Tick());
        StartCoroutine(Sputter());
	}

    IEnumerator Sputter()
    {
        if(currentHP <= 25 && currentHP > 15)
        {
            playerLight.enabled = false;
            yield return new WaitForSeconds(.1f);
            playerLight.enabled = true;
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
        }
        else if (currentHP <= 15)
        {
            playerLight.enabled = false;
            yield return new WaitForSeconds(.1f);
            playerLight.enabled = true;
            yield return new WaitForSeconds(UnityEngine.Random.Range(.5f, 1f));
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        
        StartCoroutine(Sputter());
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(TickRate);
        currentHP -= DmgPerTick;
        StartCoroutine(Tick());
    }

    public void Heal()
    {
        if (currentHP + playerHealAmount < maxHP)
        {
            currentHP += playerHealAmount;
        }
        else
        {
            currentHP = maxHP;
        }
    }

    public void Water()
    {
        currentHP -= waterDmg;
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            EndTheGame();
            Destroy(gameObject);
        }

        //hitPointText.text = currentHP.ToString();
        playerLight.intensity = (currentHP * 2f / 100f) + .25f;
        float size = Mathf.Lerp(minSize, maxSize, currentHP / 100f);
        transform.localScale = new Vector3(size, size, size);
        rend.material.Lerp(minMaterial, maxMaterial, currentHP / 100f);
        mixer.SetFloat("FirePitch", (currentHP / 100f) + .5f);

        timer += Time.deltaTime;
        survivedText.text = timer.ToString();

        if (Input.GetButton("Fire1"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                isMoving = true;
                destination = hit.point;
                agent.SetDestination(destination);
            }
        }

        if (isMoving)
        {
            if (Vector3.Distance(destination, transform.position) < stopDistance)
            {
                agent.isStopped = true;
                agent.isStopped = false;
                isMoving = false;
            }
        }        
    }

    private void EndTheGame()
    {
        currentHP = 0;
        gameController.GetComponent<GameController>().GameOver();
    }
}
