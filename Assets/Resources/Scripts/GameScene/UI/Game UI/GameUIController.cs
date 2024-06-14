using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class GameUIController : UIController
    {
		[SerializeField] private TextMeshProUGUI moneyText;
		[SerializeField] private TextMeshProUGUI killCountText;
		[SerializeField] private CaravanHealthController caravanHealthController;
		[SerializeField] private GameManager gameManager;

		protected override void Awake()
		{
			base.Awake();
			gameManager.StageGameData.OnKillCountChanged += UpdateKillCountValue;
			gameManager.OnCaravanHealthChanged += UpdateCaravanHealthValue;
		}
		private void OnDestroy()
		{
			gameManager.StageGameData.OnKillCountChanged -= UpdateKillCountValue;
			gameManager.OnCaravanHealthChanged -= UpdateCaravanHealthValue;
		}
		protected virtual void Start()
		{
			
			if (GameManager.Instance.isDebug)
			{
				return;
			}
			UpdateMoneyValue(GlobalGameManager.Instance.GetPlayerData().money);
			UpdateKillCountValue(0);
			UpdateCaravanHealthValue(3);
			
		}
		public void UpdateMoneyValue(int value)
		{
			moneyText.text = LanguageManager.Instance.GetMainMenuText("money")  + value;
		}
		public void UpdateKillCountValue(int value)
		{
			killCountText.text = LanguageManager.Instance.GetGameplayText("kill") + value;
		}
		public void UpdateCaravanHealthValue(int value)
		{
			caravanHealthController.DisplayCurrentHealth(value);
			Debug.Log("Update Caravan Health Value");
		}

	}
}
