using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DialogueDatabase : MonoBehaviour
    {
        public static DialogueDatabase Instance;
        [SerializeField] private List<DialogueGroup> currentDialogue = new List<DialogueGroup>();
        [SerializeField] private List<DialogueGroup> dialogueIndonesia = new List<DialogueGroup>();
        [SerializeField] private List<DialogueGroup> dialogueEnglish = new List<DialogueGroup>();
        private Dictionary<int, DialogueGroup> currentDialogueDictionary = new Dictionary<int, DialogueGroup>();

		private void Awake()
		{
            Instance = this;
            Initialize();
        }
		public void ChangeLanguage()
        { 
        
        }
		private void Start()
		{
           
		}
		public void Initialize()
        {
            currentDialogue = dialogueIndonesia;
            
            for (int i = 0; i < currentDialogue.Count; i++)
            {
                currentDialogueDictionary[i] = currentDialogue[i];
            }
        }
        public DialogueGroup GetDialogueGroup(int index)
        {
            return currentDialogueDictionary[index];
        }
    }
}
