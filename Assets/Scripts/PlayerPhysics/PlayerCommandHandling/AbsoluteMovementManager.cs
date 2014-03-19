using UnityEngine;
using System.Collections;

public class AbsoluteMovementManager : IMovementManager
{
	public void ApplyCommandOnPlayer (PlayerBody body, MovementCommand command, float deltaTime = -1)
	{
		if(deltaTime < 0)
		{
			deltaTime = Time.deltaTime;
		}
		
		if(command.HasMovement)
		{
		Debug.Log("Command have ("+command.FrontAxis+","+command.LateralAxis+")");
			Transform cameraReference = body.ActiveCamera.transform;
			Rigidbody rigidbody = body.gameObject.rigidbody;
			Transform playerTransform = body.gameObject.transform;
			//calculate target movement
			Vector3 targetMovement = (command.FrontAxis * cameraReference.forward) + (command.LateralAxis * cameraReference.right);
			targetMovement.y = 0;		//purge vertical component
			targetMovement.Normalize();	//normalize
			
			Vector2 commandVector = new Vector2(command.FrontAxis, command.LateralAxis);
			
			//accelerate toward target movement
			rigidbody.AddForce(targetMovement * commandVector.magnitude * body.LinearAcceleration * deltaTime);
			
			//gradually rotate front toward target
			
			Vector3 currentFront = playerTransform.forward;
			currentFront.y = 0;
			currentFront.Normalize();	//prevent 
			
			float angle = Vector3.Angle(currentFront, targetMovement);
			if(angle != 0)
			{
				//check if remaining delta is smaller than what's percorrble with current angular velocity
				playerTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(playerTransform.forward, targetMovement, body.RotAcceleration * deltaTime, body.LinearAcceleration * deltaTime));
				/*
				float potential = body.RotAcceleration * deltaTime;
				if(angle < potential)
				{
					playerTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(playerTransform.forward, targetMovement, 0, 0));
				}
				else 
				{
					rigidbody.AddTorque(body.transform.up * potential);
				}
				*/
			}
			
			
		}
		
		if(command.TestButton(MovementCommand.ActionFlags.Jump))
		{
			body.Jump();
		}
	}
}
