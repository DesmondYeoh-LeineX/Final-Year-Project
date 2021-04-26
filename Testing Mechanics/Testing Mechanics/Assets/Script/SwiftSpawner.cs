using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftSpawner : MonoBehaviour
{
    public SwiftBoss bossScript;
    public GameObject swiftBoss;

    private void Start() 
    {
        swiftBoss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!bossScript.spawned)
            {
                swiftBoss.SetActive(true);
                StartCoroutine(bossScript.StartBoss());
            }
        }
    }
}
