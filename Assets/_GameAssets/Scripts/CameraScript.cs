using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {
    public float speed;
    public Text txtTimeToDestroy;
    public Image shootImage;
    public GameObject prefabExplosion;
    public AudioClip explosionClip;
    private Camera mainCamera;
    private AudioSource audioSource;
    private string currentTargetName;
    private float timeToDestroy=0;
    private const int TIME_TO_DESTROY = 200;
    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update () {
        //Avanza el soporte de la cámara hacia su forward
        transform.Translate(mainCamera.transform.forward * Time.deltaTime * speed);
        //Obtiene el GameObject al que está mirando la cámara
        GameObject go = GetSelectedGO();
        //Procesa el objeto que ha seleccionado la cámara
        ProcesarGO(go);
        //Si se pulsa el botón físico se reinicia la escena
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ProcesarGO(GameObject go)
    {
        //Comprueba si está mirando hacia algún GameObject
        if (go!=null)
        {
            /*
             * Comprueba si el GameObject al que está mirando es el mismo al que estaba mirando 
             * en el frame anterior
             */
            if (currentTargetName == go.name)
            {
                //Si es el mismo GameObject incrementa en 1 el contador de tiempo
                timeToDestroy++;
                //Muestra por pantalla el tiempo que falta para la destrucción
                txtTimeToDestroy.text = timeToDestroy.ToString();
                //Si el tiempo acumulado excede del tiempo necesario para la destrucción, destruye
                if (timeToDestroy > TIME_TO_DESTROY)
                {
                    audioSource.PlayOneShot(explosionClip);
                    Instantiate(prefabExplosion,
                        new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z),
                        Quaternion.identity);
                    Destroy(go);
                    ResetTimeToDestroy();
                }
            }
            else
            {
                //Si ha mirado a un objeto distinto del anterior, reinicia el contador de tiempo
                ResetTimeToDestroy();
                currentTargetName = go.name;
            }
            
        } else
        {
            //Si no mira a nada, reinicia el contador de tiempo
            ResetTimeToDestroy();
        }
    }

    private void ResetTimeToDestroy()
    {
        timeToDestroy = 0;
        txtTimeToDestroy.text = "Searching...";
    }

    /*
     * Devuelve el objeto al que está mirando la cámara
     */
    private GameObject GetSelectedGO()
    {
        GameObject selectedGO = null;
        Ray raycast = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            selectedGO= raycastHit.transform.gameObject;
        }
        return selectedGO;
    }

}
