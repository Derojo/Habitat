using UnityEngine;
using System.Collections;

public class QuestItem : MonoBehaviour {

	public string partSpawnMap;
	// Use this for initialization

	void OnTriggerEnter() {
		Destroy (gameObject);
		Library.habitat.questData.partsFound++;
		Library.habitat.questData.questPartsFound.Add(name, true);
	}
	void Awake () {
//		DontDestroyOnLoad(gameObject);
//		activateItem ();
	}

	// Update is called once per frame
	void Update () {
	
	}

}