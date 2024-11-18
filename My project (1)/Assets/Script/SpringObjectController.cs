using UnityEngine;

public class SpringObjectController : MonoBehaviour
{
    public Transform centralObject; // Objeto central (objeto 1).
    public Transform movingTarget; // Objeto de destino em movimento (objeto 2).
    public Transform[] invisibleObjects; // Os 6 objetos invisíveis.
    public float updateInterval = 1f; // Intervalo para recalcular trajetos.
    public float movementSpeed = 5f; // Velocidade de ajuste.
    public float orbitDistance = 5f; // Distância mínima para começar a orbitar.
    public float orbitSpeed = 20f; // Velocidade de órbita.
     private bool hasStartedMoving = false; // Flag para controlar o início do movimento.

     public AudioSource movementAudioSource; // Referência para o AudioSource.
    public ParticleSystem movementParticleSystem; // Referência para o sistema de partículas.

    private bool isOrbiting = false;

    void Start()
    {
        if (invisibleObjects.Length != 6)
        {
            Debug.LogError("Deve haver exatamente 6 objetos invisíveis.");
            //return;
        }

        if (movementAudioSource != null)
            movementAudioSource.Stop();

        if (movementParticleSystem != null)
            movementParticleSystem.Stop();

        //targetPositions = new Vector3[invisibleObjects.Length];
    }

    // Método para iniciar a movimentação dos objetos invisíveis
    public void StartMovingObjects()
    {
        Debug.Log("iniciou");
        // Inicializa as posições-alvo.
        UpdateTargetPositions();

        // Recalcula trajetos em intervalos regulares.
        InvokeRepeating(nameof(UpdateTargetPositions), 0, updateInterval);
    }

    void Update()
    {
       
    }

    void UpdateTargetPositions()
    {
        if (movingTarget == null)
        {
            Debug.LogWarning("O objeto de destino está faltando.");
            return;
        }

         // Move os objetos invisíveis em direção às posições-alvo.
        // Move os objetos invisíveis em direção às posições-alvo até atingirem a distância de órbita
        for (int i = 0; i < invisibleObjects.Length; i++)
        {
            if (invisibleObjects[i] != null)
            {
                float distanceToTarget = Vector3.Distance(invisibleObjects[i].position, movingTarget.position);

                if (distanceToTarget > orbitDistance) 
                {
                    // Se a distância for maior que a distância de órbita, move em direção ao alvo
                    invisibleObjects[i].position = Vector3.MoveTowards(
                        invisibleObjects[i].position,
                        movingTarget.position,
                        movementSpeed * Time.deltaTime
                    );
                }
                else
                {
                    // Caso contrário, começa a orbitar
                    OrbitObject(invisibleObjects[i], movingTarget.position);
                }
            }
        }

        // Ativa o som e a partícula, caso ainda não tenha começado.
        if (!hasStartedMoving)
        {
            hasStartedMoving = true;

            if (movementAudioSource != null)
                movementAudioSource.Play(); // Toca o som.

            if (movementParticleSystem != null)
                movementParticleSystem.Play(); // Ativa as partículas.
        }

        // Distribui as posições-alvo ao redor do objeto de destino.
        //for (int i = 0; i < invisibleObjects.Length; i++)
        //{
        //    float angle = i * Mathf.PI * 2 / invisibleObjects.Length; // Divisão circular.
        //    float radius = 1f; // Ajuste conforme necessário.
        //    Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        //    targetPositions[i] = movingTarget.position + offset;
        //}
    }

    void OrbitObject(Transform objectToOrbit, Vector3 center)
    {
        if (objectToOrbit == null) return;

        // Calcula a direção da órbita
        Vector3 direction = objectToOrbit.position - center;
        Vector3 axis = Vector3.up; // A rotação será ao redor do eixo Y

        // Calcula a rotação ao redor do alvo
        objectToOrbit.RotateAround(center, axis, orbitSpeed * Time.deltaTime);
    }
}
