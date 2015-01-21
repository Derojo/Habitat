using UnityEngine;
using System.Collections;

public class SplashMenu : MonoBehaviour {
	Color color = new Color(1,1,1,0);
	public float fadeInTime = 3.3f;
	public float fadeOutTime = 3.3f;
	float fadeInTimer = 0.0f;
	float fadeOutTimer = 0.0f;
	public Texture habitat1 = null;
	public Texture background = null;

	void Update () 
	{
		if (fadeInTimer < fadeInTime)
		{
			fadeInTimer += Time.deltaTime;
			color.a = fadeInTimer / fadeInTime;
		}
		else if (fadeOutTimer < fadeOutTime)
		{
			fadeOutTimer += Time.deltaTime;
			color.a = 1.0f - (fadeOutTimer / fadeOutTime);
		}
		else
		{
			Application.LoadLevel("MainMenu");
		}
	}

	void OnGUI()
	{
		GUI.color = color;
		GUI.DrawTexture(new Rect(0, 0 , Screen.width, Screen.height), background);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), habitat1, ScaleMode.ScaleToFit, true);
	}
}
