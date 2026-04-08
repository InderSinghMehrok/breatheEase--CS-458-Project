using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MeditationSessionController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource windSource;
    public AudioSource riverSource;
    public AudioSource voiceoverSource;

    [Header("Audio Clips")]
    public AudioClip musicClip;
    public AudioClip windClip;
    public AudioClip riverClip;
    public AudioClip voiceoverClip;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI hintText;

    [Header("Settings")]
    public float sessionDuration = 180f;
    public float fadeInDuration = 3f;

    float _timeRemaining;
    bool _running;
    bool _ending;

    void Start()
    {
        _timeRemaining = sessionDuration;
        _running = true;
        _ending = false;

        SetupAudio();
        FadeInAudio();

        if (hintText != null)
        {
            hintText.text = "Press any button to end session";
            hintText.DOFade(0f, 2f).SetDelay(4f);
        }
    }

    void SetupAudio()
    {
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.volume = 0f;
            musicSource.Play();
        }

        if (windClip != null)
        {
            windSource.clip = windClip;
            windSource.loop = true;
            windSource.volume = 0f;
            windSource.Play();
        }

        if (riverClip != null)
        {
            riverSource.clip = riverClip;
            riverSource.loop = true;
            riverSource.volume = 0f;
            riverSource.Play();
        }

        if (voiceoverClip != null)
        {
            voiceoverSource.clip = voiceoverClip;
            voiceoverSource.loop = false;
            voiceoverSource.volume = 0.9f;
            voiceoverSource.PlayDelayed(4f);
        }
    }

    void FadeInAudio()
    {
        if (musicSource != null) musicSource.DOFade(0.35f, fadeInDuration);
        if (windSource != null) windSource.DOFade(0.4f, fadeInDuration);
        if (riverSource != null) riverSource.DOFade(0.6f, fadeInDuration + 1f);
    }

    void Update()
    {
        if (!_running || _ending) return;

        // Any controller button ends session
        if (OVRInput.GetDown(OVRInput.Button.Any))
            EndSession();

        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Max(0f, _timeRemaining);

        UpdateTimerUI();

        if (_timeRemaining <= 0f)
            EndSession();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
        timerText.text = $"{minutes}:{seconds:00}";
    }

    public void EndSession()
    {
        if (_ending) return;
        _ending = true;
        _running = false;

        if (musicSource != null) musicSource.DOFade(0f, 2f);
        if (windSource != null) windSource.DOFade(0f, 2f);
        if (riverSource != null) riverSource.DOFade(0f, 2f);
        if (voiceoverClip != null)
        {
            voiceoverSource.clip = voiceoverClip;
            voiceoverSource.loop = false;
            voiceoverSource.volume = 0.5f;
            voiceoverSource.PlayDelayed(15f); // 15 seconds after scene loads
        }

        DOVirtual.DelayedCall(2.5f, () =>
            SceneFader.Instance.FadeToScene("MainMenuScene"));
    }
}