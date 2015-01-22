using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	private Transform myTransform;
	public bool follow;
	
	void Awake () 
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		if (go != null) 
		{
			target = go.transform;
		}
		maxDistance = 2;
	}

	void Update() {
		if (follow) 
		{
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance) 
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		follow = true;

	}

	void OnTriggerExit (Collider other)
	{
		 follow = false;
	}

}
