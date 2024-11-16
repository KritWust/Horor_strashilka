using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Item", menuName= "inventory/Items/New Food Item")]
public class Itemcreator : Item_scriptble_jbject
{
    public float Heal_Amount;

    // Start is called before the first frame update
    void Start()
    {
        itemType = ItemType.Food;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
