using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class inventory_slot : MonoBehaviour
{
    public Item_scriptble_jbject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public Text itemAmount_text;

    private void Awake()
    {
        iconGO = transform.GetChild(0).GetChild(0).gameObject;
        itemAmount_text = transform.GetChild(0).GetChild(1).GetComponent<Text>();

    }
    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        iconGO.GetComponent<Image>().sprite = icon;
    }
}
