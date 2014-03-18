using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour
{
	public enum CameraType
	{
		Relative = 0,
		Absolute
	}
	public CameraType m_cameraMode;
	private IMovementManager m_movementManager;
	
	public enum InputType
	{
		Keyboard = 0,
		Touchscreen,
	} 
	public InputType m_inputMode;
	private IInputHandler m_inputHandler;
	
	public void Start()
	{
		switch (m_cameraMode) 
		{
		case CameraType.Relative :
				m_movementManager = new RelativeMovementManager();
				break;
		case CameraType.Absolute :
				//TODO absolute movement manager
				break;
			default:
				throw new MissingComponentException("Can't decode camera mode : " + m_cameraMode);
				break;
		}
		
		switch(m_inputMode)
		{
		case InputType.Keyboard:
				m_inputHandler = new PhysicalInputHandler();
				break;
		case InputType.Touchscreen:
				//TODO touchscreen input
				break;
			default:
				throw new MissingComponentException("Can't decode camera mode : " + m_cameraMode);
				break;
		}
	}
	
	public void Update()
	{
		//parse input and apply command on player
	 	m_movementManager.ApplyCommandOnPlayer(this, m_inputHandler.ParseInputForCommands());
	}

	[SerializeField]
	private float m_rotAcceleration;
	public float RotAcceleration
	{
		get
		{
			return m_rotAcceleration;
		}
	}
	
	[SerializeField]
	private float m_linearAcceleration;
	public float LinearAcceleration
	{
		get
		{
			return m_linearAcceleration;
		}
	}

	private bool m_isJumping;
	
	public void Jump()
	{
		if(!m_isJumping)
		{
			//TODO implement
		}
	}
}
