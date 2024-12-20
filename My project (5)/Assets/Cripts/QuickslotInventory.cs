﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
    // Объект у которого дети являются слотами
    public Transform quickslotParent;
    public Inventory_Manager inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Text healthText;
    public Transform itemContainer;
    public inventory_slot activeSlot = null;
    public Transform allWeapons;

    // Update is called once per frame
    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        // Используем колесико мышки
        if (mw > 0.1)
        {
            // Берем предыдущий слот и меняем его картинку на обычную

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)

            // Если крутим колесиком мышки вперед и наше число currentQuickslotID равно последнему слоту, то выбираем наш первый слот (первый слот считается нулевым)
            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                // Прибавляем к числу currentQuickslotID единичку
                currentQuickslotID++;
            }
            // Берем предыдущий слот и меняем его картинку на "выбранную"

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>();
            ShowItemInHand();
            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

        }
        if (mw < -0.1)
        {
            // Берем предыдущий слот и меняем его картинку на обычную

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)


            // Если крутим колесиком мышки назад и наше число currentQuickslotID равно 0, то выбираем наш последний слот
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                // Уменьшаем число currentQuickslotID на 1
                currentQuickslotID--;
            }
            // Берем предыдущий слот и меняем его картинку на "выбранную"

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>();
            ShowItemInHand();
            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

        }
        // Используем цифры
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            // если мы нажимаем на клавиши 1 по 5 то...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
                if (currentQuickslotID == i)
                {
                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                        activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>();
                        ShowItemInHand();
                        //foreach ...
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                        activeSlot = null;
                        HideItemsInHand();
                        //foreach ...

                    }
                }
                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    // ЗДЕСЬ ТЕБЕ НУЖЕН FOREACH ЦИКЛ КОТОРЫЙ БУДЕТ ВЫКЛЮЧАТЬ ВСЕ ОБЪЕКТЫ В МАССИВЕ ITEMS
                    currentQuickslotID = i;

                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>();
                    ShowItemInHand();
                    // СЮДА СКОПИРУЙ ТО ЧТО ТЫ ПИСАЛ КОГДА ВКЛЮЧАЛ КАЛАШ <--
                }
            }
        }
        // Используем предмет по нажатию на левую кнопку мыши
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().item != null)
            {
                if (quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().item.isConsumeable && !inventoryManager.IsOpened && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    // Применяем изменения к здоровью (будущем к голоду и жажде) 
                    ChangeCharacteristics();

                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().amount <= 1)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().amount--;
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().itemAmount_text.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().amount.ToString();
                    }
                }
            }
        }
    }

    private void ChangeCharacteristics()
    {
        // Если здоровье + добавленное здоровье от предмета меньше или равно 100, то делаем вычисления... 
        if (int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().item.changeHealth <= 100)
        {
            float newHealth = int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<inventory_slot>().item.changeHealth;
            healthText.text = newHealth.ToString();
        }
        // Иначе, просто ставим здоровье на 100
        else
        {
            healthText.text = "100";
        }
    }

    private void ShowItemInHand()
    {
        HideItemsInHand();
        if (activeSlot.item == null)
        {
            return;
        }
        for (int i = 0; i < allWeapons.childCount; i++)
        {
           /* if (activeSlot.item == allWeapons.GetChild(i).name)
            {
                allWeapons.GetChild(i).gameObject.SetActive(true);
            }*/
        }
    }
    private void HideItemsInHand()
    {
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            allWeapons.GetChild(i).gameObject.SetActive(false);
        }
    }
}
