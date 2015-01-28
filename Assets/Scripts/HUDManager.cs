﻿using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
	

	public GameObject QuestComplete = null;
	public Color QuestCompleteTitleColor;
	public GUISkin skin;
	private bool activeQuestCompleteHUD = false;
	private GameObject _QuestComplete;

	void Update() {
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

	void OnGUI()
	{
		float curHealth = Library.habitat.playerData.curHealth;
		float maxHealth = GameObject.Find ("PlayerSetup").GetComponent<PlayerController> ().maxHealth;
		float curhealthBar = Screen.width / 4 * (Library.habitat.playerData.curHealth / maxHealth);

		GUI.skin = skin;
		GUI.skin.GetStyle("playerHealthText").fontSize = Screen.width / 45;
		GUI.Box(new Rect(10, 15, Screen.width / 4, Screen.width / 30 ), "", GUI.skin.GetStyle("playerHealthBar"));
		GUI.Box(new Rect(10, 15, curhealthBar, Screen.width / 30 ), "", GUI.skin.GetStyle("playerCurHealthBar"));
		GUI.Label(new Rect(10, 15, Screen.width / 4, Screen.width / 30 ), curHealth + "/" + maxHealth, GUI.skin.GetStyle("playerHealthText"));

	}
}