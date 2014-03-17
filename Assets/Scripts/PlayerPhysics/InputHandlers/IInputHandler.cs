using System.Collections;
using System.Collections.Generic;

public interface IInputHandler
{
	MovementCommand ParseInputForCommands();
}