using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLogic : MonoBehaviour
{
    public GameObject WarningLight;
    public int spikeDamage;

    private PolygonCollider2D colInfo;
    private Animator spikeAnim;
    // Start is called before the first frame update
    void Start()
    {
        colInfo = this.gameObject.GetComponent<PolygonCollider2D>();
        spikeAnim = this.gameObject.GetComponent<Animator>();
        transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage(spikeDamage);
        }
    }

    public void StartAttack()
    {
        spikeAnim.Play("SpikeCharge", -1, 0f);
    }


    public void removeWarningLight()
    {
        WarningLight.SetActive(false);
    }

    public void activeWarningLight()
    {
        WarningLight.SetActive(true);
    }

    public void turnOffCollider()
    {
        colInfo.enabled = !enabled;
    }

    public void turnOnCollider()
    {
        colInfo.enabled = enabled;
    }
    
    public void turnOffObject()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
