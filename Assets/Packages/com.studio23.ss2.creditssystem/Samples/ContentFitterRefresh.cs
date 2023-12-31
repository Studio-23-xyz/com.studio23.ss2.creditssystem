using UnityEngine.UI;
using UnityEngine;

public class ContentFitterRefresh : MonoBehaviour
{
	private void Awake()
	{
		RefreshContentFitters();
	}

	[ContextMenu("Refresh Content Fitter")]
	public void RefreshContentFitters()
	{
		var rectTransform = (RectTransform)transform;
		RefreshContentFitter(rectTransform);
	}

	private void RefreshContentFitter(RectTransform transform)
	{
		if (transform == null || !transform.gameObject.activeSelf)
		{
			return;
		}

		foreach (RectTransform child in transform)
		{
			RefreshContentFitter(child);
		}

		var layoutGroup = transform.GetComponent<LayoutGroup>();
		var contentSizeFitter = transform.GetComponent<ContentSizeFitter>();
		if (layoutGroup != null)
		{
			layoutGroup.SetLayoutHorizontal();
			layoutGroup.SetLayoutVertical();
		}

		if (contentSizeFitter != null)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
		}
	}
}