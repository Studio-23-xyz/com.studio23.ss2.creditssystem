using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.CreditsSystem.Data
{
	[CreateAssetMenu(fileName = "Credit Section", menuName = "Studio-23/Credit System/Credit Section", order = 100)]
	public class CreditSectionContentScriptableObject : ScriptableObject
	{
		public HeaderData HeaderInformation;
		public List<SectionData> SectionInformation = new List<SectionData>();
	}
}