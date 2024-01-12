using System;
using TMPro;
using UnityEngine;

namespace Studio23.SS2.CreditsSystem.Core
{
	[Serializable]
	[CreateAssetMenu(fileName = "Credit Settings", menuName = "Studio-23/Credit System/Credit Settings", order = 99)]
	public class CreditControllerSettings : ScriptableObject
	{

		public delegate void DataChangeEvent(TextSettings textSettings);

		public DataChangeEvent OnHeaderSettingsChanged;
		public DataChangeEvent OnRoleNameSettingsChanged;
		public DataChangeEvent OnRoleMemberSettingsChanged;


		public GameObject ImageTextPrefab;
		public GameObject ImageHeaderPrefab;
		public GameObject TextHeaderPrefab;


		public GameObject SectionPrefabHorizontal;
		public GameObject SectionPrefabVertical;


		public TextSettings HeaderFontSettings;
		public TextSettings RoleHeaderFontSettings;
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