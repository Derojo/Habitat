using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public static void spawnQuestObjectsOnLoad() {
		
		foreach(string partName in Library.habitat.questData.questParts)
		{
			//Check if we need to spawn a part
			if(!Library.habitat.questData.questPartsFound.ContainsKey(partName)) {
				// Instantiate from resource folder
				GameObject _spawnPart = Instantiate(Resources.Load(partName)) as GameObject; 
				// Check if we are in the right scene
				if(_spawnPart.GetComponent<QuestItem>().partSpawnMap == Application.loadedLevelName) {
					// Set the right parent from hierarchy
					_spawnPart.transform.parent = GameObject.Find("Container").transform;
					_spawnPart.name = partName;
					_spawnPart.transform.localPosition = _spawnPart.transform.position;
				} else {
					Destroy (_spawnPart);
				}
			}
		}
		
	}

	public static void activateQuestHUD() {
		foreach (Transform child in GameObject.Find("QuestTrack").transform)
		{
			child.gameObject.SetActive(true);
		}
	}

	public static void setQuestTrackTitle(string _name) {
		if (_name != null) {
			GameObject.Find ("QuestTitle").GetComponent<GUIText>().text = _name;		
		}
	}
	
}
