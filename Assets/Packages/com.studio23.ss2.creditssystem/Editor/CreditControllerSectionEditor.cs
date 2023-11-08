using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.CreditsSystem.Data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace Studio23.SS2.CreditsSystem.Editor
{
    public class CreditControllerSectionEditor : EditorWindow
    {
        private CreditSectionContentScriptableObject sectionContent;
        private HeaderData headerData;
        private SectionData _sectionData;
        private List<SectionData> sectionDataList = new List<SectionData>();

        void OnEnable()
        {
            headerData = new HeaderData();
            _sectionData = new SectionData();
            sectionDataList = new List<SectionData>();
        }

        [MenuItem("Studio-23/Credit System/Create Credit Section")]
        public static void ShowWindow()
        {
            GetWindow<CreditControllerSectionEditor>("Create Credit Section");
        }

        private void OnGUI()
        {
           
            GUILayout.Label("Credit Section Creation", EditorStyles.boldLabel);
            GUILayout.Label("Header Data", EditorStyles.boldLabel);
            headerData.HeaderImage = (Sprite)EditorGUILayout.ObjectField("Header Image", headerData.HeaderImage, typeof(Sprite), false);
            headerData.MainHeaderText = EditorGUILayout.TextField("Main Header Text", headerData.MainHeaderText);
            headerData.SubHeaderText = EditorGUILayout.TextField("Sub Header Text", headerData.SubHeaderText);
            headerData.OptionalHeaderText = EditorGUILayout.TextField("Optional Header Text", headerData.OptionalHeaderText);
            GUILayout.Label("Section Information", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace(); // Push the button to the right
           
            if (GUILayout.Button("Create Category", GUILayout.Width(150))) AddSectionData();
            GUILayout.EndHorizontal();
            for (var i = 0; i < sectionDataList.Count; i++)
            {
                GUILayout.Label(sectionDataList[i].SectionName, EditorStyles.boldLabel);
                sectionDataList[i].SectionName = EditorGUILayout.TextField("Section Name", sectionDataList[i].SectionName);
                CreateCategoryData(sectionDataList[i]);
            }
            if (GUILayout.Button("Create Credit Settings")) CreateAndSaveScriptableObject();
        }

        private void CreateCategoryData(SectionData sectionData)
        {
            for (var i = 0; i < sectionData.Sections.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                //GUILayout.Space(20); // Add left padding
                sectionData.Sections[i] = EditorGUILayout.TextField($"Sections {i + 1}:", sectionData.Sections[i]);
                if (GUILayout.Button("Remove", GUILayout.Width(80)) && sectionData.Sections.Count > 0) sectionData.Sections.RemoveAt(i);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Section", GUILayout.Width(150))) sectionData.Sections.Add("");
        }

        private void AddSectionData()
        {
          
            var newSection = new SectionData();
            sectionDataList.Add(newSection);
        }

        private void CreateAndSaveScriptableObject()
        {
            // Create a new instance of CreditSectionContentScriptableObject
            sectionContent = CreateInstance<CreditSectionContentScriptableObject>();

            // Assign the collected data to the ScriptableObject
            sectionContent.HeaderInformation = headerData;
            sectionContent.SectionInformation = sectionDataList;

            // Specify the path to save the ScriptableObject
            var path = "Assets/Packages/com.studio23.ss2.creditssystem/Samples/creditSection.asset"; // Set your desired folder and asset name

            // Create the asset and save it
            AssetDatabase.CreateAsset(sectionContent, path);
            EditorUtility.SetDirty(sectionContent);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


    }
}
