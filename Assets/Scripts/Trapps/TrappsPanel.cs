using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrappsPanel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Sprite grenadeSprite;
    [SerializeField] private Sprite grenadeSpriteSelected;
    [SerializeField] private Sprite dropBombSprite;
    [SerializeField] private Sprite dropBombSpriteSelected;

    [SerializeField] private GameObject grenade;
    [SerializeField] private GameObject dropBomb;
    private GameObject objectForSpawn;

    private Button grenadeBtn;
    private Button dropBombBtn;

    #endregion

    #region Methods
    private void Awake()
    {
        grenadeBtn = transform.Find("GrenadeButton").GetComponent<Button>();
        dropBombBtn = transform.Find("DropBombButton").GetComponent<Button>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objectForSpawn != null)
        {
            PlaceTower();
        }
    }
    public void OnGrenadePressed()
    {
        objectForSpawn = grenade;
        grenadeBtn.GetComponent<Image>().sprite = grenadeSpriteSelected;
        DisableButtons();
    }
    public void OnDropBombPressed()
    {
        objectForSpawn = dropBomb;
        dropBombBtn.GetComponent<Image>().sprite = dropBombSpriteSelected;
        DisableButtons();
    }

    private void PlaceTower()
    {
        Vector3 positionForSpawn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionForSpawn.z = - Camera.main.transform.position.z;
        Instantiate(objectForSpawn, positionForSpawn, Quaternion.identity);
        objectForSpawn = null;
        ChangeSpritesToDefault();
        EnableButtons();
    }

    private void ChangeSpritesToDefault()
    {
        grenadeBtn.GetComponent<Image>().sprite = grenadeSprite;
        dropBombBtn.GetComponent<Image>().sprite = dropBombSprite;
    }

    private void DisableButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.enabled = false;        
        }
    }
    private void EnableButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.enabled = true;
        }
    }

    #endregion
}
