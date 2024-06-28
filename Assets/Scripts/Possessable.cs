using System;
using UnityEngine;

public class Possessable : MonoBehaviour{
    public SpriteRenderer sr;
    public bool pulse = false;
    public float pulseSpeed = 1.0f;

    float colorValue = 0.8f;
    float angle = 0.0f;

    public void Pulse() {
        pulse = true;
    }

    public void UnPulse() {
        pulse = false;
        sr.color = Color.white;
    }

    private void Update() { 
        if(pulse) {
            angle += Time.deltaTime * pulseSpeed;
            if(angle > 360.0f) { angle = 0.0f;}

            colorValue = Mathf.Lerp(0.3f, 0.8f, (Mathf.Cos(angle) + 1) / 2 );
            sr.color = new Color(colorValue, colorValue, colorValue);
        }
    }    
}
