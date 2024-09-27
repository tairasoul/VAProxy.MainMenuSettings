using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MainMenuSettings.Extensions;
using System.Collections;

namespace MainMenuSettings 
{
	public struct ToggleOption 
	{
		public string Id;
		public string Text;
		public Action<bool> Toggled;
		public bool defaultState;
	}
	public struct ButtonOption 
	{
		public string Id;
		public string Text;
		public Action Clicked;
	}
	public struct ModOptions
	{
		public ToggleOption[]? toggles;
		public ButtonOption[]? buttons;
		public Action<GameObject>? CreationCallback;
	}
	internal struct Mod 
	{
		public string name;
		public string id;
		public ModOptions options;
	}
	public static class MenuSettings 
	{
		public static void RegisterMod(string name, string id, ModOptions options) 
		{
			Mod mod = new()
			{
				name = name,
				id = id,
				options = options
			};
			MenuSettingsHandler.AddMod(mod);
		}
	}
	
	internal class MenuSettingsHandler
	{
		private static Mod[] Options = [];
		private static bool InMenu = false;
		internal static void AddMod(Mod mod) 
		{
			Options = [.. Options, mod];
			if (InMenu) 
				HandleMod(mod);
		}
		
		static void HandleToggles(ToggleOption[] toggles, SettingsHandler handler, GameObject Content) 
		{
			foreach (ToggleOption toggle in toggles) 
			{
				GameObject tgl = MenuComponents.CreateToggle(toggle.Id, toggle.Text);
				tgl.GetComponent<Toggle>().onValueChanged.AddListener(toggle.Toggled.Invoke);
				tgl.GetComponent<Toggle>().isOn = toggle.defaultState;
				tgl.Find("Background").Find("Checkmark").SetActive(toggle.defaultState);
				tgl.GetComponent<RectTransform>().anchoredPosition = new(-211, 79);
				tgl.SetParent(Content, false);
				MainMenuUtils.AddRotateThing(tgl);
				tgl.Find("select").GetComponent<RectTransform>().anchoredPosition = new(-19, 0);
				handler.selects = [.. handler.selects, tgl.Find("select")];
				Setting setting = new() 
				{
					toggle = tgl.GetComponent<Toggle>()
				};
				handler.settings = [.. handler.settings, setting];
			}
		}
		
		static void HandleButtons(ButtonOption[] buttons, SettingsHandler handler, GameObject Content) 
		{
			foreach (ButtonOption button in buttons) 
			{
				GameObject btn = MenuComponents.CreateButton(button.Id, button.Text);
				btn.GetComponent<Button>().onClick.AddListener(() => button.Clicked.Invoke());
				btn.GetComponent<RectTransform>().anchoredPosition = new(-211, 79);
				btn.SetParent(Content, false);
				MainMenuUtils.AddRotateThing(btn);
				btn.Find("select").GetComponent<RectTransform>().anchoredPosition = new(-19, 0);
				handler.selects = [.. handler.selects, btn.Find("select")];
				Setting setting = new() 
				{
					button = btn.GetComponent<Button>()
				};
				handler.settings = [.. handler.settings, setting];
			}
		}
		
		static void HandleModOptions(ModOptions options, GameObject ModPage) 
		{
			SettingsHandler handler = ModPage.GetComponent<SettingsHandler>();
			GameObject Content = ModPage.Find("Scroller").Find("Viewport").Find("Content");
			if (options.toggles != null)
				HandleToggles(options.toggles, handler, Content);
			if (options.buttons != null)
				HandleButtons(options.buttons, handler, Content);
			handler._Start();
		}
		
		static void HandleMod(Mod mod) 
		{
			GameObject ModPage = MainMenuUtils.CreateModPage(mod.name, mod.id);
			RectTransform mpT = ModPage.GetComponent<RectTransform>();
			mpT.anchoredPosition = new(14.17f, 0.73f);
			mpT.anchorMax = new(1, 1);
			mpT.anchorMin = new(0, 0);
			GameObject Settings = GameObject.Find("Canvas").Find("Settings");
			GameObject Scroller = Settings.Find("Scroller").Find("Viewport").Find("Content");
			GameObject Button = MenuComponents.CreateButton(mod.id, mod.name);
			Button button = Button.GetComponent<Button>();
			SettingsHandler handler = Settings.GetComponent<SettingsHandler>();
			MainMenuUtils.AddRotateThing(Button);
			handler.selects = [.. handler.selects, Button.Find("select")];
			Setting setting = new() 
			{
				button = button
			};
			button.onClick.AddListener(() => 
			{
				ModPage.SetActive(true);
				Settings.SetActive(false);
			});
			handler.settings = [.. handler.settings, setting];
			button.GetComponent<RectTransform>().anchoredPosition = new(-211, 79);
			Button.SetParent(Scroller, false);
			HandleModOptions(mod.options, ModPage);
			handler._Start();
			mod.options.CreationCallback?.Invoke(ModPage);
		}
		
		internal static void MenuLoaded() 
		{
			InMenu = true;
			foreach (Mod mod in Options) 
			{
				HandleMod(mod);
			}
		}
	}
}