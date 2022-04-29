using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake() => instance = this;
    #endregion

    [SerializeField] int size = 5;
    public List<Item> items = new List<Item>();

    public List<Key> keyChain = new List<Key>();

    public void AddKey(Key key)
    {
        keyChain.Add(key);
    }

    public void AddItem(Item item)
    {
        if(size<items.Count)
        {
            Debug.Log("Inventory is full");
            return;
        }
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
