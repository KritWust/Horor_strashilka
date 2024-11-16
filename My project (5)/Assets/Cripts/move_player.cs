using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class move_player : MonoBehaviour
{
   
    public float speed = 5f;
    public Inventory_Manager inventory_manager;
    public QuickslotInventory quickslotInventory;
    public Animator anim;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           /* if(quickslotInventory.activeSlot != null)
            {
                if (quickslotInventory.activeSlot.item.itemType == ItemType.Instrument)
                {
                    if (inventory_manager.IsOpened == false)
                    {
                        anim.SetBool("Hit", true);
                    }
                }
            }*/
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Hit", false);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        

    }
}