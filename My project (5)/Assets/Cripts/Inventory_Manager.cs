using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory_Manager : MonoBehaviour
{
    public Transform InventoryPanel;
    public List<inventory_slot> slots= new List<inventory_slot>();
    public bool IsOpened;
    public GameObject UIBG;
    public GameObject crossHair;
    public float reachDistanse = 3;

    public Inventory_Manager(float reachDistanse)
    {
        this.reachDistanse = reachDistanse;
    }

    Controller_cursor controller_Cursor;
    private Camera mainCamera;
    

    // Start is called before the first frame update
    private void Awake()
    {
        UIBG.SetActive(true);
    }
    void Start()
    {
       
        mainCamera = Camera.main;
        for (int i = 0; i < InventoryPanel.childCount; i++)
        {
            if (InventoryPanel.GetChild(i).GetComponent<inventory_slot>() != null )
            {
                slots.Add(InventoryPanel.GetChild(i).GetComponent<inventory_slot>());
            }
        }
        UIBG.SetActive(false);
        InventoryPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsOpened = !IsOpened;
            

            if (IsOpened == true) { 
        
                UIBG.SetActive(true);
                InventoryPanel.gameObject.SetActive(true);
                crossHair.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;



}
        else
            {
                UIBG.SetActive(false);
                InventoryPanel.gameObject.SetActive(false); // new line
                crossHair.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistanse))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (hit.collider.gameObject.gameObject.GetComponent<Item>() != null)
                {
                    AddItem(hit.collider.gameObject.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
                    Destroy(hit.collider.gameObject);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * reachDistanse, Color.blue);
        }
        
       
    }
    private void AddItem(Item_scriptble_jbject _item, int _amount)
    {
        foreach (inventory_slot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.Max_Amount)
                {
                    slot.amount += _amount;
                    slot.itemAmount_text.text = slot.amount.ToString();
                    return;
                }  
                break;
            }
        }
        foreach (inventory_slot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount_text.text = _amount.ToString();
                break;
            }
               
        }
    }
}
