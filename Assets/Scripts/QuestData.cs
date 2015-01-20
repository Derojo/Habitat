using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestData {
	
//	public  bool DisplayQuestlog { get { return _displayQuestLog; } set { _displayQuestLog = value; } }

	public bool displayQuestlog;
	public bool activeQuest;
	public bool completeQuest;
		
	public QuestData() {
		this.displayQuestlog = false;
		this.activeQuest = false;
		this.completeQuest = false;
	}

}