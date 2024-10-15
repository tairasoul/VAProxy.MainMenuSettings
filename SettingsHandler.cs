using Rewired;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuSettings 
{
	struct Setting 
	{
		public Toggle? toggle;
		public Button? button;
	}
	class SettingsHandler : MonoBehaviour
	{
		public Setting[] settings = [];

		public GameObject[] selects = [];
		internal Player player;

		private int currentSlot;

		public void _Start()
		{
			selects[0].SetActive(value: true);
		}
		
		private void FixedUpdate() 
		{
			player = ReInput.players.GetPlayer("Player0");
		}

		private void Update()
		{
			if (player.GetNegativeButtonDown("MovementY") || player.GetButtonDown("D_Down"))
			{
				GameObject[] array = selects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				currentSlot++;
				if (currentSlot > settings.Length - 1)
				{
					currentSlot = 0;
				}
				selects[currentSlot].SetActive(value: true);
			}
			if (player.GetButtonDown("MovementY") || player.GetButtonDown("D_Up"))
			{
				GameObject[] array = selects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				currentSlot--;
				if (currentSlot < 0)
				{
					currentSlot = settings.Length - 1;
				}
				selects[currentSlot].SetActive(value: true);
			}
			MenuComponents.ScrollToElement(selects[currentSlot]);
			if (player.GetButtonDown("Action"))
			{
				Setting current = settings[currentSlot];
				if (current.toggle != null)
					current.toggle.isOn = !current.toggle.isOn;
				else
					current.button?.onClick.Invoke();
			}
		}
	}
}