using UnityEngine;

public class DynamicOrbitController : MonoBehaviour
{
    [Header("Referências")]

    public Transform ship;
    public Transform targetPlanet; // O planeta que a nave vai orbitar.
    public float moveSpeed = 10f; // Velocidade de movimento até o planeta.
    public float orbitDistance = 5f; // Distância mínima para começar a orbitar.
    public float orbitSpeed = 50f; // Velocidade de rotação ao orbitar.

    private bool isMoving = false; // Indica se a nave está se movendo em direção ao planeta.
    private bool isOrbiting = false; // Indica se a nave está orbitando o planeta.

    void Update()
    {
        if (isMoving && !isOrbiting)
        {
            MoveToPlanet();
        }

        if (isOrbiting)
        {
            OrbitAroundPlanet();
        }
    }

    // Chamar este método quando clicar no botão.
    public void MoveAndOrbit()
    {
        if (targetPlanet != null)
        {
            isMoving = true;
            isOrbiting = false;
        }
        else
        {
            Debug.LogWarning("Nenhum planeta definido como alvo!");
        }
    }

    // Move a nave dinamicamente até o planeta, recalculando sua posição.
    private void MoveToPlanet()
    {
        // Direção atualizada para o planeta em movimento.
        Vector3 direction = (targetPlanet.position - ship.position).normalized;
        ship.position += direction * moveSpeed * Time.deltaTime;

        // Distância atual até o planeta.
        float distanceToPlanet = Vector3.Distance(ship.position, targetPlanet.position);

        // Verifica se está na distância de órbita.
        if (distanceToPlanet <= orbitDistance)
        {
            isMoving = false;
            isOrbiting = true;
        }
    }

    // Faz a nave orbitar o planeta enquanto ele está em movimento.
    private void OrbitAroundPlanet()
    {
        // Recalcula a órbita considerando o movimento contínuo do planeta.
        transform.RotateAround(targetPlanet.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
