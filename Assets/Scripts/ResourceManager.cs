using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI woodText;
    public int currentWood = 200;

    public TextMeshProUGUI goldText;
    public int currentGold = 200;

    void Update()
    {
        woodText.text = $"{currentWood}";
        goldText.text = $"{currentGold}";
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
