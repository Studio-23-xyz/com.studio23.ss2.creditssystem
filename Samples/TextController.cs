using Studio23.SS2.CreditsSystem.Core;
using TMPro;
using UnityEngine;


	public class TextController : MonoBehaviour
	{
		private TextMeshProUGUI _textAsset;

		private void Awake()
		{
			_textAsset = GetComponent<TextMeshProUGUI>();
		}

		public void UpdateText(TextSettings settings)
		{
			_textAsset.font = settings.FontAsset;
			_textAsset.fontStyle = settings.FontStyle;

			_textAsset.UpdateFontAsset();
		}
	}
