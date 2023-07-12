using System;
using NaughtyAttributes;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Tools23.CreditSystem.Settings
{
	[Serializable]
	[CreateAssetMenu(fileName = "Credit Settings", menuName = "Tools-23/Credit System/Credit Settings", order = 99)]
	public class CreditControllerSettings : ScriptableObject
	{

		public delegate void DataChangeEvent(TextSettings textSettings);

		public DataChangeEvent OnHeaderSettingsChanged;
		public DataChangeEvent OnRoleNameSettingsChanged;
		public DataChangeEvent OnRoleMemberSettingsChanged;

		[BoxGroup("Header Styles")]
		[HorizontalLine(color: EColor.Black)]
		public GameObject ImageTextPrefab;
		[BoxGroup("Header Styles")]
		public GameObject ImageHeaderPrefab;
		[BoxGroup("Header Styles")]
		public GameObject TextHeaderPrefab;

		[BoxGroup("Section Styles")]
		[HorizontalLine(color: EColor.Black)]
		public GameObject SectionPrefabHorizontal;
		[BoxGroup("Section Styles")]
		public GameObject SectionPrefabVertical;

		[OnValueChanged("OnHeaderSizeChange")]
		public TextSettings HeaderFontSettings;
		[OnValueChanged("OnRoleNameSizeChange")]
		public TextSettings RoleHeaderFontSettings;
		[OnValueChanged("OnRoleMemberSizeChange")]
		public TextSettings RoleMembersFontSettings;

		public GameObject SpacerObject;

		private void OnHeaderSizeChange()
		{
			OnHeaderSettingsChanged?.Invoke(HeaderFontSettings);
		}

		private void OnRoleNameSizeChange()
		{
			OnRoleNameSettingsChanged?.Invoke(RoleHeaderFontSettings);
		}

		private void OnRoleMemberSizeChange()
		{
			OnRoleMemberSettingsChanged?.Invoke(RoleMembersFontSettings);
		}
	}

	[Serializable]
	public class TextSettings
	{
		public TMP_FontAsset FontAsset;
		public FontStyles FontStyle;
	}
}