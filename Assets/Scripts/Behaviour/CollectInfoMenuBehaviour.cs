using Feature;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    [RequireComponent(typeof(GameStateFeature))]
    public class CollectInfoMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private GameStateFeature gameStateFeature;
        private GameObject _button;
        private GameObject _name;
        private GameObject _description;

        public void PrepareMenu(string leveledUpAbilityOrWeaponName, int oldLevel)
        {
            _name.GetComponent<TextMeshProUGUI>().text = leveledUpAbilityOrWeaponName;
            _description.GetComponent<TextMeshProUGUI>().text = $"It's now at level {oldLevel + 2}!";
        }

        public void Start()
        {
            _button = transform.Find($"CollectInfoMenu/OkButton").gameObject;
            _name = transform.Find($"CollectInfoMenu/NameText").gameObject;
            _description = transform.Find($"CollectInfoMenu/DescText").gameObject;

            _button.GetComponent<Button>().onClick.AddListener(HideMenu);
        }

        private void HideMenu()
        {
            // Hide Menu and unpause game
            gameObject.SetActive(false);
            gameStateFeature.UnpauseGame();
        }
    }
}