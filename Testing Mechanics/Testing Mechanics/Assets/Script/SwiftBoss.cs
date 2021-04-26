using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftBoss : MonoBehaviour
{
    public bool isFacingRight;
    public bool stayIdle;
    public bool teleported;
    public bool attacked;
    public bool spawned;
    
    public Transform[] teleportPoints = new Transform[3];

    public Transform[] spikeLocations = new Transform[20];

    public GameObject MaskPotion;
    public bool potionSpawned;

    private Animator bossAnim;
    private MobHealth mobHealthScript;
    private BoxCollider2D bossCollider;
    private SpriteRenderer rend;

    private GameObject player;
    private int currentLocation;


    // Start is called before the first frame update
    void Start()
    {
        stayIdle = false;
        teleported = false;
        attacked = false;
        spawned = false;
        potionSpawned = false;
        currentLocation = 0;
        player = PlayerManager.instance.player;
        bossAnim = GetComponent<Animator>();
        bossCollider = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        mobHealthScript = GetComponent<MobHealth>();
        StopAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        if(mobHealthScript.mobHealth < mobHealthScript.mobMaxHealth / 2)
        {
            if(!potionSpawned)
            {
                potionSpawned = true;
                mobHealthScript.halfhealthParticles.transform.position = transform.position;
                mobHealthScript.halfhealthParticles.Play();
                Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y - 2.0f , transform.position.z);
                GameObject spawnedMask = Instantiate(MaskPotion, spawnLocation, Quaternion.identity);
                spawnedMask.SetActive(true);
                //spawnedMask.GetComponentInChildren<PotionScript>().gameObject.SetActive(true);
            }
        }
    }

    public void LookAtPlayer()
	{
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFacingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFacingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = true;
        }
	}

    public IEnumerator Teleport()
    {
        Debug.Log("Teleport");
        int tempIndex;
        do
        {
            tempIndex = Random.Range(0, 3);
        }
        while (tempIndex == currentLocation);
        currentLocation = tempIndex;
        bossAnim.SetTrigger("SpawnOut");
        bossCollider.enabled = !enabled;
        yield return new WaitForSeconds(3.0f);
        transform.position = teleportPoints[currentLocation].position;
        bossCollider.enabled = enabled;
        rend.enabled = enabled;
        bossAnim.enabled = enabled;
        teleported = true;

        bossAnim.SetTrigger("SpawnIn");
        StartCoroutine(StayIdle());
    }

    public void StopAnimator()
    {
        bossAnim.enabled = !enabled;
    }

    public IEnumerator StayIdle()
    {
        if(!stayIdle)
        {
            Debug.Log("Idle");
            stayIdle = true;
            LookAtPlayer();
            yield return new WaitForSeconds(3.0f);
            stayIdle = false;
            if(!teleported)
            {
                StartCoroutine(Teleport());
            }
            else if(!attacked)
            {
                StartCoroutine(Attack());
                teleported = false;
                attacked = false;
            }
        }
        else
        {
            yield break;
        }
    }

    public IEnumerator StartBoss()
    {
        spawned = true;
        bossAnim.enabled = enabled;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(StayIdle());
    }

    public IEnumerator Attack()
    {
        Debug.Log("Attack");
        bossAnim.SetTrigger("Attack");
        for(int i = 0; i < spikeLocations.Length; i++)
        {
            if(spikeLocations[i] != null)
            {
                Debug.Log(i);
                spikeLocations[i].gameObject.SetActive(true);
                SpikeLogic script = spikeLocations[i].GetComponentInChildren<SpikeLogic>();
                script.StartAttack();
            }
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(StayIdle());
    }

    public void StopSpriteRenderer()
    {
        rend.enabled = !enabled;
    }
    
}
