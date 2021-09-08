using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallScript : MonoBehaviour
{
    public Transform opened;
    public Transform closed;
    public float step;
    [SerializeField] private bool openGate;
    private bool closeGate;

    // Start is called before the first frame update
    void Start()
    {
        openGate = false;
        closeGate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(openGate)
        {
            MoveTheGates(opened.position);
        }

        if(closeGate)
        {
            MoveTheGates(closed.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(UIManager.instance.CompareCounters())
            {
                openGate = true;
            }
            else
            {
                UIManager.instance.StartCoroutine("KillAllMobsPrompt");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            closeGate = true;
        }
    }

    private void MoveTheGates(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, step * Time.deltaTime);
        if(Vector2.Distance(opened.position, transform.position) < 0.1f)
        {
            openGate = false;
        }

        if(Vector2.Distance(closed.position, transform.position) < 0.1f)
        {
            closeGate = false;
        }
    }
}
