using UnityEngine;
using System.Collections;

public interface IMovementManager
{
	void ApplyCommandOnPlayer(PlayerBody body, MovementCommand command);
}
