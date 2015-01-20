using UnityEngine;
using System.Collections;

public class SplashMenu : MonoBehaviour {
	Color color = new Color(1,1,1,0);
	public float fadeInTime = 3.3f;
	public float fadeOutTime = 3.3f;
	float fadeInTimer = 0.0f;
	float fadeOutTimer = 0.0f;
	public Texture habitat1 = null;



	// Update is called once per frame
	void Update () 
	{
		Debug.Log (fadeOutTimer);


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
		GUI.DrawTexture( new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.8f), habitat1);
	}
}
