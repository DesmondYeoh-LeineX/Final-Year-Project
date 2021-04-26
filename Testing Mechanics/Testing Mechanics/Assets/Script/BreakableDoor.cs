using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    public bool broken;
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.C) && !broken)
            {
                broken = true;
                BreakDoor();
            }
        }
    }

    private void BreakDoor()
    {
        transform.parent.transform.eulerAngles = new Vector3(0, 0, 90f);
    }
}
