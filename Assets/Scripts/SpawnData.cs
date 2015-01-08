using UnityEngine;
using System.Collections;

// Persistent spawn data between scenes
public class SpawnData : MonoBehaviour {

	public static SpawnData control;
	public Vector3 spawnPosition;
	public Quaternion spawnFacing;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;		
		} 
		else if (control != this) {
			Destroy (gameObject);		
		}
	}
}
