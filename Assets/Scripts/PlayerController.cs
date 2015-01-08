using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float rotateSpeed = 15f;
	
	public float movementSpeed = 5f;
	public string playerModel;
	public CNAbstractController MovementJoystick;

	private CharacterController _characterController;
	private Transform _transformCache;
	private Transform _playerTransform;
	private Transform _mainCameraTransform;
	
	void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_mainCameraTransform = Camera.main.GetComponent<Transform>();
		_transformCache = GetComponent<Transform>();
		_playerTransform = _transformCache.FindChild(playerModel);
	}
	
	
	// Update is called once per frame
	void Update()
	{
		var movement = new Vector3(
			MovementJoystick.GetAxis("Horizontal"),
			0f,
			MovementJoystick.GetAxis("Vertical"));
		
		CommonMovementMethod(movement);
	}
	
	private void CommonMovementMethod(Vector3 movement)
	{
		movement = _mainCameraTransform.TransformDirection(movement);
		movement.y = 0f;
		movement.Normalize();
		
		FaceDirection(movement);
		_characterController.Move(movement * movementSpeed * Time.deltaTime);
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