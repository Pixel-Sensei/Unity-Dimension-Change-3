using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneScript : MonoBehaviour
{
    // Referenz auf das PlayerController-Skript des Spielers
    public PlayerController player;
    // Referenz auf die Kamera im Spiel
    public Camera gameCamera;

    // Start wird einmal zu Beginn des Spiels aufgerufen
    void Start()
    {
        
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Setzt die Kamera-Position, um dem Spieler sanft zu folgen
        gameCamera.transform.position = new Vector3(
            // Gleitet in x-Richtung langsam zur Position des Spielers
            Mathf.Lerp(gameCamera.transform.position.x, player.transform.position.x, 0.01f),
            // Passt die y-Position der Kamera an die y-Position des Spielers an
            player.transform.position.y,
            // Gleitet in z-Richtung langsam zur Position des Spielers, versetzt um -15
            Mathf.Lerp(gameCamera.transform.position.z, player.transform.position.z - 15, 0.01f)
        );
    }
}
