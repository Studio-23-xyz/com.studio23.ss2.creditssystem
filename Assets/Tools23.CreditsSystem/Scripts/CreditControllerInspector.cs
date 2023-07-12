using Tools23.CreditSystem.Core;
using UnityEditor;
using UnityEngine;

namespace Tools23.CreditSystem.EditorCore
{
	//[CustomEditor(typeof(CreditController))]
	public class CreditControllerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField("Custom Editor Here");
			DrawDefaultInspector();
			CreditController controller = (CreditController)target;
			EditorGUILayout.Space(5);
			if (GUILayout.Button("Horizontal Layout"))
			{
				controller.GenerateHorizontalSections();
			}
			EditorGUILayout.Space(5);
			if (GUILayout.Button("Vertical Layout"))
			{
				controller.GenerateVerticalSections();
			}
		}
	}
}