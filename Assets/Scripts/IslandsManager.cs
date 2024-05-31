using UnityEngine;

public class CollectGameObjects : MonoBehaviour
{
    public string tagToFind = "tower"; // El tag que estás buscando
    private GameObject[] taggedObjects;

    void Start()
    {
        // Encuentra todos los GameObjects con el tag especificado y almacénalos en el array
        taggedObjects = GameObject.FindGameObjectsWithTag(tagToFind);

        // Imprime los nombres de los GameObjects encontrados para verificar
        foreach (GameObject obj in taggedObjects)
        {
            Debug.Log("Found GameObject: " + obj.name);
        }

        Debug.Log("Finished counter" + taggedObjects.Length);
    }
}
