using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ActionFlags = MovementCommand.ActionFlags;

public class PhysicalInputHandler : IInputHandler
{
	public const string LEFT_RIGHT_AXIS_NAME = "Horizontal";
	public const string BACK_FORWARD_AXIS_NAME = "Vertical";
	public const string JUMP_AXIS_NAME = "Jump";
	
	public MovementCommand ParseInputForCommands ()
	{
		MovementCommand movCommand = new MovementCommand ();

		//movement axis
		movCommand.LateralAxis = Input.GetAxis (LEFT_RIGHT_AXIS_NAME);
		movCommand.FrontAxis = Input.GetAxis (BACK_FORWARD_AXIS_NAME);

				
		if(Input.GetAxis(JUMP_AXIS_NAME) > 0)
		{
			movCommand.AddButton(ActionFlags.Jump);
		}
		
		return movCommand;
	}
}
