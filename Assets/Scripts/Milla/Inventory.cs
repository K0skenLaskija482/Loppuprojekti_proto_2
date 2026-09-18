using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<WorldItems> itemList;
    private int MaxAmountOfItems = 2;
    private int itemCount = 0;

    //reference to the UI
    public ItemUI ui;

    private void Awake()
    {
        itemList = new List<WorldItems>();
        Instance = this;
        //ui = GetComponent<ItemUI>();
        print("tests");
    }

    private void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            print("test");
            Debug.Log(itemList[0].name);
        }
    }

    /// <summary>
    /// Add item to inventory
    /// </summary>
    /// <param name="item"></param>
    public void Add(WorldItems item, GameObject other)
    {
        if (MaxAmountOfItems > itemCount)
        {
            itemList.Add(item);
            ui.Change();
            itemCount++;
            other.SetActive(false);
            return;
        }
        Debug.Log("Liika esineitä");
    }

    //remove item from inventory (not gonna bother rn)
    /*public void Remove(WorldItems item)
    {
        itemList.Remove(item);
    }*/
}
