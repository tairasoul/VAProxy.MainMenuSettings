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

		private int currentSlot;

		public void _Start()
		{
			selects[0].SetActive(value: true);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
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
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
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
			if (Input.GetKeyDown(KeyCode.E))
			{
				Setting current = settings[currentSlot];
				if (current.toggle != null)
					current.toggle.isOn = !current.toggle.isOn;//.onValueChanged.Invoke(!current.toggle.isOn);
				else
					current.button?.onClick.Invoke();
			}
		}
	}
}