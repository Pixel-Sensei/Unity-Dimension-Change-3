using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Sprungkraft des Spielers
    public float jumpForce;
    // Bewegungskraft des Spielers
    public float moveForce;
    // Überprüfung, ob der Spieler springen kann
    private bool canJump = false;
    // Überprüfung, ob der Spieler die Ebene gewechselt hat
    private bool hasSwitchedLayers = false;

    // GUI-Objekt für die Anzeige der Münzen
    public GameObject GUI;
    // Zähler für die gesammelten Münzen
    public int CointCount = 0;
    // Spielstatus (ob das Spiel läuft oder nicht)
    private bool game = true;

    // Text-Element zur Anzeige des Punktestands
    public Text scoreText;

    // Start wird einmal am Anfang des Spiels aufgerufen
    void Start()
    {

    }

    // FixedUpdate wird in festen Zeitintervallen aufgerufen und wird für die Physik genutzt
    void FixedUpdate()
    {
        // Bewegung nach links, wenn die "a"-Taste gedrückt wird
        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                -moveForce * Time.deltaTime,  // Geschwindigkeit in x-Richtung
                GetComponent<Rigidbody>().velocity.y,  // beibehaltende y-Geschwindigkeit
                GetComponent<Rigidbody>().velocity.z); // beibehaltende z-Geschwindigkeit
        }

        // Bewegung nach rechts, wenn die "d"-Taste gedrückt wird
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                moveForce * Time.deltaTime,  // Geschwindigkeit in x-Richtung
                GetComponent<Rigidbody>().velocity.y,  // beibehaltende y-Geschwindigkeit
                GetComponent<Rigidbody>().velocity.z); // beibehaltende z-Geschwindigkeit
        }
    }

    // Update wird einmal pro Frame aufgerufen
    private void Update()
    {
        // Sprungaktion bei "w"-Taste, wenn Sprung erlaubt ist
        if (Input.GetKeyDown("w") && canJump)
        {
            canJump = false;  // Springen deaktivieren, bis der Spieler landet
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }

        // Ebenenwechsel bei Drücken der Leertaste ("space")
        if (Input.GetKey("space"))
        {
            if (hasSwitchedLayers)
            {
                // Spieler auf Ebene 0 setzen
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    0
                );
            }
            else
            {
                // Spieler auf Ebene 10 setzen
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    10
                );
            }
            // Ebenenstatus aktualisieren
            hasSwitchedLayers = !hasSwitchedLayers;
        }

        // Überprüfen, ob der Spieler gefallen ist (unter y = -10)
        if (transform.position.y < -10)
        {
            this.transform.position = new Vector3(-12, 1.5f, 0);  // Position zurücksetzen
            hasSwitchedLayers = false;
        }

        // Münzzähler anzeigen, wenn das Spiel aktiv ist
        if (game)
        {
            scoreText.text = "Coin: " + CointCount.ToString();
        }
    }

    // Kollisionserkennung, wenn der Spieler mit einem Objekt kollidiert
    private void OnCollisionEnter(Collision collision)
    {
        // Ermöglicht das Springen nach einer Landung
        canJump = true;

        // Wenn der Spieler das Objekt "Stage1Finish" berührt
        if (collision.gameObject.name == "Stage1Finish")
        {
            this.transform.position = new Vector3(37, 4, 0);  // Position zurücksetzen
            hasSwitchedLayers = false;
        }

        // Wenn der Spieler das Objekt "Stage2Finish" berührt und eine Münze besitzt
        if (collision.gameObject.name == "Stage2Finish" && CointCount == 1)
        {
            Destroy(collision.gameObject);  // Entfernt das Objekt "Stage2Finish"
            hasSwitchedLayers = false;
            game = false;  // Spiel beenden
            // Erfolgsnachricht anzeigen
            scoreText.text = "Well done!";
            scoreText.color = Color.blue;
            scoreText.fontSize = 100;
            scoreText.alignment = TextAnchor.MiddleCenter;
        }

        // Wenn der Spieler ein Objekt mit dem Tag "Obstacle" berührt
        if (collision.gameObject.tag == "Obstacle")
        {
            this.transform.position = new Vector3(37, 4, 0);  // Position zurücksetzen
            hasSwitchedLayers = false;
        }

        // Wenn der Spieler ein Objekt mit dem Tag "Trampoline" berührt
        if (collision.gameObject.tag == "Trampoline")
        {
            GetComponent<Rigidbody>().AddForce(0, 750, 0);  // Spieler nach oben katapultieren
        }

        // Wenn der Spieler ein Objekt mit dem Tag "Coin" berührt
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);  // Münze zerstören
            CointCount += 1;  // Münzzähler erhöhen
        }
    }
}
