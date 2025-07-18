using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable ]
    // Box to hold name text on screen
    public class NameContainer
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI nameText;

        public void Show(string nameToShow = "")
        {
            root.SetActive(true);

            if (nameToShow != string.Empty)
                nameText.text = nameToShow;
        }
        public void Hide()
        {
            root.SetActive(false);
        }
    }
}
