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
        private CreditSectionContentScriptableObject _sectionContent;
        private HeaderData _headerData;
        private SectionData _sectionData;
        private List<SectionData> _sectionDataList = new();
        private Texture _titleImage;

        private void OnEnable()
        {
            _headerData = new HeaderData();
            _sectionData = new SectionData();
            _sectionDataList = new List<SectionData>();
        }

        [MenuItem("Studio-23/Credit System/Create Credit Section")]
        public static void ShowWindow()
        {
            GetWindow<CreditControllerSectionEditor>("Create Credit Section");
        }

        private void OnGUI()
        {
            if (_titleImage == null)
                // Load the title image from the Resources folder
                _titleImage = Resources.Load<Texture>("Images/creditSection");

            // Display the title image at the top of the window
            if (_titleImage != null)
            {
                var titleRect = EditorGUILayout.GetControlRect(false, _titleImage.height);
                EditorGUI.DrawPreviewTexture(titleRect, _titleImage);
            }

            GUILayout.Label("Credit Section Creation", EditorStyles.boldLabel);
            GUILayout.Label("Header Data", EditorStyles.boldLabel);
            _headerData.HeaderImage =
                (Sprite)EditorGUILayout.ObjectField("Header Image", _headerData.HeaderImage, typeof(Sprite), false);
            _headerData.MainHeaderText = EditorGUILayout.TextField("Main Header Text", _headerData.MainHeaderText);
            _headerData.SubHeaderText = EditorGUILayout.TextField("Sub Header Text", _headerData.SubHeaderText);
            _headerData.OptionalHeaderText =
                EditorGUILayout.TextField("Optional Header Text", _headerData.OptionalHeaderText);
            GUILayout.Label("Section Information", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace(); // Push the button to the right

            if (GUILayout.Button("Create Category", GUILayout.Width(150))) AddSectionData();
            GUILayout.EndHorizontal();
            for (var i = 0; i < _sectionDataList.Count; i++)
            {
                GUILayout.Label(_sectionDataList[i].SectionName, EditorStyles.boldLabel);
                _sectionDataList[i].SectionName =
                    EditorGUILayout.TextField("Section Name", _sectionDataList[i].SectionName);
                CreateCategoryData(_sectionDataList[i]);
            }

            if (GUILayout.Button("Create Credit Settings")) CreateAndSaveScriptableObject();
        }

        /// <summary>
        /// This method will create new category data based on SectionData asset
        /// </summary>
        /// <param name="sectionData"></param>
        private void CreateCategoryData(SectionData sectionData)
        {
            for (var i = 0; i < sectionData.Sections.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                //GUILayout.Space(20); // Add left padding
                sectionData.Sections[i] = EditorGUILayout.TextField($"Sections {i + 1}:", sectionData.Sections[i]);
                if (GUILayout.Button("Remove", GUILayout.Width(80)) && sectionData.Sections.Count > i)
                    sectionData.Sections.RemoveAt(i);
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Section", GUILayout.Width(150))) sectionData.Sections.Add("");
        }

        private void AddSectionData()
        {
            var newSection = new SectionData();
            _sectionDataList.Add(newSection);
        }

        private void CreateAndSaveScriptableObject()
        {
            // Create a new instance of CreditSectionContentScriptableObject
            _sectionContent = CreateInstance<CreditSectionContentScriptableObject>();

            // Assign the collected data to the ScriptableObject
            _sectionContent.HeaderInformation = _headerData;
            _sectionContent.SectionInformation = _sectionDataList;

            // Specify the path to save the ScriptableObject
            string path = EditorUtility.SaveFilePanelInProject(
                "Save Credit Section",
                "creditSection",
                "asset",
                "Please enter a file name to save the credit section to",
                "Assets/Packages/com.studio23.ss2.creditssystem/Samples/"
            );

            if (!string.IsNullOrEmpty(path))
            {
                // Create the asset and save it
                AssetDatabase.CreateAsset(_sectionContent, path);
                EditorUtility.SetDirty(_sectionContent);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
