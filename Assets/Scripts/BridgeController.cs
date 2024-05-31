using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public string tagToFind = "towerIsland1"; // El tag que estás buscando
    private GameObject[] taggedObjects;

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
    }

    void OnDestroy()
    {
        // Cancelar la suscripción al evento
        Torre.OnTowerStateChanged -= CheckAllTowersComplete;
    }

    void CheckAllTowersComplete()
    {
        foreach (GameObject obj in taggedObjects)
        {
            Torre torreComponent = obj.GetComponent<Torre>();

            if (torreComponent != null)
            {
                if (torreComponent.GetEstado() != Torre.Estado.completo)
                {
                    Debug.Log("Aun falta");
                    return;
                }
            }
        }

        // Si todos están completos, desactiva el objeto
        audioSource.Play();
        gameObject.SetActive(false);
    }
}
