using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	bool collided;
	public string sceneName;
	public Transform spawnDirection;

	private Transform _playerTransform;


	IEnumerator OnTriggerEnter(Collider collider) {
		collided = true;

		if (collided) {
			// Store spawnlocation in SpawnData
			SpawnData.control.spawnPosition = new Vector3(spawnDirection.position.x, 0 ,spawnDirection.position.z);
			SpawnData.control.spawnFacing = spawnDirection.rotation;
			float fadeTime = GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
			yield return new WaitForSeconds (fadeTime);
			Application.LoadLevel(sceneName);
		}
	}

	void OnCollisionExit () {
		collided = false;
	}

	void OnLevelWasLoaded () {
		// Place player on the spawnlocation
		_playerTransform = GameObject.Find("PlayerSetup").GetComponent<Transform>();
		_playerTransform.position = SpawnData.control.spawnPosition;
		_playerTransform.rotation = SpawnData.control.spawnFacing;
	}
}
