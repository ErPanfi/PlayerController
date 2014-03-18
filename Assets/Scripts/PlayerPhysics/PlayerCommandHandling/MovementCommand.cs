using UnityEngine;

public class MovementCommand
{	
	//Movement directions
	private float m_frontAxis;
	public float FrontAxis
	{
		get
		{
			return m_frontAxis;
		}
		
		set
		{
			m_frontAxis = value;
		}
	}
	
	private float m_lateralAxis;
	public float LateralAxis
	{
		get
		{
			return m_lateralAxis;
		}
		
		set
		{
			m_lateralAxis = value;
		}
	}

	//ACTIONS BITMASK
	public enum ActionFlags
	{
		None = 0,
		Jump = 1,
	}

	//action flag management
	private ActionFlags m_buttonFlags;

	public MovementCommand()
	{
		m_buttonFlags = ActionFlags.None;
		m_frontAxis = m_lateralAxis = 0;
	}

	public void AddButton(ActionFlags newButton)
	{
		FlagsHelper.SetFlag(ref m_buttonFlags, newButton);
	}

	public void RemoveButton(ActionFlags remButton)
	{
		FlagsHelper.UnsetFlag(ref m_buttonFlags, remButton);
	}

	public bool TestButton(ActionFlags button)
	{
		return FlagsHelper.TestFlag(m_buttonFlags, button);
	}
}