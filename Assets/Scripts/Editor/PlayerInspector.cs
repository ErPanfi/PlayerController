using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;

[CustomEditor(typeof(PlayerBody))]
public class PlayerInspector : Editor 
{
	private PlayerBody m_body;
	
	public void OnEnable()
	{
		m_body = (PlayerBody)target;
	} 

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();
		
		m_body.LinearAcceleration = EditorGUILayout.FloatField("Accelerazione Frontale", m_body.LinearAcceleration);
		m_body.RotAcceleration = EditorGUILayout.FloatField( "Accelerazione angolare", m_body.RotAcceleration);
		m_body.InitialJumpAcceleration = EditorGUILayout.FloatField(  "Accelerazione in salto", m_body.InitialJumpAcceleration);
		m_body.GravityOnPlayer = EditorGUILayout.FloatField( "Gravità soggettiva", m_body.GravityOnPlayer);
	}

}
