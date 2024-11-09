using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    // Höhe, um die sich die Münze auf- und abbewegt
    public float delta = 0.5f;
    // Geschwindigkeit der Drehung und Auf- und Abbewegung
    public float speed = 3.0f;
    // Startposition der Münze
    Vector3 startPos;

    // Start wird einmal am Anfang des Spiels aufgerufen
    void Start()
    {
        // Speichert die Anfangsposition der Münze
        startPos = transform.position;
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Rotiert die Münze um die y-Achse
        transform.Rotate(0, speed, 0);
        
        // Bewegt die Münze auf und ab, basierend auf einer Sinuswelle
        Vector3 v = startPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
