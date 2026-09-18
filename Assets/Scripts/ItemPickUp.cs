using NUnit.Framework.Interfaces;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public WorldItems itemsToGive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //can pick up item
            Inventory.Instance.Add(itemsToGive, gameObject);
            //this.gameObject.SetActive(false);
        }
    }
}
