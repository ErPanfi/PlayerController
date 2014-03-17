using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ActionFlags = MovementCommand.ActionFlags;

public class PhysicalInputHandler : IInputHandler
{
	public const string LEFT_RIGHT_AXIS_NAME = "Horizontal";
	public const string BACK_FORWARD_AXIS_NAME = "Vertical";
	public const string JUMP_AXIS_NAME = "Jump";
	
	ActionFlags m_resetButtonsMask;
	
	public MovementCommand ParseInputForCommands ()
	{
		MovementCommand movCommand = new MovementCommand ();

		//movement axis
		movCommand.LateralAxis = Input.GetAxis (LEFT_RIGHT_AXIS_NAME);
		movCommand.FrontAxis = Input.GetAxis (BACK_FORWARD_AXIS_NAME);

				
		if(Input.GetAxis(JUMP_AXIS_NAME) > 0 && !FlagsHelper.TestFlag(m_resetButtonsMask, ActionFlags.Jump))
		{
			movCommand.AddButton(ActionFlags.Jump);
			FlagsHelper.SetFlag(ref m_resetButtonsMask, ActionFlags.Jump);
		}
		else
		{
			FlagsHelper.UnsetFlag(ref m_resetButtonsMask, ActionFlags.Jump);
		}
		
		return movCommand;
	}
}
