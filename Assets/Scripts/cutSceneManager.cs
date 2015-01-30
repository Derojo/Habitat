using UnityEngine;
using System.Collections;

public class cutSceneManager : MonoBehaviour {
	

	protected IEnumerator highlightItem(GameObject _target, float time, Vector3 _from, float _to, bool _rotate  = false) {
		Vector3 destinationScale = new Vector3(_to, _to, _to);
		
		float currentTime = 0.0f;

		while (currentTime <= time)
		{
			_target.transform.localScale = Vector3.Lerp(_from, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		}
		if(_rotate) {
			while(true){
				transform.Rotate(0, 1f, 0);
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	protected void showHideGameObjects(GameObject target, bool status, bool hideGUI = false) {
		Renderer[] rendererComponents = target.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = target.GetComponentsInChildren<Collider>(true);
		
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = status;
		}
		
		foreach (Collider component in colliderComponents)
		{
			component.enabled = status;
		}
		
		if(hideGUI) {
			GUIText[] HUD = target.GetComponentsInChildren<GUIText> (true);
			foreach (GUIText component in HUD) {
				component.enabled = status;
			}
		}
	}
	
}
