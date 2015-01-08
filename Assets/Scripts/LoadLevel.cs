using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {


	public string sceneName;
	public Transform spawnDirection;

	private bool _collided;
	private Transform _playerTransform;


	IEnumerator OnTriggerEnter(Collider collider) {
		_collided = true;

		if (_collided) {
			// Store spawnDirection position in spawnPosition
			SpawnData.control.spawnPosition = spawnDirection.position;
			// Store spawnDirection facing in spawnFacing
			SpawnData.control.spawnFacing = spawnDirection.rotation;
			float fadeTime = GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
			yield return new WaitForSeconds (fadeTime);
			Application.LoadLevel(sceneName);
		}
	}

	void OnCollisionExit () {
		_collided = false;
	}

	void OnLevelWasLoaded () {
		// Place player on the spawnlocation with the right facing
		_playerTransform = GameObject.Find("PlayerSetup").GetComponent<Transform>();
		_playerTransform.position = SpawnData.control.spawnPosition;
		_playerTransform.rotation = SpawnData.control.spawnFacing;
	}
}
