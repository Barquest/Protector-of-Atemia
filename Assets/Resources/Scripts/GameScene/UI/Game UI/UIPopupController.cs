using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	[System.Serializable]
	public class PopupData
	{
		[SerializeField] private string title;
		[SerializeField] private string description;
		[SerializeField] private bool isCancel;
		[SerializeField] private List<Item> items = new List<Item>();
		public event Action OnOkay;
		public event Action OnCancel;
		public PopupData(string title,string description,Action OnOkay,Action OnCancel,List<Item> items)
		{
			this.title = title;
			this.description = description;
			this.OnOkay = OnOkay;
			this.OnCancel = OnCancel;
			if (OnCancel == null)
			{
				isCancel = false;
			}
			else {
				isCancel = true;
			}
			if (items != null)
			{
				this.items = items;
			}
			else {
				this.items = null;
			}
		}
		public PopupData(string title, string description, Action OnOkay, List<Item> items)
		{
			this.title = title;
			this.description = description;
			this.OnOkay = OnOkay;
			this.OnCancel = null;
			isCancel = false;
			if (items != null)
			{
				this.items = items;
			}
			else
			{
				this.items = null;
			}
		}
		public void Okay()
		{
			OnOkay?.Invoke();
		}
		public void Cancel()
		{
			OnCancel?.Invoke();
		}
		public bool GetCancel()
		{
			return isCancel;
		}
		public string GetTitle()
		{
			return title;
		}
		public string GetDescription()
		{
			return description;
		}
		public List<Item> GetItems()
		{
			return items;
		}
	}
    public class UIPopupController : UIController
    {
        public static UIPopupController Instance;
		[SerializeField] private Queue<PopupData> popupQueue = new Queue<PopupData>();
		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI descriptionText;
		[SerializeField] private Transform itemsParent;
		[SerializeField] private GameObject cancelButton;
		[SerializeField] private ItemDisplay itemInfoPrefab;

		public event Action OnOkay;
		public event Action OnCancel;

		private PopupData currentPopup;
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
		public void AddPopup(PopupData data)
		{
			popupQueue.Enqueue(data);
			if (!canvas.isActiveAndEnabled)
			{
				CheckPopup();
			}

		}
		public void ShowPopup(PopupData data)
		{
			AudioManager.Instance.PlaySfx("Popup");
			currentPopup = data;
			titleText.text = data.GetTitle();
			descriptionText.text = data.GetDescription();
			foreach (Transform t in itemsParent)
			{
				Destroy(t.gameObject);
			}
			
			if (data.GetCancel())
			{
				cancelButton.SetActive(true);
			}
			else {
				cancelButton.SetActive(false);
			}
			if (data.GetItems() != null)
			{
				List<Item> items= data.GetItems();
				for (int i = 0; i < items.Count; i++)
				{
					ItemDisplay itemInfo = Instantiate(itemInfoPrefab, itemsParent);
					itemInfo.SetData(items[i]);
				}
			}
		}
		public void CheckPopup()
		{
			if (popupQueue.Count > 0)
			{
				Show();
				PopupData pop = popupQueue.Dequeue();
				ShowPopup(pop);
			}
			else {
				Hide();
			}
		}
		public void Okay()
		{
			AudioManager.Instance.PlaySfx("Click");
			currentPopup.Okay();
			currentPopup = null;
			CheckPopup();
		}
		public void Cancel()
		{
			AudioManager.Instance.PlaySfx("Click");
			currentPopup.Cancel();
			currentPopup = null;
			CheckPopup();
		}
	}
}
