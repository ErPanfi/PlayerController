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
		m_solidGroundCollider.enabled = false;
	
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
	 	
		if(m_isJumping)
	 	{
	 		float timeElapsed = Time.deltaTime;
	 		this.rigidbody.MovePosition(this.rigidbody.position + new Vector3(0, m_currJumpSpeed*timeElapsed, 0));
	 		m_currJumpSpeed -= timeElapsed*m_gravityOnPlayer;
	 	}
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
	
	[SerializeField]
	private Collider m_solidGroundCollider;
	[SerializeField]
	private float m_initialJumpAcceleration = 3.0f;
	[SerializeField]
	private float m_gravityOnPlayer = 1.0f;
	
	private bool m_isJumping;
	private float m_currJumpSpeed;
	
	public void Jump()
	{
		if(!m_isJumping)
		{
			this.gameObject.rigidbody.useGravity = false;
			m_solidGroundCollider.enabled = true;
			m_currJumpSpeed = m_initialJumpAcceleration;
			m_isJumping = true;
		}
	}
	
	public void OnTriggerEnter(Collider other)
	{
		m_currJumpSpeed = 0;
		m_isJumping = false;
		m_solidGroundCollider.enabled = false;		//disable non-useful checks on player's grounded state
		this.gameObject.rigidbody.useGravity = true;
	}
}
