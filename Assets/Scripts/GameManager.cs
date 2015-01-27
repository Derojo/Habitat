using UnityEngine;
using System.Collections;

// GameManager to update, manage and show gameobjects
public class GameManager : MonoBehaviour {

	void Update() {
		if (!Library.habitat.questData.completeQuest) {
			QuestManager.checkQuestComplete();	
		}
	}

	void OnLevelWasLoaded() {
			if (Library.habitat.questData.activeQuest) {
				// Spawn quest objects
				QuestManager.spawnQuestObjectsOnLoad ();
				// Show HUD
				QuestManager.activateQuestHUD ();
			}
	}
}
