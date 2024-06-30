using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class CharacterInventory 
    {
        public Sprite characterIcon;
        public Equipment equipment;
        public Accessories accessory;

        public void EquipEquipment(Equipment item)
        {
            equipment = item;
        }
        public void EquipAccessory(Accessories item)
        {
            accessory = item;
        }
        public Equipment GetEquipment()
        {
            return equipment;
        }
        public Accessories GetAccessories()
        {
            return accessory;
        }
    }
}
