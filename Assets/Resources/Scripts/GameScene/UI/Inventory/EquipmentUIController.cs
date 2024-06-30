using System;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class EquipmentUIController : UIController
	{
		public event Action OnClose;

		[SerializeField] private EquipmentInfoUI itemInfo;
		[SerializeField] private int characterIndexSelected;
		[SerializeField] private int itemIndexSelected;
		[SerializeField] private Transform inventoryParent;
		[SerializeField] private ItemDisplay itemDisplayPrefab;
		[SerializeField] private int itemCountInScreen = 24;
		[SerializeField] private List<ItemDisplay> itemList = new List<ItemDisplay>();
		[SerializeField] private Accessories equipmentSelected;
		[SerializeField] private CharacterDisplayController characterDisplayController;
		[SerializeField] private CharacterEnum currentCharacterSelected;

		[SerializeField] GameObject singleIteminfo;
		[SerializeField] GameObject doubleIteminfo;

		[SerializeField] EquipmentInfoUI itemInfoSingle;
		[SerializeField] EquipmentInfoUI itemInfoDoubleLeft;
		[SerializeField] EquipmentInfoUI itemInfoDoubleRight;

		[SerializeField] List<Item> currentDisplay = new List<Item>();

		int lastEquippedIndex;

		protected virtual void Start()
		{
			itemInfo.OnUse += EquipSelected;
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
			itemIndexSelected = -1;
			characterDisplayController.DisplayCharacter(CharacterDatabase.Instance.GetCharacterData(characterIndexSelected).prefab);
			SetUpInventoryEmpty();
			UpdatePlayerInventoryDisplay();
		}
		public void ChangeCharacterIndex()
		{
			if (characterIndexSelected == 0)
			{
				characterIndexSelected = 1;
				currentCharacterSelected = CharacterEnum.Korvin;
				characterDisplayController.DisplayCharacter(CharacterDatabase.Instance.GetCharacterData(characterIndexSelected).prefab);
			}
			else
			{
				characterIndexSelected = 0;
				currentCharacterSelected = CharacterEnum.Arona;
				characterDisplayController.DisplayCharacter(CharacterDatabase.Instance.GetCharacterData(characterIndexSelected).prefab);
			}
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
				display.OnClick += CheckEquipmentInfo;
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
			Debug.Log("UpdatePlayerInventoryDisplay");

			if (GetInventory().Count != 0)
			{
				
				Debug.Log("Inventory diatas 0");
				List<Item> playerItems = GetInventory();
				currentDisplay.Clear();
				int j = 0;
				for (int i = 0; i < playerItems.Count; i++)
				{
					if (playerItems[i].itemType == ItemType.accessories)
					{
						Accessories acc = (Accessories)playerItems[i];
						if (acc != null)
						{
							if (acc.characterUse == currentCharacterSelected)
							{
								itemList[j].SetData(playerItems[i]);
								currentDisplay.Add(playerItems[i]);
								j++;
							}
						}
					}
				}
			}
			CheckEquipmentEquipped();
		}
		private void CheckEquipmentEquipped()
		{
			singleIteminfo.SetActive(true);
			doubleIteminfo.SetActive(false);
			if (currentCharacterSelected == CharacterEnum.Arona)
			{
				Accessories acc = GlobalGameManager.Instance.GetPlayerData().aronaItems.GetAccessories();
				itemInfo.Info(acc);
				if (acc !=null)
					characterDisplayController.EquipWeapon(GlobalGameManager.Instance.GetPlayerData().aronaItems.accessory);
			}
			else
			{
				Accessories acc = GlobalGameManager.Instance.GetPlayerData().korvinItems.GetAccessories();
				itemInfo.Info(acc);
				if (acc != null)
					characterDisplayController.EquipWeapon(GlobalGameManager.Instance.GetPlayerData().korvinItems.accessory);
			}
			bool isEmpty;
			Accessories accEquipped = null;
			if (currentCharacterSelected == CharacterEnum.Arona)
			{
				if (GlobalGameManager.Instance.GetPlayerData().aronaItems != null)
				{
					accEquipped = GlobalGameManager.Instance.GetPlayerData().aronaItems.accessory;
					isEmpty = false;
				}
				else
				{
					isEmpty = true;
				}
			}
			else
			{
				if (GlobalGameManager.Instance.GetPlayerData().korvinItems != null)
				{
					accEquipped = GlobalGameManager.Instance.GetPlayerData().korvinItems.accessory;
					isEmpty = false;
				}
				else
				{
					isEmpty = true;
				}
			}
			if (accEquipped != null)
			{
				for (int i = 0; i < currentDisplay.Count; i++)
				{

					if (currentDisplay[i].id == accEquipped.id)
					{
						itemList[i].SetEquipped();
					}
				}
			}
		}
		private void CheckEquipmentInfo(int index)
		{
			Debug.Log("CheckEquipmentInfo");
			Accessories acc = null;
			if (currentCharacterSelected == CharacterEnum.Arona)
			{
				acc = GlobalGameManager.Instance.GetPlayerData().aronaItems.GetAccessories();
			}
			else {
				acc = GlobalGameManager.Instance.GetPlayerData().korvinItems.GetAccessories();
			}
			if (acc != null)
			{
				Accessories target = (Accessories)itemList[index].GetData();
				if (acc.id == target.id)
				{
					singleIteminfo.SetActive(true);
					doubleIteminfo.SetActive(false);
					itemInfoSingle.Info(acc);
					if (itemIndexSelected >= 0)
					{
						itemList[itemIndexSelected].DisplayUnselect();
					}
					itemIndexSelected = index;
					itemList[itemIndexSelected].DisplaySelect();
				}
				else
				{
					singleIteminfo.SetActive(false);
					doubleIteminfo.SetActive(true);
					itemInfoDoubleLeft.Info(acc);
					if (itemIndexSelected >= 0)
					{
						itemList[itemIndexSelected].DisplayUnselect();
					}
					itemIndexSelected = index;
					itemList[itemIndexSelected].DisplaySelect();
					itemInfoDoubleRight.Info((Accessories)currentDisplay[index]);
				}
				/*
				if (index < currentDisplay.Count && index > -1)
				{
					if (itemIndexSelected >= 0)
					{
						itemList[itemIndexSelected].DisplayUnselect();
					}
					itemIndexSelected = index;
					itemList[itemIndexSelected].DisplaySelect();
					itemInfo.Info((Accessories)currentDisplay[index]);
				}
				else
				{
					if (itemIndexSelected >= 0)
					{
						itemList[itemIndexSelected].DisplayUnselect();
					}
					itemIndexSelected = -1;
					itemInfo.Info((Accessories)null);
				}*/
			}
			else
			{
				singleIteminfo.SetActive(false);
				doubleIteminfo.SetActive(true);
				itemInfoDoubleLeft.Info((Accessories)null);
				if (itemIndexSelected >= 0)
				{
					itemList[itemIndexSelected].DisplayUnselect();
				}
				itemIndexSelected = index;
				itemList[itemIndexSelected].DisplaySelect();
				itemInfoDoubleRight.Info((Accessories)currentDisplay[index]);
			}
		}
		public void EquipSelected()
		{
			if (itemIndexSelected >= 0)
			{
				if (currentCharacterSelected == CharacterEnum.Arona)
				{
					Accessories acc = (Accessories)itemList[itemIndexSelected].GetData();
					GlobalGameManager.Instance.GetPlayerData().aronaItems.EquipAccessory(acc);
					UpdatePlayerInventoryDisplay();
					CheckEquipmentInfo(itemIndexSelected);
				}
				else
				{
					Accessories acc = (Accessories)itemList[itemIndexSelected].GetData();
					GlobalGameManager.Instance.GetPlayerData().korvinItems.EquipAccessory(acc);
					UpdatePlayerInventoryDisplay();
					CheckEquipmentInfo(itemIndexSelected);
				}
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

