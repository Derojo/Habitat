using UnityEngine;
using System.Collections;

// Load saved items such as questparts
public class ContainerInit : MonoBehaviour {

	void OnLevelWasLoaded() {
		if (Library.habitat.questData.activeQuest) {
			QuestManager.spawnQuestObjectsOnLoad ();
		}
	}
}
