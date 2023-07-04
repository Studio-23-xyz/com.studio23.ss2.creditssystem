using System.Collections.Generic;
using UnityEngine;

namespace Tools23.CreditSystem.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Credit Section", menuName = "Tools-23/Credit System/Credit Section", order = 100)]
	public class CreditSectionContentScriptableObject : ScriptableObject
	{
		public string SectionRole;

		public List<string> SectionMembers = new List<string>();
	}
}