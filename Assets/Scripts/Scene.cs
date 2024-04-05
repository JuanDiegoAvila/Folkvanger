using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour
{
    public void CambiarALaEscena(string escena)
    {

        // SceneManager.LoadSceneAsync(escena);
        StartCoroutine(EsperarYCambiarEscena(escena));
    }

    IEnumerator EsperarYCambiarEscena(string escena)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(escena);
    }
}
