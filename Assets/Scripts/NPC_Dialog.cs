using UnityEngine;
using System.Collections;

public class NPC_Dialog : MonoBehaviour {

	public string QuestTitle;
	public string[] CallButtons;
	public string[] QuestStates;
	public GUISkin habitatSkin;
	public GameObject questmark;

	void Awake() {
		if (Library.habitat.questData.activeQuest) {
			questmark.SetActive(false);
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		Rect centered = new Rect (Screen.width / 6, Screen.height / 6, Screen.width / 1.5f, Screen.height / 1.5f);
		GUI.skin = habitatSkin;

//		GUI.Button (new Rect (Screen.width / 3, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [0]);
//		GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [1]);

		if (Library.habitat.questData.displayQuestlog) {
			GUI.Box(centered, QuestTitle);
			if(!Library.habitat.questData.activeQuest) {
				GUI.Label(centered, QuestStates [0]);
				// Draw acceptbutton
				if( GUI.Button (new Rect (Screen.width / 3, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [0])) {
					// Activate quest if we click the right button
					Library.habitat.questData.activeQuest = true;
					Library.habitat.questData.displayQuestlog = false;
					// Remove questmark
					questmark.SetActive(false);
				}
				if(GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [1])) {
					Library.habitat.questData.displayQuestlog = false;
				}
			} 
			else if (Library.habitat.questData.activeQuest && !Library.habitat.questData.completeQuest) {
				GUI.Label(centered, QuestStates [1]);
			}
		}
	}

	void OnTriggerEnter() {
		Library.habitat.questData.displayQuestlog = true;
	}

	void OnTriggerExit() {
		Library.habitat.questData.displayQuestlog = false;
	}

}
