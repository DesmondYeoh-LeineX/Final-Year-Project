using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {get; private set;}
    public GameObject player;
    public Transform originRespawnPoint;

    public int playerHealth = 100;
    public int playerMaxHealth = 100;

    public bool playerInvulnerable = false;
    public bool isDead = false;

    public float dashIFrameDuration = 0.5f;
    public float damagedIFrameDuration = 0.5f;

    public Transform checkPoint;

    public ParticleSystem healEffect;

    private PlayerController2D playerController;
    private Animator playerAnim;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(!playerInvulnerable && playerHealth > 0 && !playerController.canClimbLedge && !playerController.GetIsDashing())
        {
            playerHealth -= damage;
            UIManager.instance.UpdateHealthBar();
            StartCoroutine(DamagedIFrame());
        }
        else if(playerInvulnerable && playerHealth > 0 && !playerController.canClimbLedge)
        {
            // I-Framed Cue
            // Voice Act or Visual effect works
        }

        if(playerHealth <= 0 && !isDead)
        {
            Death();
        }
        //Debug.Log(playerHealth);
    }

    public IEnumerator DamagedIFrame()
    {
        playerAnim.SetTrigger("Hurt");
        playerInvulnerable = true;
        yield return new WaitForSeconds(damagedIFrameDuration);
        playerInvulnerable = false;
    }

    public IEnumerator DashIFrame()
    {
        playerInvulnerable = true;
        yield return new WaitForSeconds(dashIFrameDuration);
        playerInvulnerable = false;
    }

    public void Death()
    {
        Debug.Log("Dead");
        isDead = true;
        playerAnim.SetTrigger("Death");
        playerController.enabled = false;
        UIManager.instance.initiateDeadPanel();
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(10.0f);
        // show buttons to restart scene or exit to main menu
        SceneManager.LoadScene("BetaLevel");
        // isDead = false;
        // playerHealth = playerMaxHealth;
        // UIManager.instance.UpdateHealthBar();
        // playerController.enabled = true;
        // player.transform.position = originRespawnPoint.position;
        // playerAnim.Play("Idle", -1, 0f);
    }

    public void SpawnAtCheckpoint()
    {
        player.transform.position = checkPoint.position;
    }

    public void HealEffect()
    {
        healEffect.Play();
        player.GetComponent<PlayerSoundEffect>().HealSFX();
    }
}
