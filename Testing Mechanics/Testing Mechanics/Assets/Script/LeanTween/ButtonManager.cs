using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public float animTime;

    public void PointerEnterButton(GameObject theButton)
    {
        LeanTween.cancel(theButton);
        theButton.transform.localScale = Vector3.one;

        Vector3 hovered = new Vector3 (1.3f, 1.10f, 1.0f);
        LeanTween.scale(theButton, hovered, animTime)
            .setEaseInOutBack();
    }

    public void PointerExitButton(GameObject theButton)
    {
        LeanTween.cancel(theButton);
        theButton.transform.localScale = Vector3.one;

        LeanTween.scale(theButton, Vector3.one, animTime)
            .setEaseInOutBack();
    }
}
