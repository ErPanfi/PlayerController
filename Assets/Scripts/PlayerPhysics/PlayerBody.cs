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
	
	[SerializeField]
	private Camera m_innerCamera;
	[SerializeField]
	private Camera m_externalCamera;	
	private Vector3 m_externalCameraOffset;
	
	public Camera ActiveCamera
	{
		get
		{
			switch(m_cameraMode)
			{
				case CameraType.Absolute:
					return m_externalCamera;
				case CameraType.Relative:
					return m_innerCamera;
				default:
					throw new MissingComponentException("Can't decode camera mode : " + m_cameraMode);
			}
		}
	}
	
	
	public enum InputType
	{
		Keyboard = 0,
		Touchscreen
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
				m_externalCamera.gameObject.SetActive(false);
				m_innerCamera.gameObject.SetActive(true);
				break;
		case CameraType.Absolute :
				m_movementManager = new AbsoluteMovementManager();
				m_externalCamera.gameObject.SetActive(true);
				m_innerCamera.gameObject.SetActive(false);				
			break;
			default:
				throw new MissingComponentException("Can't decode camera mode : " + m_cameraMode);
		}
		
		Debug.Log("Now inner camera is ("+m_innerCamera.gameObject.activeSelf+") and outer camera is ("+m_externalCamera.gameObject.activeSelf+")");
		
		m_externalCameraOffset = m_externalCamera.gameObject.transform.position - this.gameObject.transform.position;
		
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
		}
	}
	
	public void Update()
	{
		float timeElapsed = Time.deltaTime;
		//parse input and apply command on player
	 	m_movementManager.ApplyCommandOnPlayer(this, m_inputHandler.ParseInputForCommands(), timeElapsed);
	 	
		if(m_isJumping)
	 	{
	 		this.rigidbody.MovePosition(this.rigidbody.position + new Vector3(0, m_currJumpSpeed*timeElapsed, 0));
	 		m_currJumpSpeed -= timeElapsed*m_gravityOnPlayer;
	 	}
	 	
	 	if(m_externalCamera.gameObject.activeSelf)
	 	{
	 		m_externalCamera.transform.position = this.gameObject.transform.position + m_externalCameraOffset;
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
		
		 set
		{
			m_rotAcceleration = value;
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
		
		 set
		{
			m_linearAcceleration = value;
		}
	}
	
	[SerializeField]
	private Collider m_solidGroundCollider;
	[SerializeField]
	private float m_initialJumpAcceleration = 3.0f;

	public float InitialJumpAcceleration 
	{
		get 
		{
			return m_initialJumpAcceleration;
		}
		
		 set 
		{
			m_initialJumpAcceleration = value;
		}
	}

	[SerializeField]
	private float m_gravityOnPlayer = 1.0f;

	public float GravityOnPlayer 
	{
		get 
		{
			return m_gravityOnPlayer;
		}
		
		 set 
		{
			m_gravityOnPlayer = value;
		}
	}
	
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
