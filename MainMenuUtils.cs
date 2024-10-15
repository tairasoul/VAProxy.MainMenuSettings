using UnityEngine;
using UnityEngine.UI;
using MainMenuSettings.Extensions;

namespace MainMenuSettings 
{
	public static class MainMenuUtils 
	{
		internal static KeyboardTrigger.Key Keybind = new() 
		{
			key = KeyCode.G,
			Event = new()
		};
		internal static void CreateText() 
		{
			GameObject Button = new("Mod Settings");
			RectTransform transform = Button.AddComponent<RectTransform>();
			GameObject startMenu = GameObject.Find("Canvas").Find("StartMenu");
			Button.SetParent(startMenu, false);
			transform.anchoredPosition = new(102.3593f, -93.0704f);
			transform.sizeDelta = new(122.69f, 35);
			transform.anchorMax = new(0, 1);
			transform.anchorMin = new(0, 1);
			Image ButtonBG = Button.AddComponent<Image>();
			ButtonBG.sprite = Plugin.ButtonImage;
			ButtonBG.pixelsPerUnitMultiplier = 0.91f;
			ButtonBG.type = Image.Type.Sliced;
			GameObject Text = Button.AddObject("Text");
			RectTransform TTransform = Text.AddComponent<RectTransform>();
			TTransform.anchoredPosition = new(28.655f, 0);
			TTransform.sizeDelta = new(320, 60);
			TTransform.localScale = new(0.5f, 0.5f, 0.5f);
			Text text = Text.AddComponent<Text>();
			text.fontSize = 55;
			text.font = Find<Font>((v) => v.name == "Jupiter");
			text.text = "Mods";
			text.material = Find<Material>((v) => v.name == "wordsArtificer");
			GameObject select = Button.AddObject("select");
			RectTransform sTransform = select.AddComponent<RectTransform>();
			sTransform.eulerAngles = new(50, 270, 180);
			sTransform.anchoredPosition = new(78.2f, 0);
			sTransform.sizeDelta = new(30, 30);
			Image sImage = select.AddComponent<Image>();
			sImage.sprite = Plugin.RotateSprite;
			Rotate rotate = select.AddComponent<Rotate>();
			select.SetActive(false);
			rotate.speed = new(500, 0, 0);
			ChoiceSelect cSelect = startMenu.GetComponent<ChoiceSelect>();
			ChoiceSelect.Choices choice = new() 
			{
				OnHighlight = new(),
				OnPick = new()
			};
			choice.OnHighlight.AddListener(() => {
				select.SetActive(true);
				startMenu.Find("Start").Find("select").SetActive(false);
				startMenu.Find("Discord").Find("select").SetActive(false);
				startMenu.Find("Twitter").Find("select").SetActive(false);
				startMenu.Find("Exit").Find("select").SetActive(false);
				startMenu.Find("Options").Find("select").SetActive(false);
				startMenu.Find("Credits").Find("select").SetActive(false);
			});
			choice.OnPick.AddListener(() => 
			{
				startMenu.SetActive(false);
				GameObject SettingsPage = GameObject.Find("Canvas").Find("ModSettings");
				SettingsPage.SetActive(true);
				RectTransform PT = SettingsPage.GetComponent<RectTransform>();
				PT.rotation = new(0, -0.7071f, 0, 0.7071f);
				PT.sizeDelta = new(-167.86f, 18.7651f);
				PT.anchorMax = new(1, 1);
				PT.anchorMin = new(0, 0);
				PT.anchoredPosition = new(14.17f, 0.73f);
			});
			ChoiceSelect.Choices[] choices = [];
			cSelect.Choice.First().OnHighlight.AddListener(() => 
			{
				select.SetActive(false);
			});
			choices = [ cSelect.Choice.First() ];
			choices = [ .. choices, choice ];
			for (int i = 1; i < cSelect.Choice.Length; i++)  
			{
				cSelect.Choice[i].OnHighlight.AddListener(() => 
				{
					select.SetActive(false);
				});
				choices = [ .. choices, cSelect.Choice[i]];
			}
			cSelect.Choice = choices;
		}
		static T Find<T>(Func<T, bool> predicate)
		{
			foreach (T find in Resources.FindObjectsOfTypeAll(typeof(T)).Cast<T>())
			{
				if (predicate(find)) return find;
			}
			return default;
		}
		
