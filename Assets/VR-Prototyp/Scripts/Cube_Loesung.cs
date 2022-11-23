using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cube_Loesung : MonoBehaviour
{
    //Variablen
    private Interactable interactable;
    [SerializeField] GameObject wuerfel;
    [SerializeField] GameObject spieler;
    [SerializeField] Material correctResult;
    [SerializeField] Text aufgabe;
    [SerializeField] GameObject playerUI;
    Scene activeScene;
    private int aufgaben = 3;
    public float zahl1;
    public float zahl2;
    public bool addition;
    private string textValue="";


    // Start is called before the first frame update
    void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("playerUI");
        playerUI.SetActive(false);
        if (addition)
        {
            textValue = "Berechne: " + zahl1 + " + " + zahl2;
        }
        else
        {
            textValue = "Berechne: " + zahl1 + " - " + zahl2;
        }  
            
        // Initialisierung Level
        interactable = GetComponent<Interactable>();
        aufgabe.text = textValue;
        //Positionssetup Loesungs-Wuerfel
        wuerfel.transform.position = new Vector3((float)zahl1, 0.6f, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        checkResult();
    }

    private void checkResult()
    {
        //Ergebnis berechnen
        float result;
        if (addition)
        {
            result= zahl1 + zahl2;
        }
        else
        {
            result = zahl1 - zahl2;
        }
        //x -Koordinate des Wuerfels herausfinden:
        float x = wuerfel.transform.position.x;
        float z = wuerfel.transform.position.z;
        //Debug.Log(x);
        
        
        //Check, ob Loesungs-Wuerfel nicht in der Hand ist:
        if(interactable.attachedToHand == null && (x>=(result - 0.125f)&& (x<=result + 0.125f)) && (z>= 0.875f && z<= 1.125f))
        {
            wuerfel.GetComponent<MeshRenderer>().material = correctResult;
            playerUI.SetActive(true);
            Invoke("loadNextScene", 5f);
        }
    }

    void loadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < aufgaben - 1)
        {
            Destroy(wuerfel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
