using NaughtyAttributes;
using TMPro;
using Tools23.CreditSystem.Data;
using Tools23.CreditSystem.ScriptableObjects;
using Tools23.CreditSystem.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Tools23.CreditSystem.Core
{
	public class CreditController : MonoBehaviour
	{
		[Header("Header Style")]
		[OnValueChanged("GenerateSections")]
		public HeaderType HeaderType;

		public Transform ContentParent;

		public CreditSectionContentScriptableObject SectionData;
		public CreditControllerSettings SettingsData;

		[ContextMenu("Generate SectionInformation")]
		public void GenerateSections()
		{
			ClearSections();
			SetHeaderType();
			GenerateHorizontalSections();
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

		[Button("Vertical Layout")]
		public void GenerateVerticalSections()
		{
			ClearSections();
			SetHeaderType();
			foreach (var creditSection in SectionData.SectionInformation)
			{
				var sectionObj = Instantiate(SettingsData.SectionPrefabVertical, ContentParent);
				sectionObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = creditSection.SectionName;
				var sectionNames = sectionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
				sectionNames.text = "";
				foreach (var members in creditSection.Sections)
				{
					sectionNames.text += members + "\n";
				}
			}
		}

		[Button("Horizontal Layout")]
		public void GenerateHorizontalSections()
		{
			ClearSections();
			SetHeaderType();
			foreach (var creditSection in SectionData.SectionInformation)
			{
				var sectionObj = Instantiate(SettingsData.SectionPrefabHorizontal, ContentParent);
				sectionObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = creditSection.SectionName;
				var sectionNames = sectionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
				sectionNames.text = "";
				foreach (var members in creditSection.Sections)
				{
					sectionNames.text += members + "\n";
				}
			}
		}

		private void ClearSections()
		{
			int childCount = ContentParent.childCount;
			for (int i = 0; i < childCount; i++)
				DestroyImmediate(ContentParent.GetChild(0).gameObject);
		}
	}
}