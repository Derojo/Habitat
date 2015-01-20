using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	// file reference for testing: C:/Users/Rowin/AppData/LocalLow/Group1/Habitat/habitatData.gd
			
	public static void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/habitatData.gd"); //you can call it anything you want
		bf.Serialize(file, Library.habitat);
		file.Close();
	}	
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/habitatData.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/habitatData.gd", FileMode.Open);
			Library.habitat = (Library)bf.Deserialize(file);
			file.Close();
		}
	}

	public static bool dataFileExist() {
		if (File.Exists(Application.persistentDataPath + "/habitatData.gd")) {
			return true;		
		}
		return false;
	}
}
