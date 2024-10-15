using System.Collections;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using MainMenuSettings.Extensions;
using UnityEngine.SceneManagement;

namespace MainMenuSettings 
{
	[BepInPlugin("tairasoul.vaproxy.mainmenusettings", "MainMenuSettings", "2.0.0")]
	class Plugin : BaseUnityPlugin 
	{
		internal static Sprite RotateSprite;
		internal static Sprite Checkmark;
		internal static Sprite CheckmarkBackground;
		internal static Sprite ButtonImage;
		internal static bool Ready = false;
		bool init = false;
		void Start() => Init();
		
		void Init() 
		{
			if (init) return;
			SceneManager.activeSceneChanged += (Scene old, Scene _new) => 
			{
				if (_new.name == "Menu")
					StartCoroutine(SetRotateTexture());
			};
		}
		
		IEnumerator SetRotateTexture() 
		{
			while (true) {
				if (GameObject.Find("Canvas") && GameObject.Find("Canvas").Find("StartMenu") && GameObject.Find("Canvas").Find("StartMenu").Find("Start"))
					break;
				yield return null;
			}
			GameObject Optimize = GameObject.Find("Canvas").Find("Settings");
			GameObject Start = GameObject.Find("Canvas").Find("StartMenu").Find("Start");
			GameObject Static = Optimize.Find("StaticAI");
			GameObject Select = Start.Find("select");
			RotateSprite = Select.GetComponent<Image>().sprite;
			ButtonImage = Start.GetComponent<Image>().sprite;
			GameObject Background = Static.Find("Background");
			CheckmarkBackground = Background.GetComponent<Image>().sprite;
			Checkmark = Background.Find("Checkmark").GetComponent<Image>().sprite;
			Setup();
		}
		
		void Setup() 
		{
			MainMenuUtils.CreateText();
			MainMenuUtils.CreateSettingsPage();
			Ready = true;
			StartCoroutine(MenuLoaded());
		}
		
		IEnumerator MenuLoaded() 
		{
			while (true) 
			{
				if (Checkmark != null)
					break;
				yield return null;
			}
			MenuSettingsHandler.MenuLoaded();
		}
	}
}