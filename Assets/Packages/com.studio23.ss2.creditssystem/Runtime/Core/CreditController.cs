using Studio23.SS2.CreditsSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio23.SS2.CreditsSystem.Core
{
	[ExecuteInEditMode]
	public class CreditController : MonoBehaviour
	{
		public bool ScrollOnStart;
		public float ScrollSpeed;
		public float ScrollTime;

		[Header("Header Style")]
		public HeaderType HeaderType;

		public Transform ContentParent;

		public CreditSectionContentScriptableObject SectionData;
		//[Expandable]
		public CreditControllerSettings SettingsData;

        [SerializeField] private ScrollViewController _scrollViewController;
		private Transform _headerTransform;

		public UnityEvent OnCreditRollCompleted;

		private void Start()
        {
            _scrollViewController = GetComponent<ScrollViewController>();
			SettingsData.OnHeaderSettingsChanged += new CreditControllerSettings.DataChangeEvent(UpdateHeaderSettings);
			SettingsData.OnRoleNameSettingsChanged += new CreditControllerSettings.DataChangeEvent(UpdateRoleNames);
			SettingsData.OnRoleMemberSettingsChanged += new CreditControllerSettings.DataChangeEvent(UpdateRoleMembers);

			if (ScrollOnStart)
				StartScrollingDebug();
		}

		[ContextMenu("Start Scrolling")]
		public void StartScrollingDebug()
        {

               _scrollViewController.isScrolling = true;
            //_targetScrollRect.DOVerticalNormalizedPos(0f, ScrollTime).SetEase(Ease.Linear).OnComplete(() =>
            //{
            //    OnCreditRollCompleted?.Invoke();
            //});
        }

		#region Settings Update

		private void UpdateFontSettings()
		{
			UpdateHeaderSettings(SettingsData.HeaderFontSettings);
			UpdateRoleNames(SettingsData.RoleHeaderFontSettings);
			UpdateRoleMembers(SettingsData.RoleMembersFontSettings);
		}

		private void UpdateRoleNames(TextSettings textSettings)
		{
			SetFontSettings(textSettings, 0);
		}

		private void UpdateRoleMembers(TextSettings textSettings)
		{
			SetFontSettings(textSettings, 1);
		}

		private void SetFontSettings(TextSettings textSettings, int childIndex)
		{
			for (int i = 1; i < ContentParent.childCount; i++)
			{
				Debug.Log($"Currently iterating on {ContentParent.GetChild(i).name}");
				TextMeshProUGUI textAsset = ContentParent.GetChild(i).GetChild(childIndex).GetComponent<TextMeshProUGUI>();
				textAsset.font = textSettings.FontAsset;
				textAsset.fontStyle = textSettings.FontStyle;
				//textAsset.UpdateFontAsset();
			}
		}

		private void UpdateHeaderSettings(TextSettings textSettings)
		{
			ContentParent.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().font = textSettings.FontAsset;
			ContentParent.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().fontStyle = textSettings.FontStyle;
			ContentParent.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().UpdateFontAsset();
		}

		#endregion

		[ContextMenu("Generate SectionInformation")]
		public void GenerateSections()
		{
			ClearSections();
			SetHeaderType();
			GenerateVerticalSections();
		}

		private void SetHeaderType()
		{
			GameObject headerObject;
			if (HeaderType == HeaderType.TextAndImage)
				headerObject = Instantiate(SettingsData.ImageTextPrefab, ContentParent);
			else if (HeaderType == HeaderType.TextOnly)
				headerObject = Instantiate(SettingsData.TextHeaderPrefab, ContentParent);
			else
				headerObject = Instantiate(SettingsData.ImageHeaderPrefab, ContentParent);

			SetupHeaderParams(headerObject);
			headerObject.transform.SetAsFirstSibling();
			_headerTransform = headerObject.transform;
		}

		private void SetupHeaderParams(GameObject headerObject)
		{
			var headerImage = headerObject.GetComponentInChildren<Image>();
			if (headerImage != null)
				headerImage.sprite = SectionData.HeaderInformation.HeaderImage;
			var headerText = headerObject.GetComponentInChildren<TextMeshProUGUI>();
			if (headerText != null)
			{
				var tempString = headerText.text;
				tempString = tempString.Replace("Main", SectionData.HeaderInformation.MainHeaderText);
				tempString = tempString.Replace("Sub", SectionData.HeaderInformation.SubHeaderText);
				tempString = tempString.Replace("Optional", SectionData.HeaderInformation.OptionalHeaderText);
				headerText.text = tempString;
			}
		}

		public void GenerateVerticalSections()
		{
			ClearSections();
			SetHeaderType();
			foreach (var creditSection in SectionData.SectionInformation)
			{
				var sectionObj = Instantiate(SettingsData.SectionPrefabVertical, ContentParent);
				sectionObj.name = creditSection.SectionName;
				sectionObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = creditSection.SectionName;
				var sectionNames = sectionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
				sectionNames.text = "";
				foreach (var members in creditSection.Sections)
				{
					sectionNames.text += members + "\n";
				}
			}
			UpdateFontSettings();
			
		}

		public void GenerateHorizontalSections()
		{
			ClearSections();
			SetHeaderType();
			foreach (var creditSection in SectionData.SectionInformation)
			{
				var sectionObj = Instantiate(SettingsData.SectionPrefabHorizontal, ContentParent);
				sectionObj.name = creditSection.SectionName;
				sectionObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = creditSection.SectionName;
				var sectionNames = sectionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
				sectionNames.text = "";
				foreach (var members in creditSection.Sections)
				{
					sectionNames.text += members + "\n";
				}
			}
			UpdateFontSettings();
			
		}

		private void SetupSpacerObjects()
		{
			GameObject spacerObj = Instantiate(SettingsData.SpacerObject, ContentParent);
			spacerObj.transform.SetAsFirstSibling();
			spacerObj = Instantiate(SettingsData.SpacerObject, ContentParent);
			spacerObj.transform.SetAsLastSibling();
		}

		private void ClearSections()
		{
			int childCount = ContentParent.childCount;
			for (int i = 0; i < childCount; i++)
				DestroyImmediate(ContentParent.GetChild(0).gameObject);
		}


	}
}