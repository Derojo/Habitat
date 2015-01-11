using UnityEngine;
using System.Collections;

public class EnterWorld : MonoBehaviour
{
	public string StartMap;

	
	IEnumerator Start()
	{
		float fadeTime = GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel(StartMap);
	}
}
