using System.Collections;
using TMPro;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public string tagToFind = "towerIsland1"; // El tag que estás buscando
    private GameObject[] taggedObjects;

    public GameObject islandCanvas;

    public AudioSource audioSource;
    public GameObject soundPlayer;

    void Start()
    {
        // Encuentra todos los GameObjects con el tag especificado y almacénalos en el array
        taggedObjects = GameObject.FindGameObjectsWithTag(tagToFind);

        // Suscribirse al evento
        Torre.OnTowerStateChanged += CheckAllTowersComplete;

        if (soundPlayer != null)
        {
            audioSource = soundPlayer.GetComponent<AudioSource>();
        }

        if(!gameObject.activeSelf)
        {
            Torre.OnTowerStateChanged -= CheckAllTowersComplete;
        }
    }

    void OnDestroy()
    {
        // Cancelar la suscripción al evento
        Torre.OnTowerStateChanged -= CheckAllTowersComplete;
    }

    void CheckAllTowersComplete()
    {
        if(!gameObject.activeSelf)
        {
            return;
        }

        foreach (GameObject obj in taggedObjects)
        {
            Torre torreComponent = obj.GetComponent<Torre>();

            if (torreComponent != null)
            {
                if (torreComponent.GetEstado() != Torre.Estado.completo)
                {
                    return;
                }
            }
        }

        StartCoroutine(ShowMessage());
        audioSource.Play();
    }

    IEnumerator ShowMessage()
    {
        islandCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        islandCanvas.SetActive(false);
        gameObject.SetActive(false);
    }
}
