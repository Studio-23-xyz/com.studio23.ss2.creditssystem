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

            GUILayout.BeginHorizontal(); // Begin horizontal layout

            if (GUILayout.Button("Horizontal Layout"))
            {
                controller.GenerateHorizontalSections();
            }

            if (GUILayout.Button("Vertical Layout"))
            {
                controller.GenerateVerticalSections();
            }

            GUILayout.EndHorizontal(); // End horizontal layout

            EditorGUILayout.Space(5);

            // New button for generating credits data
            if (GUILayout.Button("Generate Credits Data"))
            {
                controller.GenerateSections();
            }

            EditorGUILayout.Space(5);
        }
	}
}