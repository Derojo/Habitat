using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuestData {
	
//	public  bool DisplayQuestlog { get { return _displayQuestLog; } set { _displayQuestLog = value; } }

	public bool displayQuestlog;
	public bool activeQuest;
	public bool completeQuest;
	public string currentQuest;
	public List<string> questParts;
	public Dictionary<string, bool> questPartsFound;
	public int partsFound;
		
	public QuestData() {
		this.displayQuestlog = false;
		this.activeQuest = false;
		this.completeQuest = false;
		this.currentQuest = "";
		this.questParts = new List<string> ();
		this.questPartsFound =  new Dictionary<string, bool>();
		this.partsFound = 0;
	}

}