using System.Collections;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using MainMenuSettings.Extensions;
using UnityEngine.SceneManagement;

namespace MainMenuSettings 
{
	[BepInPlugin("tairasoul.vaproxy.mainmenusettings", "MainMenuSettings", "1.0.2")]
	class Plugin : BaseUnityPlugin 
	{
		internal static Sprite RotateSprite;
		internal static Sprite Checkmark;
		internal static Sprite CheckmarkBackground;
		internal static bool Ready = false;
		bool init = false;
		void Start() => Init();
		
		void Init() 
		{
			if (init) return;
			StartCoroutine(SetRotateTexture());
			SceneManager.activeSceneChanged += (Scene old, Scene _new) => 
			{
				if (_new.name == "Menu")
					Setup();
			};
		}
		
		IEnumerator SetRotateTexture() 
		{
			while (true) {
				if (GameObject.Find("Canvas") && GameObject.Find("Canvas").Find("Image") && GameObject.Find("Canvas").Find("Image").Find("Text (Legacy) (5)"))
					break;
				yield return null;
			}
			GameObject Optimize = GameObject.Find("Canvas").Find("Optimize");
			GameObject Static = Optimize.Find("StaticAI");
			GameObject Select = Static.Find("select");
			RotateSprite = Select.GetComponent<Image>().sprite;
			GameObject Background = Static.Find("Background");
			CheckmarkBackground = Background.GetComponent<Image>().sprite;
			Checkmark = Background.Find("Checkmark").GetComponent<Image>().sprite;
		}
		
		void Setup() 
		{
			MainMenuUtils.CreateText();
			MainMenuUtils.CreateSettingsPage();
			MainMenuUtils.SetupImageCompListener();
			MainMenuUtils.SetupListener();
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