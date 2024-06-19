using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private DialogueVoiceSystem voiceSystem;
        [SerializeField] private DialogueIconDisplay iconDisplay;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Queue<DialogueData> log = new Queue<DialogueData>();
        [SerializeField] private DialogueGroup currentDialogueGroup;
        [SerializeField] private int index;

        public event Action AfterDialogueComplete;

        private string dialogueTextTemp;
        private bool isWriting;
        private bool lastIconActive;
        private IEnumerator writeCoroutine;
        private IEnumerator actionCoroutine;
		private void Start()
		{
            // canvas.enabled = false;
            //ShowDialogueGroup(0);
		}
        public void ShowDialogueGroup(int index)
        {
            lastIconActive = true;
            log.Clear();
            currentDialogueGroup = DialogueDatabase.Instance.GetDialogueGroup(index);
            this.index = 0;
            ShowDialogue(currentDialogueGroup.dialogues[this.index]);
            canvas.enabled = true;
            GameManager.Instance.HideGameUI();
        }
        public void Skip()
        {
            EndOfDialogueGroup();
        }
        public void ShowDialogue(DialogueData data)
        {
            //dialogueText.text = data.dialogueText;
            writeCoroutine = WriteDialogue(data.dialogueText);
            StartCoroutine(writeCoroutine);
            nameText.text = data.name;
           
            if (data.actions.Count > 0)
            {
                actionCoroutine = ExecuteActions(data.actions);
                StartCoroutine(actionCoroutine);
            }
            if (data.actionAtOnce.Count > 0)
            {
                ExecuteActionAtOnce(data.actionAtOnce);
            }
            if (data.sound != null)
            {
                voiceSystem.PlayVoice(data.sound);
            }
            if (data.name == "-") return;
            SetIconPosition(data.talkingPosition,data.icon);
        }
        private void SetIconPosition(IconPosition pos,Sprite sprite)
        {
            iconDisplay.ActivateIcon(pos);
            iconDisplay.ChangeIconSprite(pos, sprite);
        }
        private void ExecuteActionAtOnce(List<DialogueAction> actions)
        {
            int index = 0;
            while (index < actions.Count)
            {
                CheckAction(actions[index]);
                index++;
            }
        }
        private IEnumerator ExecuteActions(List<DialogueAction> actions)
        {
            int index = 0;
            while (index < actions.Count)
            {
                CheckAction(actions[index]);
                yield return new WaitForSeconds(ActionDelay(actions[index]));
                index++;
            }
        }
        private float ActionDelay(DialogueAction action)
        {
            switch (action)
            {
                case DialogueAction.ShakeLow:
                    return 0.25f;
                case DialogueAction.ShakeMid:
                    return 0.5f;
                case DialogueAction.ShakeHard:
                    return 1;
            }
            return 2;
        }
        public void CheckAction(DialogueAction action)
        {
            switch (action)
            {
                case DialogueAction.ShakeLow:
                    GameManager.Instance.CameraShake(0.25f,5);
                    break;
                case DialogueAction.ShakeMid:
                    GameManager.Instance.CameraShake(0.5f, 10);
                    break;
                case DialogueAction.ShakeHard:
                    GameManager.Instance.CameraShake(1, 15);
                    break;
                case DialogueAction.HideLeftIcon:
                    iconDisplay.DeactivateIcon(IconPosition.left);
                    break;
                case DialogueAction.HideRightIcon:
                    iconDisplay.DeactivateIcon(IconPosition.right);
                    break;
                case DialogueAction.HideMiddleIcon:
                    iconDisplay.DeactivateIcon(IconPosition.middle);
                    break;
                case DialogueAction.ShowLeftIcon:
                    iconDisplay.ActivateIcon(IconPosition.left);
                    break;
                case DialogueAction.ShowRightIcon:
                    iconDisplay.ActivateIcon(IconPosition.right);
                    break;
                case DialogueAction.ShowMiddleIcon:
                    iconDisplay.ActivateIcon(IconPosition.middle);
                    break;
                case DialogueAction.DarkenLeftIcon:
                    iconDisplay.DarkenIcon(IconPosition.left);
                    break;
                case DialogueAction.DarkenRightIcon:
                    iconDisplay.DarkenIcon(IconPosition.right);
                    break;
                case DialogueAction.DarkenMiddleIcon:
                    iconDisplay.DarkenIcon(IconPosition.middle);
                    break;
                case DialogueAction.LightenLeftIcon:
                    iconDisplay.LightenIcon(IconPosition.left);
                    break;
                case DialogueAction.LightenRightIcon:
                    iconDisplay.LightenIcon(IconPosition.right);
                    break;
                case DialogueAction.LightenMiddleIcon:
                    iconDisplay.LightenIcon(IconPosition.middle);
                    break;
            }
        }
        public IEnumerator WriteDialogue(string text)
        {
            dialogueText.text = "";
            int charIndex = 0;
            isWriting = true;
            dialogueTextTemp = text;
            while (charIndex <= text.Length)
            {
                yield return new WaitForSeconds(0.05f);
                dialogueText.text = text.Substring(0,charIndex);
                charIndex++;
            }
            isWriting = false;
        }
        public void NextDialogue()
        {
            if (isWriting)
            {
                if (writeCoroutine != null)
                    StopCoroutine(writeCoroutine);
                if (actionCoroutine != null)
                    StopCoroutine(actionCoroutine);
                dialogueText.text = dialogueTextTemp;
                isWriting = false;
            }
            else
            {
                if (index < currentDialogueGroup.dialogues.Count - 1)
                {
                    log.Enqueue(currentDialogueGroup.dialogues[this.index]);
                    index++;
                    ShowDialogue(currentDialogueGroup.dialogues[index]);
                }
                else
                {
                    EndOfDialogueGroup();
                }
            }
        }
        public void EndOfDialogueGroup()
        {
            Debug.Log("End Of Dialogue");
            voiceSystem.StopVoice();
            AfterDialogueComplete?.Invoke();
            AfterDialogueComplete = null;
            Hide();
            GameManager.Instance.ShowGameUI();
        }
        public void Hide()
        {
            canvas.enabled = false;
        }
        public void AddLog(DialogueData data)
        {

        }
        public void ShowLog()
        { 
            
        }
    }
}
