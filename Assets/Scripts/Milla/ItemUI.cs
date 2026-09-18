using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public TMP_Text itemtUI;
    private void Start()
    {
        Change();
    }
    public void Change()
    {
        print("testi ui");
        int count = Inventory.Instance.itemList.Count;
        itemtUI.text = $"items: {count}/2";
    }
}
