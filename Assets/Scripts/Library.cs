using UnityEngine;
using System.Collections;

/**
 * Main data class that keeps perstince data and handles save and load data.
 */
[System.Serializable]
public class Library {

	public static Library habitat;

	public QuestData questData;
	public SpawnData spawnData;
	
	public Library () {
		questData = new QuestData();
		spawnData = new SpawnData();
	}

	// Gets Vector3 from string
	public Vector3 getVector3(string rString){
		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		Vector3 rValue = new Vector3(x,y,z);
		return rValue;
	}

	// Gets Quatorneion from string
	public Quaternion getQuaternion(string rString){
		string[] temp = rString.Substring(1,rString.Length-3).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		float w = float.Parse(temp[3]);
		Quaternion rValue = new Quaternion(x,y,z,w);
		return rValue;
	}

}
