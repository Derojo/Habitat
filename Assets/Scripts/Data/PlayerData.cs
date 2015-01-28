using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
	
	//	public  bool DisplayQuestlog { get { return _displayQuestLog; } set { _displayQuestLog = value; } }
	
	public float curHealth;

	
	public PlayerData() {
		this.curHealth = 100;
	}
	
}