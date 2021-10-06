using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool playerCollide = false;
    public bool show = false;
    public bool invisible = false;
    public float ttl = 5;
    public float InFadeSpeed = 1;
    public float OutFadeSpeed = 1;
    private Renderer render;

    private Color CurrentColor = new Color(1f, 1f, 1f, 0f);
    void Start()
    {
        if (show) {
            CurrentColor.a = 1;
            
        } else {
            
            CurrentColor.a = 0;
        }
        render = GetComponent<Renderer>();
        render.material.SetColor("_Color", CurrentColor);
    }

    void Update() {
        if (playerCollide && ttl > 0) {
            ttl -= Time.deltaTime;
            //Debug.Log(ttl);
        }
        if (ttl < 0) {
            show = false;
        } 

        if (show && CurrentColor.a < 1) {
            appear();
        } else if (!show && CurrentColor.a > 0) {
            disappear();
        } else if (!show) { 
            GetComponent<BoxCollider>().enabled = false;
            invisible = true;
        }

    }
    
    
    public void appear() {
        CurrentColor.a += InFadeSpeed * Time.deltaTime;
        render.material.SetColor("_Color", CurrentColor);
    }
    public void disappear() {
        CurrentColor.a -= OutFadeSpeed * Time.deltaTime;
        render.material.SetColor("_Color", CurrentColor);
 
    }
}
