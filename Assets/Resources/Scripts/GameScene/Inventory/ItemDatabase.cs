using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] private List<ItemData> dataList = new List<ItemData>();
        [SerializeField] private Dictionary<int,ItemData> dataDictionary = new Dictionary<int,ItemData>();

		private void Awake()
		{
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
		public Item Get(int id,int count)
		{
			Item item = new Item(GetData(id), count);
			return item;
		}
	}
}
