using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour {
	[HideInInspector]
	PlatformerCharacter2D character;
	static bool showInventory; // Hide/Show inventory
	static KeyCode invKey = KeyCode.I; // Keycode enum for selected inventory toggle key
	static bool toggleEquipPosition; // Determines the location for the default equipment window
	static int slotSize = 50; // Squared size of the slot
	static int slotPadding = 12; // Amount of space between slots
	static int rowCount = 2; // Amount of horizontal slots
	static int colCount = 3; // Amount of vertical slots
	static int equipCol = 1, equipRow = 1; // Vertical and horizontal slot count for equipment (need to have enough equippedItems[] slots and itemTypes defined in Item.cs)
	static Rect inventorySize; // Rectangle that holds the inventory window position and size (size is generated)
	Collision2D coll;
	static int id;

	static Item[] equippedItems = new Item[1];
	static GUISkin skin; // The skin where the GUI graphics are stored ! V1.5: Skin is retrieved from Resources automatically
	static List<Item> inventory = new List<Item>(); // List that holds player's items
	static ItemDatabase database; // Get the items stored in the Item Database object
	static bool showTooltip; // Show/Hide tooltip (generated)
	static string tooltipText; // The tooltip content
	static bool draggingItem; // Bool to determine if an item is being dragged (generated)
	static Item draggedItem; // The item that is currently being dragged (generated)
	static int draggedIndex; // Stores the slot an item was dragged from
	// Coordinate of the click to be able to draw the sprite at the correct off-set to the mouse
	static Vector2 dragCoord;
	public int playerEquip;
	//
	void Awake () {

	}
	
	void Start () {
		character = GetComponent<PlatformerCharacter2D> ();
		// Loop to add an inventory slot for each slot based on the result of XxY
		for (int i = 0; i < (rowCount * colCount); i++)
			inventory.Add(new Item());
		
		// Locate the AMP Inventory GUI Skin
		skin = Resources.Load<GUISkin>("AMP");
		
		// Set the inventory window to be hidden by default
		showInventory = false;
		
		// Find the item's database object and set it to be equal to database
		database = GameObject.Find("Item Database").GetComponent<ItemDatabase>();
		
		// Call the inventory load method. This loads the saved items and equipment. This method is required to successfully load
		// the equipment slots with the correct itemTypes.

		// Method that adds an item based on its ID

	}
	
	void Update()
	{
		// Check to see if the inventory button has been pressed
		if (Input.GetKeyDown(invKey) && !draggingItem)
			// If it has, set show inventory to be whatever it isn't (show it if it's hidden, hide it is it's visible)
			showInventory = !showInventory;
	}
	
	void OnGUI()
	{
		// Set the curernt GUI skin to be equal to the custom inventory gui skin set in the inspector
		GUI.skin = skin;
		
		// Set the tooltip to a blank screen every GUI event (the CreateTooltip method just overrides this when it is being used)
		tooltipText = "";
		
		// If show inventory is true (this is changed by hitting the "inventory" input button)
		if (showInventory)
		{
			// Call the method that creates and draws the inventory components
			DrawInventory();
			
			// Draw the tooltip if showtooltip is true
			if (showTooltip)
				DrawTooltip();
			
			// If we're dragging an item, draw the item's icon at the mouse position and make it follow the cursor
			if (draggingItem)
				GUI.DrawTexture(new Rect(Event.current.mousePosition.x - dragCoord.x, Event.current.mousePosition.y - dragCoord.y, slotSize, slotSize), draggedItem.itemIcon);
		}
		else if (draggingItem)
		{
			// This is to check to see if we're dragging an item while the inventory isn't displayed
			// If we are, we'll just reset the dragged item to make sure we can't mess something up
			inventory[draggedIndex] = draggedItem;
			draggingItem = false;
			draggedItem = null;
		}
		
		
	}
	
	// Main method for drawing the inventory and its components
	void DrawInventory()
	{
		// Initialize an index variable that we can use as a reference within our inventory loops
		int i = 0;
		
		// Calculate the size of the inventory windows based on the inventory components
		inventorySize.width = (slotSize + slotPadding) * colCount + slotPadding;
		inventorySize.height = (slotSize + slotPadding) * rowCount + slotPadding;
		
		// Draw the inventory window's background graphic using the size we calculated above
		GUI.Box(inventorySize, "", skin.GetStyle("Inventory Background"));
		
		// Store the current GUI input event in an Event variable
		Event e = Event.current;
		
		// Store the position and size of each item slot within a temp variable we use for drawing the slots
		Rect slotRect = new Rect(inventorySize.x,inventorySize.y, slotSize, slotSize);

		
		// Loop through each row of the inventory
		for (int y = 0; y < rowCount; y++)
		{
			// Then loop through each column (this is a great way to generate a grid)
			for(int x = 0; x < colCount; x++)
			{
				// Modify the slotRect rect based on inventory components' dimensions to find out where to draw the slots
				// (based on the position of the inventory window and the current item the loop is on)
				slotRect.x = slotPadding + inventorySize.x + x * (slotSize + slotPadding); // column position
				slotRect.y = slotPadding + inventorySize.y + y * (slotSize + slotPadding); // row position
				
				// Store the current loop's item in a temp Item variable for easier reference
				Item item = inventory[i];
				
				// Draw the slot graphic
				GUI.Box(slotRect, "", skin.GetStyle("Box"));
				
				// Check to see if the slot has an item in it
				// We do this with itemName as all slots have an instance of item, just not any item information
				if (item.itemName != null)
				{
					
					// If there is an item, let's draw the item's icon within that slot
					if (item.itemIcon != null)
						GUI.DrawTexture(slotRect, item.itemIcon);
					//else
						//Debug.LogWarning("Failed to load item icon for " + item.itemName + ". If you have added an icon be sure to restart the game!");
					// Now let's check to see if the mouse's position is within the item's slot
					if (slotRect.Contains(Event.current.mousePosition))
					{
						// Here we're going to check to see if the left-mouse-button was clicked on an item and then dragged
						// We also make sure we're not already dragging an item so we don't get any funny behaviourCu
						if (e.isMouse && e.button == 0 && e.type == EventType.mouseDown && !draggingItem)
						{
							// If we start dragging, set dragging to true, set the dragged item to the item in that slot, 
							// empty the slot we dragged from (making it an empty slot)
							// v1.5: Icons are now dragged from the position they were clicked at. This creates
							// a smooth drag
							playerEquip = item.itemID;
							//Debug.LogWarning("Failed to load item icon for " + item.itemName + ". If you have added an icon be sure to restart the game!");

							
						}
						
						// Now let's check to see if we released said mouse button while dragging an item
						// Since all of this is wrapped in if (inventory[i].itemName != null), then this means this happens
						// When the dragged item is dropped on a slot that contains an item
						if (e.isMouse && e.type == EventType.mouseUp && draggingItem)
						{
							// v1.5:
							// If so, we'll make that slot's item equal to the item we were dragging
							// Also need to set the slot we dragged FROM to have the item in the slot we dropped the item on
							// Then set that we're not dragging an item
							// and set the dragged item to be empty
							Item tempItem = item;
							inventory[i] = draggedItem;
							draggedItem = tempItem;
						}
						
						// Now we're going to be checking to see if we're trying to use an item in a slot with the RMB.
						if (e.isMouse && e.type == EventType.mouseDown && e.button == 1)
						{
								// Go through each equip slot
								for (int h = 0; h < equippedItems.Length; h++)
								{
									// Find the appropriate slot type for the item that we right clicked
									// This handles placing the equipment in an empty slot
									if (item.itemType == equippedItems[h].itemType && equippedItems[h].itemName == null)
									{
										equippedItems[h] = item;
										inventory[i] = new Item();
									}
									// This one handles placing it in a slot that already contains an item
									// (swaps the right clicked item and the equipped item)
									else if (item.itemType == equippedItems[h].itemType)
									{
										Item tempItem = equippedItems[h];
										equippedItems[h] = item;
										inventory[i] = tempItem;
									}
								}
							
						}

						if (!draggingItem)
						{
							// Make the tooltip string equal to the generated tooltip for this item
							// And then set a bool to show the tooltip to true
							tooltipText = CreateTooltip(item);
							showTooltip = true;
						}
					} 
					// If the tooltip string is blank, don't bother showing a tooltip
					if (tooltipText == "")
						showTooltip = false;
				}
				// If the slot where the mouse is doesn't contain an item
				else 
				{
					// If we're dragging an item and we release the mouse button
					if (e.isMouse && e.type == EventType.mouseUp && draggingItem)
					{
						// If the mouse is hovering over an inventory slot when the mouse is release
						if (slotRect.Contains(e.mousePosition))
						{
							// We'll set that slot to be equal to the item that we were dragging
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				
				// Increase the value of the loop's index
				i++;
			}
		}

		Rect trashcan = new Rect (inventorySize.x + inventorySize.width - 40, inventorySize.y + inventorySize.height + 10, 40, 40);
		GUI.Box (trashcan,"X", skin.GetStyle("Inventory Empty Slot"));
		if (draggingItem && e.type == EventType.mouseUp && trashcan.Contains (e.mousePosition)) 
		{
			draggedItem = null;
			draggingItem = false;
		}
	}
	
	// v1.5:
	// Method that calculate the offset for the item icons when they're being dragged
	protected Vector2 GetIconCoordDifference(Vector2 m, Rect s)
	{
		// This gets the difference between the mouse's position and the slots screen coordinates. 
		// The result is the offset from the top left of the icon to where I clicked in local coordinates to the icon itself
		Vector2 diff = new Vector2 (m.x - s.x, m.y - s.y);
		return diff;
	}
	
	
	// A method we call to draw the contents of the item's tooltip
	private void DrawTooltip()
	{
		// First we have to calculate to see the height of the tooltip's content so we know what height to draw the tooltip box
		float tooltipHeight = skin.GetStyle("Inventory Tooltip").CalcHeight(new GUIContent(tooltipText), 200);
		// Now draw a box that follows that mouse's position at a width of 200 and a height of the result of the height calculation above
		// We also use a custom GUI Skin style for the tooltip like everything else
		GUI.Box(new Rect(Event.current.mousePosition.x + 10, Event.current.mousePosition.y, 200, tooltipHeight), tooltipText, skin.GetStyle("Inventory Tooltip"));
	}
	
	// A method for generating and returning a tooltip string for the item that we're hovering over
	private string CreateTooltip(Item item)
	{
		// First thing we do is reset the tooltip to a blank string just in case
		tooltipText = "";
		// ID of -1 represents a null item 
		// We'll then check to see what type of item it is to know what type of tooltip to generate (this is optional, you could just use a generic tooltip for everything)
		tooltipText += item.itemName + "\n" + item.itemDescription + "\n";
		
		// After we've generated the tooltip we'll return it back to where ever it was called
		return tooltipText;
	}
	
	// The method we use to add items from the database to our player's inventory
	public void AddItem(int itemID)
	{
		// Check to make sure we have fewer items in our inventory than slot
		if (inventory.Count > CountItems())
		{
			// First thing we do is loop through all of the items in the inventory
			for (int i = 0; i < database.items.Count; i++)
			{
				// We check each one to see if the ID of the database item and the ID of the requested item match
				if (itemID == database.items[i].itemID)
				{
					// Once we find a match, we have to go through each item in inventory to find an empty slot
					for(int j = 0; j < inventory.Count; j++)
					{
						// Check each one to see if it contains an item or not
						if (inventory[j].itemName == null)
						{
							// The first one that doesn't contain an item gets populated with the item we added
							inventory[j] = database.items[i];
							// We're done now, so break out and continue on
							break;
						}
					}
					
				}
			}
		} else {
			Debug.LogWarning ("Player inventory full. Your code for informing the player of this should go here!");
		}
	}
	
	// A method we use to remove a specific item and amount of the item from the inventory
	public void RemoveItem(int id, int amount)
	{
		// Initialize a temp variable for checking the count of item we removed 
		int itemCount = 0;
		
		// Go through each item in the player's inventory
		for (int i = 0; i < inventory.Count; i++)
		{
			// Find one that matches the id we passed to the method
			if (inventory[i].itemID == id && itemCount < amount)
			{
				// As long as we haven't removed more than we requested and the IDs match
				// remove another one
				inventory[i] = new Item();
				
				// Increase the count of items we've removed
				itemCount++;
			}
			// Break out once we have removed as many of that item as the method requested
			if (itemCount >= amount)
				break;
		}
	}
	
	// Count the amount of items in our inventory 
	int CountItems()
	{
		// Init an int to 0
		int itemCount = 0;
		foreach(Item item in inventory)
			if (item.itemName != null)
				itemCount++;
		return itemCount;
	}
	
	// Example method for handling the usage of Consumable items
	void UseOneTime(Item item, int slot)
	{
		// Method for using consumables.
		// An example for special effects per item would be the following switch statement
		switch(item.itemID)
		{
		case 0: // Enter effects for consumable if item ID = 0 (just examples)
		{
			Debug.LogWarning("INSERT CONSUMABLE EFFECT FOR ITEM: " + item.itemName);
			// Example: playerStats.IncreaseStat(STAT ID, BUFF DURATION, BUFF AMOUNT);
			break;
		}
		case 1: // Enter effects for consumable if item ID = 1 (just examples)
		{
			Debug.LogWarning("INSERT CONSUMABLE EFFECT FOR ITEM: " + item.itemName);
			// Example: playerStats.IncreaseStat(STAT ID, BUFF DURATION, BUFF AMOUNT);
			break;
		}
		}
		
		// Empty out the slot of the used item
		inventory[slot] = new Item();
		print (inventory[slot].itemID);
		print ("Used: " + item.itemName);
	}

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "JetPack") {
			AddItem(1);
			Destroy (coll.gameObject);
		}

		if (coll.gameObject.tag == "bomb" && character.hasBomb == false) {
			AddItem(2);
			character.hasBomb = true;
			Destroy (coll.gameObject);
		}

		if (coll.gameObject.tag == "Burger" && character.hasBurger == false) {
			AddItem(3);
			character.hasBurger = true;
			Destroy (coll.gameObject);
		}

		if (coll.gameObject.tag == "TM") {
			AddItem(4);
			character.hasTM = true;
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Gun") {
			AddItem(5);
			Destroy (coll.gameObject);
		}
	}

}









