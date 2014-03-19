using UnityEngine;
using System.Collections;

public class RelativeMovementManager : IMovementManager
{
	public void ApplyCommandOnPlayer (PlayerBody body, MovementCommand command, float deltaTime = -1)
	{
		if(deltaTime < 0)
		{
			deltaTime = Time.deltaTime;
		}
		
		Transform bodyTransform = body.gameObject.transform;
		Rigidbody bodyRigidbody = body.gameObject.rigidbody;
		if(command.LateralAxis != 0)
		{
			float torqueMagnitude = body.RotAcceleration * command.LateralAxis * deltaTime;
			bodyRigidbody.AddTorque(bodyTransform.up * torqueMagnitude);
		}
		
		if(command.FrontAxis != 0)
		{
			float forceMagnitude = body.LinearAcceleration * command.FrontAxis * deltaTime;
			/*
			Vector3 comOffset = (-bodyTransform.up + bodyTransform.forward * Mathf.Sign(command.FrontAxis)) / 2;			
			bodyRigidbody.AddForceAtPosition(bodyTransform.forward * forceMagnitude, bodyRigidbody.centerOfMass + comOffset, ForceMode.Acceleration);
			*/
			bodyRigidbody.AddForce(bodyTransform.forward*forceMagnitude);
		}
		
		if(command.TestButton(MovementCommand.ActionFlags.Jump))
		{
			body.Jump();
		}
	}	
}
