
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Configurações de Asteroides")]
    public GameObject asteroidPrefab;  // Prefab do asteroide a ser instanciado.
    public int numberOfAsteroids = 10;  // Número de asteroides a serem gerados.
    public float spawnRadius = 100f;  // Raio dentro do qual os asteroides serão gerados.
    public float speedMin = 5f;  // Velocidade mínima dos asteroides.
    public float speedMax = 20f;  // Velocidade máxima dos asteroides.

    [Header("Configurações de Movimento")]
    public Vector3 movementDirection = new Vector3(0, 0, 1);  // Direção inicial do movimento.

    void Start()
    {
        SpawnAsteroids();
    }

    // Função para gerar asteroides
    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // Gera uma posição aleatória dentro do raio de spawn.
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(-spawnRadius, spawnRadius)
            );

            // Cria o asteroide no espaço.
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            // Adiciona movimento aleatório para o asteroide.
            AsteroidMovement movement = asteroid.AddComponent<AsteroidMovement>();
            movement.SetRandomMovement(speedMin, speedMax);
        }
    }
}
