using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBorder : MonoBehaviour
{
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;

    public Enum_Elements Element { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (GetComponent<Image>().sprite.name == "Border_Fire")
            Element = Enum_Elements.Fire;
        else if (GetComponent<Image>().sprite.name == "Border_Water")
            Element = Enum_Elements.Water;
        else
            Element = Enum_Elements.Standard;
    }

    private void Update()
    {
        if (rectTransform.sizeDelta != Vector2.zero && rectTransform.sizeDelta != boxCollider.size)
            boxCollider.size = rectTransform.sizeDelta;
    }
}
