using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Vector3 movementDirection;  // Direção do movimento do asteroide.
    private float movementSpeed;  // Velocidade do movimento.

    // Configura movimento aleatório do asteroide.
    public void SetRandomMovement(float speedMin, float speedMax)
    {
        // Define uma direção aleatória para o movimento.
        movementDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        // Define uma velocidade aleatória.
        movementSpeed = Random.Range(speedMin, speedMax);
    }

    void Update()
    {
        // Movimenta o asteroide com base na direção e velocidade.
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
}
