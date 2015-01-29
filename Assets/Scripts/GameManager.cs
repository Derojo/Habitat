using UnityEngine;
using System.Collections;

// GameManager to update, manage and show gameobjects
public class GameManager : MonoBehaviour {
	private GameObject _mapEnvironment;
	void Update() {
		if (!Library.habitat.questData.completeQuest) {
			QuestManager.checkQuestComplete();	
		}
	}

	void OnLevelWasLoaded() {
		if(Library.habitat.spawnData.spawnMap == "Home") {
			string levelEnvironment= "Level1";
			if (Library.habitat.questData.plantComplete) {
				levelEnvironment = "Level2";
			}
			_mapEnvironment = Instantiate(Resources.Load(levelEnvironment)) as GameObject; 
			_mapEnvironment.transform.parent = GameObject.Find("Environment").transform;
			_mapEnvironment.name = levelEnvironment;
			_mapEnvironment.transform.localPosition = _mapEnvironment.transform.position;
			_mapEnvironment.transform.localScale = _mapEnvironment.transform.lossyScale;
		} else {
			Destroy (_mapEnvironment);
		}
		
		if(Library.habitat.questData.activeQuest) {
			// Spawn quest objects
			QuestManager.spawnQuestObjectsOnLoad ();
			// Show HUD
			QuestManager.activateQuestHUD(true);
		}
	}
}
