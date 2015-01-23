using UnityEngine;
using System.Collections;

public class NPC_Dialog : MonoBehaviour {
	
	public string questTitle;
	public string[] callButtons;
	public string[] currentQuest;
	public string[] questStates;
	public GameObject[] flowerParts;
	public GameObject pollinator;
	public string placeUnder = "";
	public GUISkin habitatSkin;
	public GameObject questmark;
	public GameObject questmarkFinished;

	private GameObject _spawnPart;

	void Awake() {
		if (Library.habitat.questData.activeQuest) {
			// Place exclamation mark above NPC
			questmark.SetActive (false);
			// Place flower parts
			spawnFlowerParts();
		} else if (Library.habitat.questData.completeQuest) {
			// Place question mark above NPC
			GameObject questFinished = Instantiate(questmarkFinished, questmark.transform.position, questmark.transform.rotation) as GameObject;
			questFinished.transform.parent = GameObject.Find("Quest").transform;
		}
	}
	
	void OnGUI() {
		Rect centered = new Rect (Screen.width / 6, Screen.height / 6, Screen.width / 1.5f, Screen.height / 1.5f);
		GUI.skin = habitatSkin;

//		GUI.Button (new Rect (Screen.width / 3, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), callButtons [0]);
//		GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), callButtons [1]);

		if (Library.habitat.questData.displayQuestlog) {
			GUI.Box(centered, questTitle);
			if(!Library.habitat.questData.activeQuest) {
				GUI.Label(centered, questStates [0]);
				// Draw acceptbutton
				if( GUI.Button (new Rect (Screen.width / 3, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), callButtons[0])) {
					// Activate quest if we click the right button
					Library.habitat.questData.activeQuest = true;
					Library.habitat.questData.displayQuestlog = false;
					// Remove questmark
					questmark.SetActive(false);
					// Add linked parts to the scene
					spawnFlowerParts ();
					// Add HUD progress
					QuestManager.setQuestTrackTitle(questTitle);
					QuestManager.activateQuestHUD();
				}
				if(GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), callButtons[1])) {
					Library.habitat.questData.displayQuestlog = false;
				}
			} 
			else if (Library.habitat.questData.activeQuest && !Library.habitat.questData.completeQuest) {
				GUI.Label(centered, questStates [1]);
			}
			else if (Library.habitat.questData.activeQuest && Library.habitat.questData.completeQuest) {
				// Parts are found give player a new quest
				GUI.Label(centered, questStates [1]);
			}
		}
	}

	void OnTriggerEnter() {
		Library.habitat.questData.displayQuestlog = true;
	}

	void OnTriggerExit() {
		Library.habitat.questData.displayQuestlog = false;
	}
	
	void spawnFlowerParts() {
		foreach(GameObject Part in this.flowerParts) {
			if(Part == null) {
				continue;
			}
			if(!Library.habitat.questData.questParts.Contains(Part.name)) {
				Library.habitat.questData.questParts.Add(Part.name);
			}

		}
	}

}
