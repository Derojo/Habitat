using UnityEngine;
using System.Collections;

public class NPC_Dialog : MonoBehaviour {
	
	public string questTitle;
	public string[] callButtons;
	public string[] currentQuest;
	public string[] questStates;
	public GameObject[] flowerParts;
	public GameObject[] pollinator;
	public string placeUnder = "";
	public GUISkin habitatSkin;
	public GameObject questmark;
	public GameObject questmarkFinished;
	public AudioClip blop;

	// So we have a reference to the instanstiated questmarkFinished
	private GameObject _questFinished;
	private GameObject _spawnPart;

	void Awake() {
		if (Library.habitat.questData.activeQuest || Library.habitat.questData.completeQuest) {
			// Place exclamation mark above NPC
			questmark.SetActive (false);
		}
		if (Library.habitat.questData.plantComplete) {
			Destroy(transform.parent.gameObject);
		}
//		Library.habitat.questData.currentQuest = currentQuest [1];
	}
	
	void OnGUI() {
		// Dialog window
		Rect centered = new Rect (Screen.width / 6, Screen.height / 6, Screen.width / 1.5f, Screen.height / 1.5f);
		// Gui skin
		GUI.skin = habitatSkin;
		// Fix font size for every resolution
		GUI.skin.label.fontSize = Screen.width / 35;
		GUI.skin.box.fontSize = Screen.width / 25;
		GUI.skin.button.fontSize = Screen.width / 30;
		// Quest structure
		if (Library.habitat.questData.displayQuestlog) {
			// Draw Overlay
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"", GUI.skin.GetStyle("overlay"));
			// Draw Dialog window
			GUI.Box(centered, questTitle);

			if(!Library.habitat.questData.plantComplete) {
				// Start first quest [search_flowerparts]
				if(Library.habitat.questData.currentQuest == "" || Library.habitat.questData.currentQuest == currentQuest[0])
				{
					firstQuest();
					Debug.Log ("first Quest");
				}
				// Start second quest [search_pollinator]
				else if(Library.habitat.questData.currentQuest == currentQuest[1])
				{
					secondQuest();
					Debug.Log ("Second Quest");
				}
			}

		}
	}

	void OnLevelWasLoaded() {
		if (Library.habitat.questData.completeQuest) {
			// Place question mark above NPC
			_questFinished = Instantiate(questmarkFinished, questmark.transform.position, questmark.transform.rotation) as GameObject;
			_questFinished.transform.parent = GameObject.Find("Quest").transform;
			_questFinished.name = questmarkFinished.name;
		}
	}

	void OnTriggerEnter() {
		// play sound
		audio.PlayOneShot (blop);
		Library.habitat.questData.displayQuestlog = true;
	}

	void OnTriggerExit() {
		Library.habitat.questData.displayQuestlog = false;
	}

	void spawnParts(GameObject[] _selectedObjects) {
		foreach(GameObject Part in _selectedObjects) {
			if(Part == null) {
				continue;
			}
			if(!Library.habitat.questData.questParts.Contains(Part.name)) {
				Debug.Log (Part.name);
				Library.habitat.questData.questParts.Add(Part.name);
			}

		}
		QuestManager.spawnQuestObjectsOnLoad ();
	}

	// Search flower parts quest
	private void firstQuest() {
		Rect label = new Rect (Screen.width / 6, Screen.height / 3, Screen.width / 1.5f, Screen.height / 1.5f);
		// Quest is not active yet
		if(!Library.habitat.questData.activeQuest) {
			GUI.Label(label, questStates [0]);
			// Draw acceptbutton
			if( GUI.Button (new Rect (Screen.width/5, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[0])) {
				// Activate quest if we click the right button
				Library.habitat.questData.activeQuest = true;
				Library.habitat.questData.displayQuestlog = false;
				Library.habitat.questData.currentQuest = currentQuest[0];
				// Remove questmark
				questmark.SetActive(false);
				// Add linked parts to the scene
				// Add HUD progress
				QuestManager.activateQuestHUD(true);
				QuestManager.setQuestTrackTitle(questTitle);
				spawnParts(flowerParts);
			}
			// On close button
			if(GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[1])) {
				Library.habitat.questData.displayQuestlog = false;
			}
		} 
		// We are still doing the quest
		else if (Library.habitat.questData.activeQuest && !Library.habitat.questData.completeQuest) {
			GUI.Label(label, questStates [1]);
		}
//		// Player is done 
		else if (Library.habitat.questData.activeQuest && Library.habitat.questData.completeQuest) {
			// Parts are found give player a new quest
			GUI.Label(label, questStates [2]);
			// Complete quest
			if( GUI.Button (new Rect (Screen.width/2 -90, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[2])) {
				Destroy (_questFinished);
				questmark.SetActive (true);
				// reset questdata
				QuestManager.resetQuestData();
				// Set new quest
				Library.habitat.questData.currentQuest = currentQuest[1];
			}
		}
	}
	// Search pollinator
	private void secondQuest() {
		Rect label = new Rect (Screen.width / 6, Screen.height / 3, Screen.width / 1.5f, Screen.height / 1.5f);
		// Quest is not active yet
		if(!Library.habitat.questData.activeQuest) {
			GUI.Label(label, questStates [3]);
			// Draw acceptbutton
			if( GUI.Button (new Rect (Screen.width/5, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[0])) {
				// Activate quest if we click the right button
				Library.habitat.questData.activeQuest = true;
				Library.habitat.questData.displayQuestlog = false;
				// Remove questmark
				questmark.SetActive(false);
				// Add linked parts to the scene
				spawnParts(pollinator);
				// Add HUD progress
				QuestManager.activateQuestHUD();
				QuestManager.setQuestTrackTitle(questTitle);
				
			}
			// On close button
			if(GUI.Button (new Rect ((Screen.width /100) * 55, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[1])) {
				Library.habitat.questData.displayQuestlog = false;
			}
		}
		// We are still doing the quest
		else if (Library.habitat.questData.activeQuest && !Library.habitat.questData.completeQuest) {
			GUI.Label(label, questStates [4]);
		}
		else if (Library.habitat.questData.activeQuest && Library.habitat.questData.completeQuest) {
			// Parts are found give player a new quest
			GUI.Label(label, questStates [5]);
			Library.habitat.questData.plantComplete = true;
			// Complete quest
			if( GUI.Button (new Rect (Screen.width/2 -90, Screen.height / 1.6f, Screen.width / 4, Screen.height / 7), callButtons[2])) {
				Destroy (_questFinished);
				// reset questdata
				QuestManager.resetQuestData();
				Application.LoadLevel ("Home");
			}
		}
	}

}
