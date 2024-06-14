using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "DialogueGroup", menuName = "ScriptableObjects/DialogueGroup", order = 1)]

    public class DialogueGroup : ScriptableObject
    {
        public int id;
        public List<DialogueData> dialogues;
    }
}
