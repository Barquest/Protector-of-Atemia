using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField] public static DebugManager Instance;
		[SerializeField] private DebugUI debugUI;

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
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F2))
			{
				debugUI.Show();
				Debug.Log("Debug Show");
			}
		}
	}
}
