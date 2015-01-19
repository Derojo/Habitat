using UnityEngine;
using System.Collections;

public class NPC_Dialog : MonoBehaviour {

	public string QuestTitle;
	public string[] CallButtons;
	public string[] QuestStates;
	public GUISkin habitatSkin;
	
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

		if (QuestManager.DisplayQuestlog) {
			GUI.Box(centered, QuestTitle);
			if(!QuestManager.ActiveQuest) {
				GUI.Label(centered, QuestStates [0]);
				// Draw acceptbutton
				if( GUI.Button (new Rect (Screen.width / 3, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [0])) {
					// Activate quest if we click the right button
					Debug.Log ("Activate Quest");
					QuestManager.ActiveQuest = true;
					QuestManager.DisplayQuestlog = false;
				}
				if(GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 7, Screen.height / 7), CallButtons [1])) {
					QuestManager.DisplayQuestlog = false;
				}
			} 
			else if (QuestManager.ActiveQuest && !QuestManager.CompleteQuest) {
				GUI.Label(centered, QuestStates [1]);
			}
		}
	}

	void OnTriggerEnter() {
		QuestManager.DisplayQuestlog = true;
	}

	void OnTriggerExit() {
		QuestManager.DisplayQuestlog = false;
	}

}
