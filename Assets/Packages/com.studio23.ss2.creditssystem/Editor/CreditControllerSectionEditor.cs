using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.CreditsSystem.Data;
using UnityEditor;
using UnityEngine;


namespace Studio23.SS2.CreditsSystem.Editor
{
    public class CreditControllerSectionEditor : EditorWindow
    {
        private CreditSectionContentScriptableObject sectionContent;
        private HeaderData headerData;
        private List<SectionData> sectionDataList = new List<SectionData>();

        private SerializedObject serializedObject;
        SerializedProperty sectionNameProperty;
        SerializedProperty sectionsProperty;

        void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            sectionNameProperty = serializedObject.FindProperty("SectionName");
            sectionsProperty = serializedObject.FindProperty("Sections");
        }



        [MenuItem("Studio-23/Credit System/Create Credit Section")]
        public static void ShowWindow()
        {
            GetWindow<CreditControllerSectionEditor>("Create Credit Section");
        }

        private void OnGUI()
        {
            GUILayout.Label("Credit Section Creation", EditorStyles.boldLabel);
            HeaderData headerData = new HeaderData();
            SectionData sectionData = new SectionData();

            GUILayout.Label("Header Data", EditorStyles.boldLabel);
            headerData.HeaderImage = (Sprite)EditorGUILayout.ObjectField("Header Image", headerData.HeaderImage, typeof(Sprite), false);
            headerData.MainHeaderText = EditorGUILayout.TextField("Main Header Text", headerData.MainHeaderText);
            headerData.SubHeaderText = EditorGUILayout.TextField("Sub Header Text", headerData.SubHeaderText);
            headerData.OptionalHeaderText = EditorGUILayout.TextField("Optional Header Text", headerData.OptionalHeaderText);

            GUILayout.Label("Section Data", EditorStyles.boldLabel);
            sectionData.SectionName = EditorGUILayout.TextField("Section Name", headerData.MainHeaderText);

            GUILayout.Label("Sections", EditorStyles.boldLabel);

            if (sectionData.Sections == null)
            {
                sectionData.Sections = new List<string>();
            }

            SerializedProperty sectionsProperty = serializedObject.FindProperty("Sections");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sectionsProperty, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

        }

        private CreditSectionContentScriptableObject CreateCreditSectionContent()
        {
            var newSection = CreateInstance<CreditSectionContentScriptableObject>();
            var path = AssetDatabase.GenerateUniqueAssetPath(
                "Assets/YourFolderPath/YourSectionAssetName.asset"); // Set the desired folder and asset name
            AssetDatabase.CreateAsset(newSection, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newSection;

            return newSection;
        }

        private void SetHeaderData()
        {
            if (headerData != null)
            {
                sectionContent.HeaderInformation = headerData;
                EditorUtility.SetDirty(sectionContent);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void AddSectionData()
        {
            var newSectionData = new SectionData();
            sectionDataList.Add(newSectionData);
        }
    }
}
