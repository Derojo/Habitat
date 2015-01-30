using UnityEngine;
using System.Collections;

public class cutScene : cutSceneManager {

	public Vector3 hightlightPosition;
	public Vector3 highlightRotation;
	public float highlightScale = 90f;
	public bool rotateEffect = false;
	private GameObject _lifeStreamEffect;
	private float _delay = 4f;

	// Use this for initialization
	void Start () {
		// need to be changed later, fast fix
		QuestItem.sceneCutPlaying = true;
		// Hide Player
		showHideGameObjects(GameObject.Find ("PlayerSetup"), false);
		// Hide Controls
		showHideGameObjects(GameObject.Find ("HUD"), false, true);
		transform.localPosition = hightlightPosition;
		StartCoroutine(growAnimation ());
	}

	IEnumerator growAnimation() {
		// FadeInOut
		GameObject.Find ("Fader").GetComponent<Fader> ().fadeSpeed = 0.8f;
		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
		yield return new WaitForSeconds(0.8f);

		GameObject.Find("Fader").GetComponent<Fader>().BeginFade(-1);
		yield return new WaitForSeconds(_delay);
		StartCoroutine (highlightItem(gameObject, 2f, transform.localScale, highlightScale, true));
		Library.habitat.questData.plantComplete = true;
		yield return new WaitForSeconds(5f);
		Destroy (GameObject.Find ("Bij"));
	}
	
}
