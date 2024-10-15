using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_skybox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.1f); // ajusta a velocidade
    }

}
