using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MainMenuSettings.Extensions;

namespace MainMenuSettings
{
	public static class MenuComponents 
	{
		internal static void ScrollToElement(GameObject element) {
			ScrollRect scrollRect = GameObject.Find("Canvas").Find("ModSettings").Find("Scroller").GetComponent<ScrollRect>();
			Vector3 targetPosition = scrollRect.viewport.InverseTransformPoint(element.GetComponent<RectTransform>().position);
			
			int offset = 40;

			float viewportTop = scrollRect.viewport.rect.yMax - offset;
			float viewportBottom = scrollRect.viewport.rect.yMin + offset;

			if (targetPosition.y > viewportTop) {
				float scrollAmount = (targetPosition.y - viewportTop) / scrollRect.content.rect.height;
				int maxIterations = 100;
				int iterations = 0;
				while (targetPosition.y > viewportTop && iterations < maxIterations) {
					scrollRect.verticalNormalizedPosition += scrollAmount;
					targetPosition = scrollRect.viewport.InverseTransformPoint(element.GetComponent<RectTransform>().position); 
					iterations++;
				}
			}
			else if (targetPosition.y < viewportBottom) {
				float scrollAmount = (viewportBottom - targetPosition.y) / scrollRect.content.rect.height;
				int maxIterations = 100;
				int iterations = 0;
				while (targetPosition.y < viewportBottom && iterations < maxIterations) {
					scrollRect.verticalNormalizedPosition -= scrollAmount;
					targetPosition = scrollRect.viewport.InverseTransformPoint(element.GetComponent<RectTransform>().position);
					iterations++;
				}
			}
		}
		public static Font GetFont(string name)
		{
			UnityEngine.Object[] fonts = Resources.FindObjectsOfTypeAll(typeof(Font));
			foreach (Font font in fonts.Cast<Font>())
			{
				if (font.name == name) return font;
			}
			return null;
		}
		public static GameObject CreateButton(string name, string txt) 
		{
			Font Bold = GetFont("Orbitron-Bold");
			GameObject Button = new(name);
			Button.AddComponent<RectTransform>();
			Button.transform.localScale = new(1.2392f, 1.2392f, 1.2392f);
			Button.AddComponent<Button>();
			Button.AddComponent<LayoutElement>().preferredHeight = 25;
			GameObject Label = Button.AddObject("Label");
			Label.AddComponent<RectTransform>().anchoredPosition = new(14.9559f, -1);
			Label.AddComponent<CanvasRenderer>();
			Label.transform.localScale = new(0.15f, 0.15f, 0.15f);
			Text text = Label.AddComponent<Text>();
			text.font = Bold;
			text.text = txt;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.color = new(0.984f, 0.690f, 0.231f);
			text.fontSize = 100;
			text.alignment = TextAnchor.MiddleLeft;
			return Button;
		}
		
		public static GameObject CreateToggle(string name, string txt) 
		{
			Font Bold = GetFont("Orbitron-Bold");
			GameObject Button = new(name);
			Button.AddComponent<RectTransform>();
			Button.transform.localScale = new(1.2392f, 1.2392f, 1.2392f);
			Button.AddComponent<LayoutElement>().preferredHeight = 25;
			Toggle toggle = Button.AddComponent<Toggle>();
			GameObject Label = Button.AddObject("Label");
			Label.AddComponent<RectTransform>().anchoredPosition = new(23.6223f, -1);
			Label.AddComponent<CanvasRenderer>();
			Label.transform.localScale = new(0.15f, 0.15f, 0.15f);
			Text text = Label.AddComponent<Text>();
			text.font = Bold;
			text.text = txt;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.color = new(0.984f, 0.690f, 0.231f);
			text.alignment = TextAnchor.MiddleLeft;
			text.fontSize = 100;
			GameObject background = Button.AddObject("Background");
			background.AddComponent<RectTransform>().sizeDelta = new(20, 20);
			background.AddComponent<CanvasRenderer>();
			background.AddComponent<Image>().sprite = Plugin.CheckmarkBackground;
			GameObject checkmark = background.AddObject("Checkmark");
			checkmark.AddComponent<RectTransform>().sizeDelta = new(15, 15);
			checkmark.AddComponent<CanvasRenderer>();
			checkmark.AddComponent<Image>().sprite = Plugin.Checkmark;
			toggle.onValueChanged.AddListener(checkmark.SetActive);
			return Button;
		}
	}
}