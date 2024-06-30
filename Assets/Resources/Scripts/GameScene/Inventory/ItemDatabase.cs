using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ItemDatabase : MonoBehaviour
    {
		public static ItemDatabase Instance;

        [SerializeField] private List<ItemData> dataList = new List<ItemData>();
        [SerializeField] private Dictionary<int,ItemData> dataDictionary = new Dictionary<int,ItemData>();

		private void Awake()
		{
			Instance = this;
			SetDatabase();
		}
		private void SetDatabase()
		{
			for (int i = 0; i < dataList.Count; i++)
			{
				dataDictionary[i] = dataList[i];
			}
		}
		public ItemData GetData(int id)
		{
			return dataDictionary[id];
		}
		public GiftData GetGiftData(int id)
		{
			return (GiftData)dataDictionary[id];
		}
		public AccessoriesData GetAccessoriesData(int id)
		{
			return (AccessoriesData)dataDictionary[id];
		}
		public ConsumableData GetConsumableData(int id)
		{
			return (ConsumableData)dataDictionary[id];
		}
		public EquipmentData GetEquipmentData(int id)
		{
			return (EquipmentData)dataDictionary[id];
		}
		public Item GetItem(int id)
		{
			Item item = new Item(GetData(id));
			return item;
		}
		public Consumable GetConsumable(int id,int count)
		{
			Consumable item = new Consumable(GetData(id), count);
			return item;
		}
		public Accessories GetAccessories(int id)
		{
			Accessories item = new Accessories(GetAccessoriesData(id));
			return item;
		}
		public Gift GetGift(int id)
		{
			Gift item = new Gift(GetGiftData(id));
			return item;
		}
		public Equipment GetEquipment(int id)
		{
			EquipmentData data = GetEquipmentData(id);
			Equipment item = new Equipment(data);
			return item;
		}
	}
}
