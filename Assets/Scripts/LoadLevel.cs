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
			// First contact with portal
			if(!Library.habitat.spawnData.isSpawned) {
				Library.habitat.spawnData.isSpawned = true;
			}
			// Store spawnDirection position in spawnPosition
			Library.habitat.spawnData.spawnPosition = (string)spawnDirection.position.ToString();
			// Store spawnDirection facing in spawnFacing
			Library.habitat.spawnData.spawnFacing = (string)spawnDirection.rotation.ToString ();

			float fadeTime = GameObject.Find("Fader").GetComponent<Fader>().BeginFade(1);
			Debug.Log(fadeTime);
			yield return new WaitForSeconds(fadeTime);
			Loader.activeLoading = true;
			Application.LoadLevel(sceneName);
		}
	}

	void OnCollisionExit () {
		_collided = false;
	}

	void OnLevelWasLoaded () {
		// Place player on the spawnlocation with the right facing
		if (Library.habitat.spawnData.isSpawned) {
			_playerTransform = GameObject.Find ("PlayerSetup").GetComponent<Transform> ();
			_playerTransform.position = Library.habitat.getVector3(Library.habitat.spawnData.spawnPosition);
			_playerTransform.rotation = Library.habitat.getQuaternion(Library.habitat.spawnData.spawnFacing);
		}
		// Load questItems if needed
		// Store current spawn map
		Library.habitat.spawnData.spawnMap = Application.loadedLevelName;
	}

}
