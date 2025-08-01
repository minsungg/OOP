using System.Collections;
using UnityEngine;

namespace Item
{
    public abstract class ItemAction : ScriptableObject
    {
        // Will be executed once the item is obtained within the inventory.
        public virtual void ItemAcquisitionAction(Inventory.Inventory userInventory, int itemIndex) { }

        // Active and UnActive mean are for when a character has this item actively in use and selected
        // An example of this is displaying the grid when selecting seeds.
        public virtual void ItemActiveAction(Inventory.Inventory userInventory, int itemIndex) { }
        public virtual void ItemUnactiveAction(Inventory.Inventory userInventory, int itemIndex) { }

        public virtual void ItemRemoveAction(Inventory.Inventory userInventory, int itemIndex) { }

        // This needs to return try in order to be allowed to have a use action
        public abstract IEnumerator ItemUseAction(Inventory.Inventory userInventory, int itemIndex);
        public abstract bool ItemUseCondition(Inventory.Inventory userInventory, int itemIndex);
    }
}
