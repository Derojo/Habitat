using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// Movement
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	// Health
	public float maxHealth = 100;
	public float curHealth = 100;
	private GameObject _enemyHealthHUD = null;
	// Attack
	public float attackTimer = 0.0f;
	public float coolDown = 1.0f;
	public float damage = 5f;
	//sounds
	public AudioClip getHit;
	public AudioClip buzz;
	private Transform myTransform;
	public bool follow;
	private bool _isPLaying = false;
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
		_enemyHealthHUD = Instantiate(Resources.Load("EnemyHealth")) as GameObject;
		_enemyHealthHUD.GetComponent<HoverFollow> ().target = this.transform;
		_enemyHealthHUD.transform.parent = GameObject.Find("HUD").transform;

	}

	void Update() {
		if (follow) 
		{
		
			
			Vector3 lookPos = target.position - myTransform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance) 
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;		
		}

		if (attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
		}
		
		if (attackTimer < 0) 
		{
			attackTimer = 0;
		}
		
		if(attackTimer == 0)
		{
			Attack();
			attackTimer = coolDown;
		}

		// Health overlay
		_enemyHealthHUD.GetComponent<GUIText>().text = ""+curHealth;
		
		
	}
	
	void Attack()
	{

		//calculating distance between target en player
		float distance = Vector3.Distance (target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		Debug.Log (direction);
		
		//getting player health
		PlayerController player = target.GetComponent<PlayerController>();
		if(player)
		{
			if(distance <= maxDistance && direction > 0)
			{
				audio.PlayOneShot (getHit);
				player.ReceiveDamage(-damage);
			}
		}
		
	}

	void OnTriggerEnter (Collider other) 
	{
		if(!_isPLaying) 
		{
			audio.PlayOneShot(buzz);
			_isPLaying = true;
		}

		target.GetComponent<PlayerController>().inCombat = true;
		follow = true;
	}

	void OnTriggerExit (Collider other)
	{
		audio.Stop();
		target.GetComponent<PlayerController>().inCombat = false;
		 follow = false;
		_isPLaying = false;
	}


	public void ReceiveDamage ( float amt)
	{
		curHealth += amt;
		
		if (curHealth < 0)
		{
			target.GetComponent<PlayerController>().inCombat = false;
			// enemy is dead, let's remove it
			Destroy (gameObject);
			// Destroy enemy health hud
			Destroy(_enemyHealthHUD);
		}
		
		
		if (curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
		
		if (maxHealth < 1) {
			maxHealth = 1;
		}
		
		
	}

}
