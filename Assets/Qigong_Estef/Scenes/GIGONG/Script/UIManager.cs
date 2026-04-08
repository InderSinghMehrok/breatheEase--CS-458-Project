using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelIntro;
    public GameObject panelConfig;

    [Header("Sliders")]
    public Slider timeSlider;

    [Header("UI Text")]
    public TMP_Text timeText;
    public TMP_Text timerText;

    [Header("Animator y Animaciones")]
    public Animator animator;
    public string[] animationTriggers;

    [Header("Audio")]
    public AudioSource musicSource;

    [Header("End UI")]
    public GameObject endPanel;
    public TMP_Text endText;

    [Header("Valuees")]
    public int sessionTime;

    private float remainingTime;
    private bool sessionActive = false;

    void Start()
    {
        UpdateTime();
        if (endPanel != null)
            endPanel.SetActive(false);
    }

    void Update()
    {
        //Skips on button press & returns to MainScene
        if (OVRInput.GetDown(OVRInput.Button.Start))
            SceneManager.LoadScene("MainMenuScene");
    }

    // GO TO SETTINGS
    public void GoToConfig()
    {
        panelIntro.SetActive(false);
        panelConfig.SetActive(true);
    }

    // BACK TO INTRO
    public void BackToIntro()
    {
        panelConfig.SetActive(false);
        panelIntro.SetActive(true);
    }

    // UPDATE SLIDER TIME
    public void UpdateTime()
    {
        sessionTime = Mathf.RoundToInt(timeSlider.value);
        timeText.text = sessionTime + " min";
    }

    // START EXPERIENCE
    public void StartExperience()
    {
        sessionTime = Mathf.RoundToInt(timeSlider.value);

        panelIntro.SetActive(false);
        panelConfig.SetActive(false);

        if (endPanel != null)
            endPanel.SetActive(false);

        if (animator != null && animationTriggers.Length > 0)
        {
            remainingTime = sessionTime * 60f;
            sessionActive = true;

            // START MUSIC
            if (musicSource != null)
            {
                musicSource.loop = true;
                musicSource.Play();
            }

            StartCoroutine(PlayExercisesByTime());
            StartCoroutine(UpdateTimer());
        }
        else
        {
            Debug.LogWarning("Animator no asignado o lista de triggers vacía");
        }
    }

    // LOOP ANIMATIONS
    IEnumerator PlayExercisesByTime()
    {
        while (sessionActive)
        {
            foreach (string trigger in animationTriggers)
            {
                if (!sessionActive) yield break;

                // ACTIVATE ANIMATION
                animator.ResetTrigger(trigger);
                animator.SetTrigger(trigger);

                Debug.Log("Trigger activado: " + trigger);

                // WAIT THAT START ANIMATION
                yield return new WaitUntil(() =>
                    animator.GetCurrentAnimatorStateInfo(0).IsName(trigger)
                );

                // WAIT THAT END ANIMATION
                yield return new WaitWhile(() =>
                    animator.GetCurrentAnimatorStateInfo(0).IsName(trigger)
                );
            }
        }

        Debug.Log("Sesión terminada");
    }

    // TIMER CONTROLADO
    IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            yield return null;
        }

        sessionActive = false;
        timerText.text = "";

        if (musicSource != null)
        {
            musicSource.Stop();
        }

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        if (endText != null)
        {
            endText.text = "Session ended";
        }

        Debug.Log("Tiempo finalizado");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenuScene");

    }
}
