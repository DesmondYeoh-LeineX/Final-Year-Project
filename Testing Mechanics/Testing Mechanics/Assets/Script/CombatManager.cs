using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance {get; private set;}

    public bool tryAttack = false;
    public bool canReceiveInput = true;
    public bool inputReceived = false;
    public Animator playerAnim;
    public PlayerController2D playerController;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    public void TryAttack()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(canReceiveInput)
            {
                inputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }
    }

    public void ResetAttack()
    {
        playerController.ResetAttack();
    }
}
