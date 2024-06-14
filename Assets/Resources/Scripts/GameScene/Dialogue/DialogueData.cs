using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1)]
    public class DialogueData : ScriptableObject
    {
        public int id;
        [TextArea]
        public string dialogueText;
        public string name;
        public Sprite icon;
        public AudioClip sound;
        public List<DialogueAction> actions;
        public List<DialogueAction> actionAtOnce;
        public IconPosition talkingPosition;

    }
}
