using Studio23.SS2.CreditsSystem.Core;
using TMPro;
using UnityEngine;


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
