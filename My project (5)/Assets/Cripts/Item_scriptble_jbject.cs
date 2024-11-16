using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ItemType { Default, Food, Weapon, Instrument }
public class Item_scriptble_jbject : ScriptableObject
{

    public string itemName;
    public int Max_Amount;
    public GameObject ItemPrefab;
    public Sprite icon;
    public ItemType itemType;
    public string itemDescription;
    public bool isConsumeable;
    public string inHandName;

    [Header("Consumable Characteristics")]
    public float changeHealth;
    public float changeHunger;
    public float changeThirst;


}
