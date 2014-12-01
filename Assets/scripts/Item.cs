using UnityEngine;
using System.Collections;



[System.Serializable]
public class Item {
	
	public string itemName;
	public int itemID;
	public string itemDescription;
	public ItemType itemType;
	public Texture2D itemIcon;
	public GameObject itemObject;
	

	public enum ItemType {
		
		OneTime,
		Permanent
		
	}
	
	
	// Generic item constructor (Icon and Object is based on item name, so match them in resources folder)
	
	public Item(string itemName, int itemID, string itemDescription, ItemType itemType, Texture2D itemIcon, GameObject itemObject)
	{
		this.itemName = itemName;
		this.itemID = itemID;
		this.itemDescription = itemDescription;
		this.itemType = itemType;
		this.itemIcon = itemIcon;
		this.itemObject = itemObject;

		
	}
	
	public Item()
	{
		this.itemID = -1;
	}
	
	public Item(ItemType itemType)
	{
		this.itemID = -1;
		this.itemType = itemType;
	}
	
}


