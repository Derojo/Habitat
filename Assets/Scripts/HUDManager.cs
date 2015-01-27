using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
	

	public GameObject QuestComplete = null;
	private bool activeQuestCompleteHUD = false;
	void Update() {
		if (Library.habitat.questData.activeQuest) {
			GameObject.Find ("QuestTitle").GetComponent<GUIText> ().text = Library.habitat.questData.currentQuest;
			GameObject.Find ("QuestPartsFound").GetComponent<GUIText> ().text = Library.habitat.questData.partsFound + " / " + Library.habitat.questData.questParts.Count;
		}
		if (Library.habitat.questData.completeQuest) {
			if(!activeQuestCompleteHUD) {
				GameObject _QuestComplete = Instantiate (QuestComplete) as GameObject;
				_QuestComplete.transform.parent = GameObject.Find("QuestTrack").transform;
				activeQuestCompleteHUD = true;
			}
		}
	}
}
