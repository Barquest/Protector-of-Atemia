using System;
using UnityEngine;
namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LevelSelectButton : MonoBehaviour
    {
      
        public bool isLocked { get;private set; }
        [SerializeField] private LevelSelectData levelData;
        [SerializeField] private Material mat;

        [SerializeField] private int index;
        public event Action<LevelSelectButton> OnClick;

		private void Start()
		{
            Setup();
            Lock();
		}
        public void SetIndex(int data)
        {
            index = data;
        }
        public int GetIndex()
		{
            return index;
		}
		private void OnMouseDown()
		{
            //PlayLevel();
            OnClick?.Invoke(this);
		}
		public void Setup()
        {
            mat = GetComponent<Renderer>().material;
        }
        public void Unlock()
        {
            isLocked = false;
            mat.color = Color.white;
        }
        public void Lock()
        {
            isLocked = true;
            mat.color = Color.grey;
        }
        public void SetLevel(LevelSelectData value)
        {
            levelData = value;
        }
        public LevelSelectData GetLevelData()
        {
            return levelData;
        }
       
    }
}
