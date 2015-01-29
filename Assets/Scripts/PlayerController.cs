using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float rotateSpeed = 15f;

	// Movement en model
	public float movementSpeed = 5f;
	public string playerModel;
	public CNJoystick MovementJoystick;
	private float _acceleration;
	private Vector3 _currentPosition;
	private CharacterController _characterController;
	private Transform _transformCache;
	private Transform _playerTransform;
	private Transform _mainCameraTransform;

	// Attack
	public CNButton AttackButton;
	public float damage = 25f;
	public bool inCombat = false;
	private float cooldownRemaining = 0f;
	private float _attackState = 0f;
	private GameObject target;
	private bool _isAttacking = false;

	// Health
	public float maxHealth = 100;
	public float regenerateSpeed = 5f;
	private float _regenerateCooldown= 0f;

	// Animation
	private Animator _animator;
	public string Idle = "Idle";
	public string AttackIdle = "AttackIdle";
	public string Walk = "Walk";
	public string Run = "Run";
	public string Punch = "Punch";



	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
	void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_transformCache = GetComponent<Transform>();
		_playerTransform = _transformCache.FindChild(playerModel);
		_animator = _playerTransform.GetComponent<Animator> ();
		AttackButton.FingerTouchedEvent += fingerTouched;
		AttackButton.FingerLiftedEvent += fingerLifted;
		target = GameObject.FindGameObjectWithTag("Enemy");
	}
	

	void Update()
	{
		// Movement handle
		Vector3 movement = new Vector3(
			MovementJoystick.GetAxis("Horizontal"),
			0f,
			MovementJoystick.GetAxis("Vertical"));

		CommonMovementMethod(movement);

		// Animation handle
		if (movement != _currentPosition) {
			if(_acceleration >= 1.5) {
				_animator.Play(Run);
			}
			else {
				_animator.Play(Walk);
			}
		}
		else {
			if(!_isAttacking && cooldownRemaining < 0) {
				if(_attackState < 0) {
					_animator.Play(Idle);
				} else {
					_animator.Play (AttackIdle);
				}
			}
			_currentPosition = movement;
		}

		// Attack handle
		cooldownRemaining -= Time.deltaTime;
		_attackState -= Time.deltaTime;

		//Health regen
		_regenerateCooldown -= Time.deltaTime;
		if (!inCombat && Library.habitat.playerData.curHealth < maxHealth) {
			regenerateHealth ();	
		}

	}
	
	/**
	 * Movement area
	 * 
	 ***************************************************************/
	private void CommonMovementMethod(Vector3 movement)
	{
		movement.y = 0f;
		movement = Quaternion.AngleAxis(MovementJoystick.GetCameraFix (GameObject.Find("ARCamera").GetComponent<Camera>()), Vector3.up) * movement;
		movement.Normalize();
		_acceleration = Mathf.Round(MovementJoystick.getFromBasePosition ().sqrMagnitude * 100f) / 100f;

		FaceDirection(movement);

		_characterController.Move(movement * movementSpeed * _acceleration * Time.deltaTime);
	}
	
	public void FaceDirection(Vector3 direction)
	{
		StopCoroutine("RotateCoroutine");
		StartCoroutine("RotateCoroutine", direction);
	}
	
	IEnumerator RotateCoroutine(Vector3 direction)
	{
		if (direction == Vector3.zero) yield break;
		
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		do
		{
			_playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
			yield return null;
		}
		while ((direction - _playerTransform.forward).sqrMagnitude > 0.2f);
	}


	/*
	 * Player Health and Attack area
	 * 
	 * **************************************************************************************************/

	public void ReceiveDamage ( float amt)
	{
		Library.habitat.playerData.curHealth += amt;

		// We are dead, respawn
		if (Library.habitat.playerData.curHealth <= 0)
		{
			// Start at home map
			Application.LoadLevel("Home");
			// Set data back to 100%
			Library.habitat.playerData.curHealth = 100;
		}

		if (Library.habitat.playerData.curHealth > maxHealth)
		{
			Library.habitat.playerData.curHealth = maxHealth;
		}
		
		if (maxHealth < 1) {
			maxHealth = 1;
		}
	}

	private void fingerTouched(CNAbstractController cnAbstractController)
	{
		// Set attack state
		_attackState = 1.5f;
		if (cooldownRemaining < 0) {
			_isAttacking = true;
			playerAttack ();
			cooldownRemaining = 0.4f;
		}
	}

	private void playerAttack()
	{
		_animator.Play(Punch);
		//calculating distance between target en player
		if(target != null) {
			float distance = Vector3.Distance (target.transform.position, transform.position);
			
			Vector3 dir = (target.transform.position - transform.position);
			
			float direction = Vector3.Dot(dir, transform.forward);
			Debug.Log ("Distance to enemy:"+distance);
			
			//getting enemy script
			Enemy enemy = target.GetComponent<Enemy>();
			
			if(enemy)
			{
				Debug.Log ("Enemy distance"+enemy.maxDistance);
				if(distance <= enemy.maxDistance && direction > 0)
				{
					enemy.ReceiveDamage(-damage);
				}
			}
		}
	}

	private void regenerateHealth() {
		if (_regenerateCooldown < 0) {
			Library.habitat.playerData.curHealth+= 10;
			_regenerateCooldown = regenerateSpeed;
		}
	}
	private void fingerLifted(CNAbstractController cnAbstractController)
	{
		_isAttacking = false;
	}
	
}