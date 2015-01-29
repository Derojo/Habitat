using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public static void spawnQuestObjectsOnLoad() {
		
		foreach(string partName in Library.habitat.questData.questParts)
		{
			// Check if the part is already spawned
			if(GameObject.Find (partName) == null) {
//				Debug.Log ("Part not spawned"+partName);
				//Check whether or not the part is already found
				if(!Library.habitat.questData.questPartsFound.ContainsKey(partName)) {
					// Instantiate from resource folder
					GameObject _spawnPart = Instantiate(Resources.Load(partName)) as GameObject; 
					// Check if we are in the right scene
					if(_spawnPart.GetComponent<QuestItem>().partSpawnMap == Library.habitat.spawnData.spawnMap) {
						// Set the right parent from hierarchy
						_spawnPart.transform.parent = GameObject.Find("Container").transform;
						_spawnPart.name = partName;
						_spawnPart.transform.localPosition = _spawnPart.transform.position;
						_spawnPart.transform.localScale = _spawnPart.transform.lossyScale;
					} else {
						Destroy (_spawnPart);
					}
				}
			}
		}
		
	}

	public static void activateQuestHUD(bool _status = true) {
			foreach (Transform child in GameObject.Find("QuestTrack").transform) {
				child.gameObject.SetActive(_status);
			}
	}
	
	public static void setQuestTrackTitle(string _name) {
		if (_name != null) {
			GameObject.Find ("QuestTitle").GetComponent<GUIText>().text = _name;		
		}
	}

	public static void checkQuestComplete() {
		if (Library.habitat.questData.activeQuest) {
			if (Library.habitat.questData.partsFound == Library.habitat.questData.questParts.Count) {
				Library.habitat.questData.completeQuest = true;
			}
		}
	}

	public static void resetQuestData() {
		// Reset all quest data
		Library.habitat.questData.activeQuest = false;
		Library.habitat.questData.completeQuest = false;
		Library.habitat.questData.partsFound = 0;
		Library.habitat.questData.questParts.Clear ();
		Library.habitat.questData.questPartsFound.Clear ();
		// Deactivate HUD
		activateQuestHUD(false);
	}

}
