using UnityEngine;
using System.Collections;

public class QuestItem : MonoBehaviour {

	public string partSpawnMap;
	public Vector3 hightlightPosition;
	public Vector3 highlightRotation;
	public float highlightScale = 90f;
	public GameObject lifeStreamEffect;
	public GUISkin skin = null;
	public static bool sceneCutPlaying = false;

	private bool onTrigger = false;
	private GameObject _lifeStreamEffect;
	// Use this for initialization

	IEnumerator OnTriggerEnter() {
		// Fade in and out
		QuestItem.sceneCutPlaying = true;
		GameObject.Find ("Fader").GetComponent<Fader> ().fadeSpeed = 0.8f;
		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
		yield return new WaitForSeconds(0.8f);
		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(-1);
		// Hide Player
		showHideGameObjects(GameObject.Find ("PlayerSetup"), false);
		// Hide Controls
		showHideGameObjects(GameObject.Find ("HUD"), false, true);
		// Update parts
		Library.habitat.questData.partsFound++;
		Library.habitat.questData.questPartsFound.Add(name, true);
		// Highlight part
		transform.localPosition = hightlightPosition;
		transform.eulerAngles =  new Vector3(highlightRotation.x, highlightRotation.y, highlightRotation.z);
		// Add lifestream effect
		_lifeStreamEffect = Instantiate (lifeStreamEffect) as GameObject;
		_lifeStreamEffect.transform.parent = GameObject.Find("Container").transform;
		_lifeStreamEffect.transform.localPosition = _lifeStreamEffect.transform.position;
		_lifeStreamEffect.transform.localScale = _lifeStreamEffect.transform.lossyScale;
		StartCoroutine(highlightItem(1));


	}

	void OnGUI() {

		GUI.skin = skin;
		// Fix font size for every resolution
		GUI.skin.GetStyle("questPartTitle").fontSize = Screen.width / 20;
		GUI.skin.button.fontSize = Screen.width / 30;
		if (onTrigger) {
			// Create a continue button
			GUI.Label(new Rect (Screen.width / 2, Screen.height / 10, 0, 0), gameObject.name+" Gevonden!", GUI.skin.GetStyle("questPartTitle"));
			if (GUI.Button (new Rect (Screen.width/2 -90, Screen.height/2 +50, Screen.width / 4, Screen.height / 7), "Doorgaan")) {
				StartCoroutine(continueQuest());
				QuestItem.sceneCutPlaying = false;
			}
		}
	}
	IEnumerator highlightItem(float time) {
		Vector3 originalScale = transform.localScale;
		Vector3 destinationScale = new Vector3(highlightScale, highlightScale, highlightScale);
		
		float currentTime = 0.0f;

		while (currentTime <= time)
		{
			transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		}
		onTrigger = true;
	}

	private void showHideGameObjects(GameObject target, bool status, bool hideGUI = false) {
		Renderer[] rendererComponents = target.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = target.GetComponentsInChildren<Collider>(true);

		foreach (Renderer component in rendererComponents)
		{
			component.enabled = status;
		}
	
		foreach (Collider component in colliderComponents)
		{
			component.enabled = status;
		}

		if(hideGUI) {
			GUIText[] HUD = target.GetComponentsInChildren<GUIText> (true);
			foreach (GUIText component in HUD) {
				component.enabled = status;
			}
		}
	}

	IEnumerator continueQuest() {
		// Fade in and out
		GameObject.Find ("Fader").GetComponent<Fader> ().fadeSpeed = 0.8f;
		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
		yield return new WaitForSeconds(0.8f);
		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(-1);
		// Show Player
		showHideGameObjects(GameObject.Find ("PlayerSetup"), true);
		// Show Controls
		showHideGameObjects(GameObject.Find ("HUD"), true, true);
		// Kill the part
		Destroy (gameObject);
		Destroy (_lifeStreamEffect);
		
	}
	
}