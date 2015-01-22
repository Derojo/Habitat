using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public static void spawnQuestObjectsOnLoad() {
		
		foreach(string partName in Library.habitat.questData.questParts)
		{
			GameObject _spawnPart = Instantiate(Resources.Load(partName)) as GameObject; 
			if(_spawnPart.GetComponent<QuestItem>().partSpawnMap == Application.loadedLevelName) {
				_spawnPart.transform.parent = GameObject.Find("Container").transform;
				_spawnPart.name = partName;
				_spawnPart.transform.localPosition = _spawnPart.transform.position;
			} else {
				Destroy (_spawnPart);
			}
		}
		
	}
}
