using UnityEngine;
using System.Collections;

public class QuestItem : MonoBehaviour {

	public string partSpawnMap;
	public Vector3 hightlightPosition;
	// Use this for initialization

	void OnTriggerEnter() {
		Library.habitat.questData.partsFound++;
		Library.habitat.questData.questPartsFound.Add(name, true);
		transform.localPosition = hightlightPosition;
		StartCoroutine(highlightItem(1));

	}
	void Awake () {
//		DontDestroyOnLoad(gameObject);
//		activateItem ();
	}

	IEnumerator highlightItem(float time) {
		Vector3 originalScale = transform.localScale;
		Vector3 destinationScale = new Vector3(70f, 70f, 70f);
		
		float currentTime = 0.0f;

		while (currentTime <= time)
		{
			transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		}
	}
	
}