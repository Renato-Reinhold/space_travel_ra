using UnityEngine;
using UnityEngine.UI;


public class SpaceTravel : MonoBehaviour
{

    public Button planetButton;   // Referência ao botão UI
    public Transform planet;      // Referência ao planeta no espaço
    public Transform ship;           // Referência à nave
    public float travelSpeed = 500f;  // Velocidade da viagem (ajuste para simular alta velocidade)
    public float stopDistance = 10f; // Distância mínima do planeta para parar
    public float rotationSpeed = 5f;  // Velocidade de rotação da nave
    private Transform targetPlanet;   // Planeta para o qual viajar
    public float orbitSpeed = 20f;   // Velocidade da órbita
    private bool isTravelling = false;
    

    void Update()
    {
        if (targetPlanet != null)
        {
            if (isTravelling){
                // Move a nave em direção ao planeta
                ship.position = Vector3.MoveTowards(ship.position, targetPlanet.position, travelSpeed * Time.deltaTime);

                // Calcula a direção do planeta
                Vector3 direction = targetPlanet.position - transform.position;

                // Calcula a rotação necessária para olhar na direção do planeta
                Quaternion toRotation = Quaternion.LookRotation(direction);

                // Faz a rotação suave da nave para o planeta
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                // Verifica se a nave chegou perto do planeta e para a viagem
                if (Vector3.Distance(ship.position, targetPlanet.position) < stopDistance)
                {
                    isTravelling = false;
                }
            }

            if (Vector3.Distance(ship.position, targetPlanet.position) < stopDistance){
                // Faz a nave orbitar ao redor do planeta
                transform.RotateAround(planet.position, Vector3.up, orbitSpeed * Time.deltaTime);
            }
        }
    }

    public void TravelToPlanet()
    {
        targetPlanet = planet;
        isTravelling = true;
    }

}
