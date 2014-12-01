using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemDatabase : MonoBehaviour {
	
	public List<Item> items = new List<Item>();
	
	public Item GetItem(int id)
	{
		foreach(Item item in items)
		{
			if (item.itemID == id)
			{
				return item;
			}
		}
		return null;
	}
	
}
