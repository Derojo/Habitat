using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	void Start() {

	}
}
