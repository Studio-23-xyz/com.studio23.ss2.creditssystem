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
        private GameObject imageTextPrefab;
        private GameObject imageHeaderPrefab;
        private GameObject textHeaderPrefab;
        private GameObject sectionPrefabHorizontal;
        private GameObject sectionPrefabVertical;
        private TextSettings headerFontSettings;
        private TextSettings roleHeaderFontSettings;
        private TextSettings roleMembersFontSettings;
        private GameObject spacerObject;

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

            imageTextPrefab = (GameObject)EditorGUILayout.ObjectField("Image Text Prefab", imageTextPrefab, typeof(GameObject), false);
            imageHeaderPrefab = (GameObject)EditorGUILayout.ObjectField("Image Header Prefab", imageHeaderPrefab, typeof(GameObject), false);
            textHeaderPrefab = (GameObject)EditorGUILayout.ObjectField("Text Header Prefab", textHeaderPrefab, typeof(GameObject), false);
            sectionPrefabHorizontal = (GameObject)EditorGUILayout.ObjectField("Section Prefab (Horizontal)", sectionPrefabHorizontal, typeof(GameObject), false);
            sectionPrefabVertical = (GameObject)EditorGUILayout.ObjectField("Section Prefab (Vertical)", sectionPrefabVertical, typeof(GameObject), false);

            GUILayout.Label("Header Font Settings", EditorStyles.boldLabel);
            headerFontSettings = DrawTextSettingsFields(headerFontSettings);

            GUILayout.Label("Role Header Font Settings", EditorStyles.boldLabel);
            roleHeaderFontSettings = DrawTextSettingsFields(roleHeaderFontSettings);

            GUILayout.Label("Role Members Font Settings", EditorStyles.boldLabel);
            roleMembersFontSettings = DrawTextSettingsFields(roleMembersFontSettings);

            spacerObject = (GameObject)EditorGUILayout.ObjectField("Spacer Object", spacerObject, typeof(GameObject), false);

            if (GUILayout.Button("Create Credit Settings")) CreateCreditControllerSettingsDataAsset();
        }

        private TextSettings DrawTextSettingsFields(TextSettings textSettings)
        {
            if (textSettings == null)
                textSettings = new TextSettings();

            textSettings.FontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", textSettings.FontAsset, typeof(TMP_FontAsset), false);
            textSettings.FontStyle = (FontStyles)EditorGUILayout.EnumPopup("Font Style", textSettings.FontStyle);

            return textSettings;
        }

        private void CreateCreditControllerSettingsDataAsset()
        {
            // Check if any required fields are null
            if (imageTextPrefab == null || imageHeaderPrefab == null || textHeaderPrefab == null ||
                sectionPrefabHorizontal == null || sectionPrefabVertical == null ||
                headerFontSettings == null || roleHeaderFontSettings == null || roleMembersFontSettings == null ||
                spacerObject == null)
            {
                // Show an error message in the editor GUI
                EditorUtility.DisplayDialog("Error", "All fields must be set.", "OK");
                return;
            }

            var creditSettings = CreateInstance<CreditControllerSettings>();
            creditSettings.ImageTextPrefab = imageTextPrefab;
            creditSettings.ImageHeaderPrefab = imageHeaderPrefab;
            creditSettings.TextHeaderPrefab = textHeaderPrefab;
            creditSettings.SectionPrefabHorizontal = sectionPrefabHorizontal;
            creditSettings.SectionPrefabVertical = sectionPrefabVertical;
            creditSettings.HeaderFontSettings = headerFontSettings;
            creditSettings.RoleHeaderFontSettings = roleHeaderFontSettings;
            creditSettings.RoleMembersFontSettings = roleMembersFontSettings;
            creditSettings.SpacerObject = spacerObject;

            var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Packages/com.studio23.ss2.creditssystem/Samples/creditsSettings.asset"); // Set the desired folder and asset name
            AssetDatabase.CreateAsset(creditSettings, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = creditSettings;
        }
    }


}

