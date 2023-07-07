using System.Collections.Generic;
using Tools23.CreditSystem.Data;
using UnityEngine;

namespace Tools23.CreditSystem.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Credit Section", menuName = "Tools-23/Credit System/Credit Section", order = 100)]
	public class CreditSectionContentScriptableObject : ScriptableObject
	{
		public HeaderData HeaderInformation;

		public List<SectionData> SectionInformation = new List<SectionData>();
	}
}