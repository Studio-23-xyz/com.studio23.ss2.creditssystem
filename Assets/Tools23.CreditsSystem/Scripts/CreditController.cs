using System.Collections.Generic;
using TMPro;
using Tools23.CreditSystem.ScriptableObjects;
using UnityEngine;

namespace Tools23.CreditSystem.Core
{
	public class CreditController : MonoBehaviour
	{
		public GameObject HeaderSection;
		public GameObject FooterSection;

		public Transform ContentParent;

		[SerializeField] private GameObject _sectionPrefab;

		public List<CreditSectionContentScriptableObject> Sections = new List<CreditSectionContentScriptableObject>();

		public void GenerateSections()
		{
			ClearSections();
			foreach (var creditSection in Sections)
			{
				var sectionObj = Instantiate(_sectionPrefab);
				sectionObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = creditSection.SectionRole;
				var sectionNames = sectionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
				sectionNames.text = "";
				foreach (var members in creditSection.SectionMembers)
				{
					sectionNames.text += members + "\n";
				}
			}
		}

		private void ClearSections()
		{
			foreach (Transform child in ContentParent)
			{
				Destroy(child.gameObject);
			}
		}
	}
}