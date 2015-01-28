using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
	

	public GameObject QuestComplete = null;
	public Color QuestCompleteTitleColor;
	private bool activeQuestCompleteHUD = false;
	private GameObject _QuestComplete;

	void Update() {
		Debug.Log (Library.habitat.questData.activeQuest);
		Debug.Log (Library.habitat.questData.completeQuest);
		if (HabitatState.statusTracking == HabitatState.StatusTracking.Tracking) {
			if (Library.habitat.questData.activeQuest) {
				GameObject.Find ("QuestTitle").GetComponent<GUIText> ().text = Library.habitat.questData.currentQuest;
				GameObject.Find ("QuestPartsFound").GetComponent<GUIText> ().text = Library.habitat.questData.partsFound + " / " + Library.habitat.questData.questParts.Count;
			}
			if (Library.habitat.questData.completeQuest) {
				GameObject.Find ("QuestTitle").GetComponent<GUIText> ().text = Library.habitat.questData.currentQuest+" voltooid!";
				GameObject.Find ("QuestTitle").GetComponent<GUIText>().material.color = QuestCompleteTitleColor;
			}

			if (Library.habitat.questData.completeQuest && !QuestItem.sceneCutPlaying ) {
				if (!activeQuestCompleteHUD) {
					_QuestComplete = Instantiate (QuestComplete) as GameObject;
					_QuestComplete.transform.parent = GameObject.Find ("QuestTrack").transform;
					_QuestComplete.name = QuestComplete.name;
					_QuestComplete.transform.localPosition = QuestComplete.transform.position;
					_QuestComplete.transform.localScale = QuestComplete.transform.lossyScale;
					activeQuestCompleteHUD = true;
				}
			}
			if(!Library.habitat.questData.completeQuest) {
				if (activeQuestCompleteHUD) {
					Destroy (_QuestComplete);
					activeQuestCompleteHUD = false;
				}
			}
		}
	}
}
