using UnityEngine;
using System.Collections;

/**
 * Main data class that keeps perstince data and handles save and load data.
 */
[System.Serializable]
public class Library {

	public static Library habitat;

	public QuestData questData;
	
	public Library () {
		questData = new QuestData();
	}

	// Gets Vector3 from string
//	public Vector3 getVector3(string rString){
//		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
//		float x = float.Parse(temp[0]);
//		float y = float.Parse(temp[1]);
//		float z = float.Parse(temp[2]);
//		Vector3 rValue = new Vector3(x,y,z);
//		return rValue;
//	}

}
