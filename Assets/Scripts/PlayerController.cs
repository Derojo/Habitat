using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float rotateSpeed = 15f;
	
	public float movementSpeed = 5f;
	public string playerModel;
	public string Idle = "Idle";
	public string Walk = "Walk";
	public string Run = "Run";
	public CNJoystick MovementJoystick;

	private float _acceleration;
	private Vector3 _currentPosition;
	private CharacterController _characterController;
	private Transform _transformCache;
	private Transform _playerTransform;
	private Transform _mainCameraTransform;
	private Animator _animator;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
	void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_transformCache = GetComponent<Transform>();
		_playerTransform = _transformCache.FindChild(playerModel);
		_animator = _playerTransform.GetComponent<Animator> ();
	}
	
	
	// Update is called once per frame
	void Update()
	{
		Debug.Log (MovementJoystick.GetCameraFix (GameObject.Find("ARCamera").GetComponent<Camera>()));
		Vector3 movement = new Vector3(
			MovementJoystick.GetAxis("Horizontal"),
			0f,
			MovementJoystick.GetAxis("Vertical"));

		CommonMovementMethod(movement);

		// Animation handle
		if (movement != _currentPosition) {
			Debug.Log (_acceleration);
			if(_acceleration >= 1.5) {
				_animator.Play(Run);
			}
			else {
				_animator.Play(Walk);
			}
		}
		else {
			_animator.Play(Idle);
			_currentPosition = movement;
		}

	}
	
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


}