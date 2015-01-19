using UnityEngine;
using System.Collections;

	public class MainMenu : MonoBehaviour {
	public GUISkin skin = null;
	public Texture habitat1 = null;
	public Texture backGround;

	//var standardResolutionX:float= 1024; var standardResolutionY:float= 768;
	
	//function OnGUI () { GUI.matrix = Matrix4x4.TRS (Vector3(0, 0, 0), Quaternion.identity, Vector3(Screen.width / standardResolutionX, Screen.height /standardResolutionY, 1)); 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//scherm meescalen
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (Screen.width / 1024.0f, Screen.height / 768.0f, 1.0f));

		if (skin != null)
		GUI.skin = skin;
		//menu naam oproepen

		//GUI.DrawTexture( new Rect(Screen.width/2, -100, Screen.width * 0.8f, Screen.height * 0.8f), habitat1);
		//GUI.DrawTexture( new Rect(Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 1.0f, Screen.height * 1.0f), habitat1);

		//GUI.DrawTexture( new Rect(370, 10, 50, 50), habitat1);
		//GUI.Label (new Rect (370, 10, 300, 50), "Habitat");
		//buttons oproepen

		GUI.DrawTexture(new Rect (0, 0, 1024, 768), backGround);
		GUI.DrawTexture(new Rect (275, -50, 500, 500), habitat1);
		if (GUI.Button (new Rect (370, 300, 300, 100), "NIEUW SPEL")) 
		{
			Application.LoadLevel ("Initialize");
		}

		GUI.Button (new Rect (370, 450, 300, 100), "OPTIES");

		if (GUI.Button (new Rect (370, 600, 300, 100), "AFSLUITEN"))
		{
			Application.Quit();
		}
	}
}

