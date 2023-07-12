using TMPro;
using Tools23.CreditSystem.Settings;
using UnityEngine;

namespace Tools23.CreditSystem.Controller
{
	public class TextController : MonoBehaviour
	{
		public TextMeshProUGUI TextAsset;

		private void Awake()
		{
			TextAsset = GetComponent<TextMeshProUGUI>();
		}

		public void UpdateText(TextSettings settings)
		{
			TextAsset.font = settings.FontAsset;
			TextAsset.fontStyle = settings.FontStyle;

			TextAsset.UpdateFontAsset();
		}
	}
}
