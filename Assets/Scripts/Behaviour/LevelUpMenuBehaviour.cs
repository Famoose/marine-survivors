using System;
using Feature;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    [RequireComponent(typeof(GameStateFeature))]
    public class LevelUpMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private GameStateFeature gameStateFeature;
        private GameObject _chooseOption1Button;
        private GameObject _chooseOption2Button;
        private GameObject _chooseOption3Button;
        private GameObject _chooseOption4Button;
        
        public void Start()
        {
            _chooseOption1Button = transform.Find("LevelUpMenu/ChooseOption1Button").gameObject;
            _chooseOption2Button = transform.Find("LevelUpMenu/ChooseOption2Button").gameObject;
            _chooseOption3Button = transform.Find("LevelUpMenu/ChooseOption3Button").gameObject;
            _chooseOption4Button = transform.Find("LevelUpMenu/ChooseOption4Button").gameObject;

            _chooseOption1Button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(0));
            _chooseOption2Button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(1));
            _chooseOption3Button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(2));
            _chooseOption4Button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(3));
        }

        private void ChooseOption(int optionIndex)
        {
            Debug.Log(optionIndex);
            
            // Hide Menu and unpause game
            gameObject.SetActive(false);
            gameStateFeature.UnpauseGame();
        }
    }
}