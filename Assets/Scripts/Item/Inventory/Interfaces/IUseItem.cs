
namespace Item.Inventory.Interfaces
{
    public interface IUseItem
    {
        void OnUseItem(int index, ItemData data, int amount);
    }
}