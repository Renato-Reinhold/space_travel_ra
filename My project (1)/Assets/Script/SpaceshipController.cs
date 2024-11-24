using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movimento e Física")]
    public float maxThrust = 50f;  // Velocidade máxima da nave.
    public float rotationSpeed = 10f;  // Velocidade de rotação da nave.
    public float accelerationRate = 5f;  // Taxa de aceleração.
    public float decelerationRate = 1f;  // Taxa de desaceleração.

    [Header("Gravidade Personalizada")]
    public Transform planet;  // Planeta que atrai a nave.
    public float gravityStrength = 9.8f;

    [Header("Piloto Automático")]
    public Transform targetPlanet;  // O planeta para o qual a nave deve ir.
    public float stopDistance = 10f;  // Distância mínima para entrar em órbita.
    public float targetApproachSpeed = 20f;  // Velocidade de aproximação.
    public float orbitalSpeed = 10f;  // Velocidade de órbita.
    public float randomOrbitDeviation = 0.2f;  // Desvio aleatório da órbita.

    private Rigidbody rb;
    private bool isAutoPilotEnabled = false;  // Se o piloto automático está ativo.
    private bool isOrbiting = false;  // Se a nave está orbitando.
    private Vector3 targetPosition;  // Posição do alvo durante a viagem.
    private float currentThrust = 0f;  // Força de aceleração atual.
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Desativa a gravidade padrão.
        rb.drag = 0;  // Sem resistência linear.
        rb.angularDrag = 0;  // Sem resistência angular.
    }

    void Update()
    {
        if (isAutoPilotEnabled && !isOrbiting)
        {
            MoveToTargetPlanet();
        }
        else if (isOrbiting)
        {
            OrbitPlanet();
        }
    }

    // Habilitar o piloto automático para um planeta específico.
    public void EnableAutoPilot(Transform newTargetPlanet)
    {
        if (newTargetPlanet != null)
        {
            targetPlanet = newTargetPlanet;  // Define o planeta alvo.
            targetPosition = targetPlanet.position;  // Define a posição de destino inicial.
            isAutoPilotEnabled = true;
            isOrbiting = false;
        }
    }

    // Movimento até o planeta alvo.
    private void MoveToTargetPlanet()
    {
        if (targetPlanet == null) return;

        Vector3 direction = (targetPlanet.position - transform.position).normalized;

        // Alinha a nave para olhar para o planeta enquanto se move em direção a ele.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToPlanet = Vector3.Distance(transform.position, targetPlanet.position);

        // A nave vai tomando um caminho mais suave e aleatório (gerando desvios).
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation),
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation),
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation)
        );
        targetPosition = targetPlanet.position + randomOffset;

        // Desaceleração suave à medida que a nave se aproxima do planeta.
        float speed = Mathf.Lerp(targetApproachSpeed, orbitalSpeed, 1 - (distanceToPlanet / stopDistance));
        rb.velocity = direction * Mathf.Clamp(speed, orbitalSpeed, targetApproachSpeed);

        // Entra em órbita ao alcançar a distância mínima.
        if (distanceToPlanet <= stopDistance)
        {
            isAutoPilotEnabled = false;
            isOrbiting = true;
        }
    }

    // Simula a órbita ao redor do planeta, com comportamento aleatório.
    private void OrbitPlanet()
    {
        if (targetPlanet == null) return;

        // Gera um vetor perpendicular para a órbita, criando um efeito de movimento aleatório.
        Vector3 directionToPlanet = (transform.position - targetPlanet.position).normalized;
        Vector3 orbitalDirection = Vector3.Cross(directionToPlanet, Vector3.up).normalized;

        // Introduz uma variação para tornar a órbita mais imprevisível.
        orbitalDirection += new Vector3(
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation),
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation),
            Random.Range(-randomOrbitDeviation, randomOrbitDeviation)
        );

        // Mantém a nave em movimento orbital constante.
        rb.velocity = orbitalDirection * orbitalSpeed;

        // A nave sempre olha para o planeta durante a órbita.
        //Quaternion targetRotation = Quaternion.LookRotation(-directionToPlanet);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Aplica uma gravidade personalizada se o planeta estiver definido.
    private void ApplyCustomGravity()
    {
        if (planet != null)
        {
            Vector3 directionToPlanet = (planet.position - transform.position).normalized;
            float distance = Vector3.Distance(planet.position, transform.position);

            Vector3 gravityForce = directionToPlanet * (gravityStrength / Mathf.Pow(distance, 2));  // Gravidade inversamente proporcional ao quadrado da distância.
            rb.AddForce(gravityForce);
        }
    }

    // Atualiza os efeitos visuais e sonoros conforme a velocidade da nave.
    private void UpdateEffects()
    {
        // Aqui você pode adicionar lógica para ativar partículas e sons com base na velocidade ou ações.
    }
}
