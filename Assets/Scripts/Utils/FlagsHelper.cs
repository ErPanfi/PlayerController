using UnityEngine;
using System.Collections;

public class FlagsHelper
{
	public static void SetFlag<T>(ref T flagSet, T value)
	{
		flagSet = (T)(object)((int)(object)flagSet | (int)(object)value);
	}
	
	public static void UnsetFlag<T>(ref T flagSet, T value)
	{
		flagSet = (T)(object)((int)(object)flagSet & ~(int)(object)value);
	}
	
	public static bool TestFlag<T>(T flagSet, T value)
	{
		return ((int)(object)flagSet & (int)(object)value) != 0;
	}
	
}
