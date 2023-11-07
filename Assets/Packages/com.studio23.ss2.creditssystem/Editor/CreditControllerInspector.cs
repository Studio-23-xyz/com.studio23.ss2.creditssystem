using Studio23.SS2.CreditsSystem.Core;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.CreditsSystem.Editor
{
	[CustomEditor(typeof(CreditController))]
	public class CreditControllerInspector : UnityEditor.Editor
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