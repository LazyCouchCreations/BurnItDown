using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
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

    // Use this for initialization
    void Start () {

        gameController = GameObject.FindGameObjectWithTag("GameController");

        maxHP = 100;
        currentHP = maxHP/2;
        hitPointText = GameObject.FindGameObjectWithTag("HPText").GetComponent<Text>();
        survivedText = GameObject.FindGameObjectWithTag("Survived").GetComponent<Text>();
        DmgPerTick = 3;
        playerHealAmount = 1;
        stopDistance = 0.4f;
        TickRate = 0.5f;
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        waterDmg = 3;
        StartCoroutine(Tick());
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
        }

        hitPointText.text = currentHP.ToString();
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
