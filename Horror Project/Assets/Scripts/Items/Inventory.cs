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

    public List<Key> keys = new List<Key>();

    public void AddKey(Key key)
    {
        keys.Add(key);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
