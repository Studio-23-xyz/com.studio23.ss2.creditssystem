using System.Collections;
using System.Collections.Generic;
using Tools23.CreditSystem.ScriptableObjects;
using UnityEngine;

namespace Tools23.CreditSystem.Core
{
	public class CreditController : MonoBehaviour
	{
		public GameObject HeaderSection;
		public GameObject FooterSection;

		[SerializeField] private GameObject _sectionPrefab;

		public List<CreditSectionContentScriptableObject> Sections = new List<CreditSectionContentScriptableObject>();
	}
}