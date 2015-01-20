using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	
	public static bool activeLoading = false;
	public Texture backgroundLoading = null;
	public Camera foregroundCam;
	
	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void OnGUI() {
		GUI.depth = -1;
		if(activeLoading) {
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), backgroundLoading);
			if (Event.current.type == EventType.Repaint){
				foregroundCam.enabled = true;
				foregroundCam.Render();
			}
		}
	}
	void OnLevelWasLoaded(int level) {
		foregroundCam.enabled = false;
		activeLoading = false;
	}
}