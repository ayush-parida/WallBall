using UnityEngine;
using System.Collections;

// This script handles all references made to other MonoBehaviourScripts

public class ManagerBase : MonoBehaviour {


	private SoundManager _soundManager;
	public SoundManager soundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = FindObjectOfType<SoundManager> ();
			
			return _soundManager;
		}
	}

}
