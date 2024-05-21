using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        public TextMeshProUGUI woodText;
        public int currentWood = 200;

        public TextMeshProUGUI goldText;
        public int currentGold = 200;

        public Slider slider;
        public int maxHunger = 100;
        public int currentHunger = 100;

        public PlayerController player;

        void Update()
        {
            woodText.text = $"{currentWood}";
            goldText.text = $"{currentGold}";

            if (Time.time % 4 < 0.01f)
            {
                currentHunger -= 1;
                slider.value = currentHunger / (float) maxHunger;
            }

            if(currentHunger == maxHunger && Time.time % 2 < 0.01f)
            {
                player.AddHealth(10);
            }
        }

        public void AddMeat()
        {
            currentHunger += 10;
        }


        public void AddWood(int woodToAdd)
        {
            currentWood += woodToAdd;
            if (woodText != null)
            {
                woodText.text = $"{currentWood}";
            }
        }

        public void RemoveWood(int woodToRemove)
        {
            currentWood -= woodToRemove;
            if (woodText != null)
            {
                woodText.text = currentWood + "";
            }
        }

        public void AddGold(int goldToAdd)
        {
            currentGold += goldToAdd;

            if (goldText != null)
            {
                goldText.text = currentGold + "";
            }
        }

        public void RemoveGold(int goldToRemove)
        {
            currentGold -= goldToRemove;
            if (goldText != null)
            {
                goldText.text = currentGold + "";
            }
        }

        public bool ComprarMejora(int precio)
        {
            if (precio <= currentWood)
            {
                currentWood -= precio;
                return true;
            }
            return false;
        }
    }
}