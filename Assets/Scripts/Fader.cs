using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{
	public Texture2D fadeOutTexture;	// overlay texture
	public float fadeSpeed = 0.8f; 		// fading speed

	private int drawDepth = -1000;		// make sure it renders on top of everything
	private float alpha = 1.0f; 		// manage alpha between 0 and 1
	private int fadeDir = -1;			// direction to fade: in = -1 out - 1

	void OnGUI () {
		// fade out/in
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// force the alpha number between 0 and 1
		alpha = Mathf.Clamp01 (alpha);
		// Set the GUI fading color
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth; // make the texture render on top
		GUI.DrawTexture ( new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture ); //draw the texture on the whole screen
	}

	// set fadeDir to -1 (fade in) or 1 (fade out)
	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed); // return fadeSpeed so we can time other functions
	}

	// Called when new scene is loaded
	void OnLevelWasLoaded () {
		BeginFade (-1); // call the fade in function
	}
}