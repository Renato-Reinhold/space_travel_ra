using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MotionBlurControl : MonoBehaviour
{
    public PostProcessVolume volume;  // Arraste o PostProcessingVolume no Inspector
    private MotionBlur motionBlur;

    void Start()
    {
        volume.profile.TryGetSettings(out motionBlur);  // Obtém a referência do Motion Blur
    }

    public void ActivateMotionBlur()
    {
        motionBlur.enabled.value = true;  // Ativa o Motion Blur
    }

    public void DeactivateMotionBlur()
    {
        motionBlur.enabled.value = false;  // Desativa o Motion Blur
    }
}
