using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    // Maximale Distanz, um die sich die Plattform bewegt
    public float delta = 2f;
    // Geschwindigkeit der Bewegung
    public float speed = 2f;
    // Vektor zur Festlegung der Bewegungsrichtung und -geschwindigkeit
    Vector3 v;

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Berechnet die Bewegung entlang der x-Achse basierend auf einer Sinuswelle,
        // die Zeit und Geschwindigkeit einbezieht
        v.x = delta * Mathf.Sin(Time.time * speed);
        // Setzt die Geschwindigkeit der Plattform
        GetComponent<Rigidbody>().velocity = v;
    }
}
