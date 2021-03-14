using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBorder : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] BoxCollider2D boxCollider;

    private void Update()
    {
        if (rectTransform.sizeDelta != Vector2.zero && rectTransform.sizeDelta != boxCollider.size)
            boxCollider.size = rectTransform.sizeDelta;

    }
}