		internal static void CreateSettingsPage() 
		{
			/*GameObject BasePage = GameObject.Find("Canvas").Find("Optimize");
			GameObject cloned = BasePage.Instantiate();
			cloned.SetActive(false);
			cloned.SetParent(GameObject.Find("Canvas"), false);
			RectTransform transform = cloned.GetComponent<RectTransform>();
			transform.localScale = new(1, 1, 1);
			transform.anchoredPosition = new(14.17f, 0.73f);
			transform.sizeDelta = new(-167.86f, -13.83f);
			cloned.name = "Settings";
			KeyboardTrigger comp = cloned.GetComponent<KeyboardTrigger>();
			comp.Choices.First().Event.AddListener(() => 
			{
				GameObject Background = GameObject.Find("Canvas").Find("Image");
				GameObject SettingsPage = GameObject.Find("Canvas").Find("Settings");
				Background.SetActive(true);
				SettingsPage.SetActive(false);
			});
			GameObject.Destroy(cloned.GetComponent<SettingsMainMenu>());
			cloned.AddComponent<SettingsHandler>();
			GameObject.Destroy(cloned.Find("StaticAI"));
			GameObject.Destroy(cloned.Find("DynamicAI"));
			GameObject.Destroy(cloned.Find("Shadow"));
			GameObject.Destroy(cloned.Find("Graphics"));*/
			GameObject Page = new("ModSettings");
			Page.AddComponent<RectTransform>();
			CanvasRenderer rend = Page.AddComponent<CanvasRenderer>();
			Page.AddComponent<Image>().color = new(0, 0, 0, 0.447f);
			rend.materialCount = 1;
			Material origin = Find((Material m) =>
			{
				return m.name == "Default UI Material";
			});
			Material newM = new(origin)
			{
				name = "Modified UI Material",
				renderQueue = origin.renderQueue + 100
			};
			rend.SetMaterial(newM, 0);
			KeyboardTrigger.Key key = new()
			{
				key = KeyCode.Escape,
				Event = new()
			};
			key.Event.AddListener(() => 
			{
				Page.SetActive(false);
				GameObject.Find("Canvas").Find("StartMenu").SetActive(true);
			});
			KeyboardTrigger trigger = Page.AddComponent<KeyboardTrigger>();
			trigger.Choices = [key];
			trigger.dis = false;
			Page.AddComponent<SettingsHandler>();
			GameObject Text = new("EscapeText");
			RectTransform txtt = Text.AddComponent<RectTransform>();
			txtt.anchoredPosition = new(275.37f, -220.685f);
			txtt.sizeDelta = new(220.05f, 91.79f);
			Text txt = Text.AddComponent<Text>();
			txt.text = "Esc/ Return";
			txt.font = MenuComponents.GetFont("Arial");
			txt.fontSize = 15;
			txt.alignment = TextAnchor.UpperLeft;
			Text.SetParent(Page, true);
			GameObject Scroller = CreateScrollingPage();
			Scroller.SetParent(Page, false);
			Scroller.name = "Scroller";
			Scroller.GetComponent<RectTransform>().anchoredPosition = new(223.7368f, 64.6945f);
			Page.SetActive(false);
			Page.SetParent(GameObject.Find("Canvas"), false);
			Page.transform.localScale = new(1, 1, 1);
		}
		
