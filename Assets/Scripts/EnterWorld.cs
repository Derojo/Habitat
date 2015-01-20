using UnityEngine;
using System.Collections;

public class EnterWorld : MonoBehaviour
{
	public string StartMap;

	public void Awake() {
		if(SaveLoad.dataFileExist())
	    {
			// Load saved habitat data from file
			SaveLoad.Load();
		}
		else {
			// First initalization of the game, create a data file first
			Library.habitat = new Library();
			SaveLoad.Save();
		}
	}
	
	IEnumerator Start()
	{
		float fadeTime = GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel(StartMap);
	}
}
