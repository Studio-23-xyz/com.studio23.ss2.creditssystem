using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.CreditsSystem.Core;
using TMPro;
using UnityEditor;
using UnityEngine;


namespace Studio23.SS2.CreditsSystem.Editor
{
    [CustomEditor(typeof(CreditControllerSettings))]
    public class CreditControllerSettingsEditor : EditorWindow
    {
        private GameObject _imageTextPrefab;
        private GameObject _imageHeaderPrefab;
        private GameObject _textHeaderPrefab;
        private GameObject _sectionPrefabHorizontal;
        private GameObject _sectionPrefabVertical;
        private TextSettings _headerFontSettings;
        private TextSettings _roleHeaderFontSettings;
        private TextSettings _roleMembersFontSettings;
        private GameObject _spacerObject;

        private Texture _titleImage;

        [MenuItem("Studio-23/Credit System/Create Credit Settings", false, 2)]
        public static void ShowWindow()
        {
            GetWindow<CreditControllerSettingsEditor>("Create Credit Settings");
        }

        private void OnGUI()
        {
            if (_titleImage == null)
                // Load the title image from the Resources folder
                _titleImage = Resources.Load<Texture>("Images/creditSettings");

            // Display the title image at the top of the window
            if (_titleImage != null)
            {
                var titleRect = EditorGUILayout.GetControlRect(false, _titleImage.height);
                EditorGUI.DrawPreviewTexture(titleRect, _titleImage);
            }
            GUILayout.Label("Credit Settings Data Creation", EditorStyles.boldLabel);

            _imageTextPrefab =
                (GameObject)EditorGUILayout.ObjectField("Image Text Prefab", _imageTextPrefab, typeof(GameObject),
                    false);
            _imageHeaderPrefab = (GameObject)EditorGUILayout.ObjectField("Image Header Prefab", _imageHeaderPrefab,
                typeof(GameObject), false);
            _textHeaderPrefab =
                (GameObject)EditorGUILayout.ObjectField("Text Header Prefab", _textHeaderPrefab, typeof(GameObject),
                    false);
            _sectionPrefabHorizontal = (GameObject)EditorGUILayout.ObjectField("Section Prefab (Horizontal)",
                _sectionPrefabHorizontal, typeof(GameObject), false);
            _sectionPrefabVertical = (GameObject)EditorGUILayout.ObjectField("Section Prefab (Vertical)",
                _sectionPrefabVertical, typeof(GameObject), false);

            GUILayout.Label("Header Font Settings", EditorStyles.boldLabel);
            _headerFontSettings = DrawTextSettingsFields(_headerFontSettings);

            GUILayout.Label("Role Header Font Settings", EditorStyles.boldLabel);
            _roleHeaderFontSettings = DrawTextSettingsFields(_roleHeaderFontSettings);

            GUILayout.Label("Role Members Font Settings", EditorStyles.boldLabel);
            _roleMembersFontSettings = DrawTextSettingsFields(_roleMembersFontSettings);

            _spacerObject =
                (GameObject)EditorGUILayout.ObjectField("Spacer Object", _spacerObject, typeof(GameObject), false);

            if (GUILayout.Button("Create Credit Settings")) CreateCreditControllerSettingsDataAsset();
        }

        private TextSettings DrawTextSettingsFields(TextSettings textSettings)
        {
            if (textSettings == null)
                textSettings = new TextSettings();

            textSettings.FontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", textSettings.FontAsset,
                typeof(TMP_FontAsset), false);
            textSettings.FontStyle = (FontStyles)EditorGUILayout.EnumPopup("Font Style", textSettings.FontStyle);

            return textSettings;
        }

        private void CreateCreditControllerSettingsDataAsset()
        {
            // Check if any required fields are null
            if (_imageTextPrefab == null || _imageHeaderPrefab == null || _textHeaderPrefab == null ||
                _sectionPrefabHorizontal == null || _sectionPrefabVertical == null ||
                _headerFontSettings == null || _roleHeaderFontSettings == null || _roleMembersFontSettings == null ||
                _spacerObject == null)
            {
                // Show an error message in the editor GUI
                EditorUtility.DisplayDialog("Error", "All fields must be set.", "OK");
                return;
            }

            var creditSettings = CreateInstance<CreditControllerSettings>();
            creditSettings.ImageTextPrefab = _imageTextPrefab;
            creditSettings.ImageHeaderPrefab = _imageHeaderPrefab;
            creditSettings.TextHeaderPrefab = _textHeaderPrefab;
            creditSettings.SectionPrefabHorizontal = _sectionPrefabHorizontal;
            creditSettings.SectionPrefabVertical = _sectionPrefabVertical;
            creditSettings.HeaderFontSettings = _headerFontSettings;
            creditSettings.RoleHeaderFontSettings = _roleHeaderFontSettings;
            creditSettings.RoleMembersFontSettings = _roleMembersFontSettings;
            creditSettings.SpacerObject = _spacerObject;

            string path = EditorUtility.SaveFilePanelInProject(
                "Save Credit Settings",
                "creditSettings",
                "asset",
                "Please enter a file name to save the credit settings to",
                "Assets/Packages/com.studio23.ss2.creditssystem/Samples/"
            ); // Set the desired folder and asset name

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(creditSettings, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = creditSettings;
            }
        }
    }


}

