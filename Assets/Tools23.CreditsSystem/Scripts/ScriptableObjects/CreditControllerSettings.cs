using NaughtyAttributes;
using UnityEngine;

namespace Tools23.CreditSystem.Settings
{
	[CreateAssetMenu(fileName = "Credit Settings", menuName = "Tools-23/Credit System/Credit Settings", order = 99)]
	public class CreditControllerSettings : ScriptableObject
	{
		[BoxGroup("Font Settings")]
		public int HeaderSize;
		[BoxGroup("Font Settings")]
		public int RoleTextSize;
		[BoxGroup("Font Settings")]
		public int RoleMembersTextSize;

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
	}
}