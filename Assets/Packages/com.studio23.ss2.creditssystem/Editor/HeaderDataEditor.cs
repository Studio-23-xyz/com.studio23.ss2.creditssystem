using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.CreditsSystem.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.CreditsSystem.Editor
{
    [CustomEditor(typeof(HeaderData))]
    public class HeaderDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
          //  HeaderData headerData = (HeaderData)target;
           HeaderData headerData = new HeaderData();

            headerData.HeaderImage = (Sprite)EditorGUILayout.ObjectField("Header Image", headerData.HeaderImage, typeof(Sprite), false);
            headerData.MainHeaderText = EditorGUILayout.TextField("Main Header Text", headerData.MainHeaderText);
            headerData.SubHeaderText = EditorGUILayout.TextField("Sub Header Text", headerData.SubHeaderText);
            headerData.OptionalHeaderText = EditorGUILayout.TextField("Optional Header Text", headerData.OptionalHeaderText);
        }
    }

}
