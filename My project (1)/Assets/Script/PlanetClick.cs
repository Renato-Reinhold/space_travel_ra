using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.PostProcessing;

public class PlanetClick : MonoBehaviour
{
    public Camera mainCamera;
    public PlayableDirector timelineDirector;
    public PostProcessVolume volume;
    private MotionBlur motionBlur;
    public float maxFOV = 90f;
    public float minFOV = 60f;

    void Start()
    {
        volume.profile.TryGetSettings(out motionBlur);  // Obtém a referência ao Motion Blur
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Planet"))
            {
                timelineDirector.Play();  // Inicia a animação da Timeline
                ActivateMotionBlur();  // Ativa o Motion Blur
            }
        }
    }

    void ActivateMotionBlur()
    {
        motionBlur.enabled.value = true;
    }

    void DeactivateMotionBlur()
    {
        motionBlur.enabled.value = false;
    }

}
