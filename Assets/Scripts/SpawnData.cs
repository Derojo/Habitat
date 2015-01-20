using UnityEngine;
using System.Collections;

// Persistent spawn data between scenes
[System.Serializable]
public class SpawnData {

	public string spawnPosition;
	public string spawnFacing;
	public string spawnMap;
	public bool isSpawned;

	public SpawnData() {
		this.spawnPosition = "";
		this.spawnFacing = "";
		this.spawnMap = "Home";
		this.isSpawned = false; 
	}
}
