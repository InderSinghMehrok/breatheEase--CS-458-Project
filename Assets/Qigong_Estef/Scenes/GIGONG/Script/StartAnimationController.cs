using UnityEngine;

public class StartAnimationController : MonoBehaviour
{
    public Animator animator;          // Arrastrar el Animator del personaje aquí
    public string triggerName = "StartAnimation"; // Nombre del trigger

    private bool animationStarted = false;

    public void StartAnimation()
    {
        if (animator == null)
        {
            Debug.LogError("Animator no asignado en StartAnimationController");
            return;
        }

        if (!animationStarted)
        {
            animator.SetTrigger(triggerName);
            animationStarted = true;
            Debug.Log("Animación iniciada");
            Debug.Log("Trigger activado: " + triggerName);
        }
    }
}
