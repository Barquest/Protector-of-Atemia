using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SkillUIDisplay : UIController
    {
		[SerializeField] private SkillManager skillManager;
        [SerializeField] private IconDisplay korvinDisplay;
        [SerializeField] private IconDisplay aronaDisplay;

		private void Start()
		{
			korvinDisplay.DisplayUI(0);
			aronaDisplay.DisplayUI(0);
			korvinDisplay.ForceActivate();

			korvinDisplay.OnClick += skillManager.UseKorvinSkill;
			aronaDisplay.OnClick += skillManager.UseAronaSkill;

			skillManager.OnAronaAttackSuccess += DisplayAronaIconUI;
			skillManager.OnKorvinDefendSuccess += DisplayKorvinIconUI;
			skillManager.OnAronaBackToPosition += ActivateAronaIconUI;
			skillManager.OnAronaStartAttack += DisactivateAronaIconUI;
		}
		private void DisplayAronaIconUI(float val)
		{
			aronaDisplay.DisplayUI(val);
		}
		private void DisplayKorvinIconUI(float val)
		{
			korvinDisplay.DisplayUI(val);
		}
		private void DisactivateAronaIconUI()
		{
			aronaDisplay.Disactivate();
		}
		private void ActivateAronaIconUI()
		{
			aronaDisplay.Activate();
		}



	}
}
