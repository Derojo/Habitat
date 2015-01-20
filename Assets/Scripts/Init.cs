using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	public GameObject PlayerSetup;

	void Start () 
	{
		if (!SpawnData.control.isSpawned) {
			GameObject.Find ("Fader").GetComponent<Fader> ().BeginFade (-1);
			PlayerSetup.SetActive (true);
		}
	}


}
