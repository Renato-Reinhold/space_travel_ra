using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    public Vector3 rotationSpeed;  // Define a velocidade de rotação para cada eixo (x, y, z)

    void Update()
    {
        // Rotaciona o objeto em seus próprios eixos (auto-rotação)
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
