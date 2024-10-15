using UnityEngine;

public class OrbitAroundSun : MonoBehaviour
{
    public Transform sun;         // Referência ao Sol
    public float orbitSpeed = 10f;  // Velocidade de translação (orbitar)

    void Update()
    {
        // Faz o planeta orbitar em torno do Sol
        transform.RotateAround(sun.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