		internal static GameObject CreateModPage(string modName, string modId) 
		{
			GameObject Page = new("Settings");
			Page.AddComponent<RectTransform>();
			CanvasRenderer rend = Page.AddComponent<CanvasRenderer>();
			Page.AddComponent<Image>().color = new(0, 0, 0, 0.447f);
			rend.materialCount = 1;
			Material origin = Find((Material m) =>
			{
				return m.name == "Default UI Material";
			});
			Material newM = new(origin)
			{
				name = "Modified UI Material",
				renderQueue = origin.renderQueue + 100
			};
			rend.SetMaterial(newM, 0);
			GameObject scroller = CreateScrollingPage();
			KeyboardTrigger.Key key = new()
			{
				key = KeyCode.Escape,
				Event = new()
			};
			key.Event.AddListener(() => 
			{
				Page.SetActive(false);
				GameObject.Find("Canvas").Find("Settings").SetActive(true);
			});
			KeyboardTrigger trigger = Page.AddComponent<KeyboardTrigger>();
			trigger.Choices = [key];
			trigger.dis = false;
			Page.AddComponent<SettingsHandler>();
			GameObject Text = new("EscapeText");
			RectTransform txtt = Text.AddComponent<RectTransform>();
			txtt.anchoredPosition = new(275.37f, -220.685f);
			txtt.sizeDelta = new(220.05f, 91.79f);
			Text txt = Text.AddComponent<Text>();
			txt.text = "Esc/ Return";
			txt.font = MenuComponents.GetFont("Arial");
			txt.fontSize = 15;
			txt.alignment = TextAnchor.UpperLeft;
			Text.SetParent(Page, true);
			GameObject TopText = new("TopText");
			RectTransform Toptxtt = TopText.AddComponent<RectTransform>();
			Toptxtt.anchoredPosition = new(57.421f, 149.4127f);
			Toptxtt.sizeDelta = new(220.05f, 91.79f);
			Text Toptxt = TopText.AddComponent<Text>();
			Toptxt.text = modName;
			Toptxt.font = MenuComponents.GetFont("Arial");
			Toptxt.fontSize = 15;
			Toptxt.alignment = TextAnchor.UpperLeft;
			TopText.SetParent(Page, true);
			scroller.name = "Scroller";
			scroller.SetParent(Page, false);
			scroller.GetComponent<RectTransform>().anchoredPosition = new(223.7368f, 64.6945f);
			Page.name = modId;
			Page.SetActive(false);
			Page.SetParent(GameObject.Find("Canvas"), false);
			Page.transform.localScale = new(1, 1, 1);
			return Page;
		}
		public static GameObject CreateScrollingPage() 
		{
			GameObject Page = new();
			RectTransform PT = Page.AddComponent<RectTransform>();
			Page.AddComponent<CanvasRenderer>();
			ScrollRect rect = Page.AddComponent<ScrollRect>();
			GameObject Scrollbar = GameObject.Find("Canvas").Find("SlotSelect").Find("Scroll View").Find("Scrollbar Vertical").Instantiate();
			Scrollbar.SetParent(Page, true);
			RectTransform sT = Scrollbar.GetComponent<RectTransform>();
			sT.anchoredPosition = new(172.5f, -243);
			sT.localScale = new(1, 19, 1);
			GameObject Viewport = Page.AddObject("Viewport");
			Viewport.AddComponent<Image>().sprite = GameObject.Find("Canvas").Find("SlotSelect").Find("Scroll View").Find("Viewport").GetComponent<Image>().sprite;
			//Viewport.AddComponent<Mask>();
			RectTransform vT = Viewport.GetComponent<RectTransform>();
			vT.sizeDelta = new(400, 320);
			vT.anchoredPosition = new(-230.2206f, -68.943f);
			vT.anchorMax = new(1, 1);
			vT.anchorMin = new(0, 0);
			GameObject Content = Viewport.AddObject("Content");
			RectTransform cT = Content.AddComponent<RectTransform>();
			cT.anchoredPosition = new(-194.8779f, -249.9995f);
			cT.sizeDelta = new(-45, 358);
			cT.anchorMax = new(1, 1);
			cT.anchorMin = new(0, 1);
			Content.AddComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
			ContentSizeFitter fitter = Content.AddComponent<ContentSizeFitter>();
			fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			rect.content = Content.GetComponent<RectTransform>();
			rect.horizontal = false;
			rect.inertia = true;
			rect.movementType = ScrollRect.MovementType.Elastic;
			rect.vertical = true;
			rect.viewport = Viewport.GetComponent<RectTransform>();
			rect.verticalScrollbar = Scrollbar.GetComponent<Scrollbar>();
			PT.anchoredPosition = new(26, 0);
			return Page;
		}
		internal static void AddRotateThing(GameObject obj) 
		{
			GameObject select = new("select");
			RectTransform transform = select.AddComponent<RectTransform>();
			select.AddComponent<CanvasRenderer>();
			Image img = select.AddComponent<Image>();
			img.sprite = Plugin.RotateSprite;
			Rotate rotate = select.AddComponent<Rotate>();
			rotate.speed = new(500, 0, 0);
			select.SetParent(obj, false);
			transform.anchoredPosition = new(1.1334f, 0);
			transform.sizeDelta = new(30, 30);
			transform.localScale = new(0.5f, 0.5f, 0.5f);
			select.SetActive(false);
		}
	}
}