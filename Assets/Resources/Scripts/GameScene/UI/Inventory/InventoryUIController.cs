using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class InventoryUIController : UIController
    {
        public event Action OnClose;

		[SerializeField] private ItemInfoUI itemInfo;
		[SerializeField] private int indexSelected;
		[SerializeField] private Transform inventoryParent;
		[SerializeField] private ItemDisplay itemDisplayPrefab;
		[SerializeField] private int itemCountInScreen = 24;
		[SerializeField] private List<ItemDisplay> itemList = new List<ItemDisplay>();

		protected virtual void Start()
		{
			
			itemInfo.OnUse += UseItem;
		}
		private void OnDestroy()
		{
            OnClose = null;
		}
		public void BackToMenu()
        {
            OnClose?.Invoke();
        }
		public override void Show()
		{
			base.Show();
			indexSelected = -1;
			SetUpInventoryEmpty();
			UpdatePlayerInventoryDisplay();
		}
		public void SetUpInventoryEmpty()
		{
			itemList.Clear();
			foreach (Transform t in inventoryParent)
			{
				Destroy(t.gameObject);
			}
			for (int i = 0; i < itemCountInScreen; i++)
			{
				ItemDisplay display = Instantiate(itemDisplayPrefab, inventoryParent);
				display.SetEmptyData(i);
				display.OnClick += CheckItemInfo;
				itemList.Add(display);
			}
		}
		public void RefreshInventory()
		{
			EmptyAllSlot();
			UpdatePlayerInventoryDisplay();
		}
		public void EmptyAllSlot()
		{
			for (int i = 0; i < itemCountInScreen; i++)
			{
				itemList[i].SetEmptyData(i);
			}
		}
		public void UpdatePlayerInventoryDisplay()
		{
			if (GetInventory().Count != 0)
			{
				List<Item> playerItems = GlobalGameManager.Instance.GetPlayerData().Inventory;
				for (int i = 0; i < playerItems.Count; i++)
				{
					itemList[i].SetData(playerItems[i]);
				}
			}
			CheckItemInfo(indexSelected);
		}
		private void CheckItemInfo(int index)
		{
			if (index < GetInventory().Count && index > -1)
			{
				if (indexSelected >= 0)
				{
					itemList[indexSelected].DisplayUnselect();
				}
				indexSelected = index;
				itemList[indexSelected].DisplaySelect();
				itemInfo.Info(GetInventory()[index]);
			}
			else {
				if (indexSelected >= 0)
				{
					itemList[indexSelected].DisplayUnselect();
				}
				indexSelected = -1;
				itemInfo.Info(null);
			}
		}
		private void UseItem()
		{
			if (GetInventory()[indexSelected] != null)
			{
				GetPlayerData().Consume(indexSelected);
				RefreshInventory();
				CheckItemInfo(indexSelected);
			}
		}
		private PlayerData GetPlayerData()
		{
			return GlobalGameManager.Instance.GetPlayerData();
		}
		private List<Item> GetInventory()
		{
			return GlobalGameManager.Instance.GetPlayerData().Inventory;
		}
	}
}
