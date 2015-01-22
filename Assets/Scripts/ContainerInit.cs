using UnityEngine;
using System.Collections;

// Load saved items such as questparts
public class ContainerInit : MonoBehaviour {

	void Update() {
		GameObject.Find ("QuestPartsFound").GetComponent<GUIText>().text = Library.habitat.questData.partsFound + " / " + Library.habitat.questData.questParts.Count;
	}
	void OnLevelWasLoaded() {
		if (Library.habitat.questData.activeQuest) {
			// Spawn quest objects
			QuestManager.spawnQuestObjectsOnLoad ();
			// Show HUD
			QuestManager.activateQuestHUD();
		}
	}
}
