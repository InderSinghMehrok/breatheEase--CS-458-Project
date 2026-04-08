using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Attach this to the same GameObject as RoutineManager.
/// It will validate all references and trace every step of EnterState at runtime.
/// Press R in Play Mode to re-run ControllersDown from scratch.
/// </summary>
public class RoutineManagerDebugger : MonoBehaviour
{
    private RoutineManager rm;

    void Awake()
    {
        rm = GetComponent<RoutineManager>();
        if (rm == null)
        {
            Debug.LogError("[Debugger] RoutineManagerDebugger must be on the same GameObject as RoutineManager!");
            return;
        }

        Debug.Log("[Debugger] === REFERENCE VALIDATION ===");

        // Dependencies
        LogRef("nodDetector",          rm.nodDetector);
        LogRef("poseDetector",         rm.poseDetector);
        LogRef("passthrough",          rm.passthrough);

        if (rm.passthrough != null)
        {
            // Check the inner OVRPassthroughLayer via reflection-free approach
            var passthroughLayer = rm.passthrough.passthroughLayer;
            LogRef("passthrough.passthroughLayer", passthroughLayer);
        }

        // Scene
        LogRef("backgroundSphere",     rm.backgroundSphere);

        // UI
        LogRef("controllersDownPrompt",rm.controllersDownPrompt);
        LogRef("readyPrompt",          rm.readyPrompt);
        LogRef("finishPrompt",         rm.finishPrompt);
        LogRef("grabControllerPrompt", rm.grabControllerPrompt);
        LogRef("uiFeedback",           rm.uiFeedback);
        LogRef("promptBackground",     rm.promptBackground);

        // Audio clips
        LogRef("welcomeAudio",         rm.welcomeAudio);
        LogRef("readyToBeginAudio",    rm.readyToBeginAudio);
        LogRef("pose1Audio",           rm.pose1Audio);
        LogRef("pose2Audio",           rm.pose2Audio);
        LogRef("finishAudio",          rm.finishAudio);
        LogRef("nodToCloseAudio",      rm.nodToCloseAudio);

        // Poses array
        if (rm.poses == null || rm.poses.Length == 0)
            Debug.LogError("[Debugger] poses array is NULL or EMPTY!");
        else
        {
            for (int i = 0; i < rm.poses.Length; i++)
                LogRef($"poses[{i}]", rm.poses[i]);
        }

        // AudioSource
        var audio = GetComponent<AudioSource>();
        LogRef("AudioSource", audio);

        Debug.Log("[Debugger] === END REFERENCE VALIDATION ===");
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Press R to retrigger ControllersDown state
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.Log("[Debugger] R pressed — Re-triggering ControllersDown step by step...");
            RunControllersDownDiagnostic();
        }
    }

    void RunControllersDownDiagnostic()
    {
        Debug.Log("[Debugger] Step 1: passthrough.ShowRealWorld()");
        if (rm.passthrough == null) { Debug.LogError("[Debugger] FAIL — passthrough is null"); return; }
        if (rm.passthrough.passthroughLayer == null) { Debug.LogError("[Debugger] FAIL — passthrough.passthroughLayer is null"); return; }
        rm.passthrough.ShowRealWorld();
        Debug.Log("[Debugger] Step 1 OK");

        Debug.Log("[Debugger] Step 2: backgroundSphere.SetActive(false)");
        if (rm.backgroundSphere == null) { Debug.LogError("[Debugger] FAIL — backgroundSphere is null"); return; }
        rm.backgroundSphere.SetActive(false);
        Debug.Log("[Debugger] Step 2 OK — backgroundSphere is now: " + rm.backgroundSphere.activeSelf);

        Debug.Log("[Debugger] Step 3: promptBackground.SetActive(true)");
        if (rm.promptBackground == null) { Debug.LogError("[Debugger] FAIL — promptBackground is null"); return; }
        rm.promptBackground.SetActive(true);
        Debug.Log("[Debugger] Step 3 OK — promptBackground is now: " + rm.promptBackground.activeSelf);

        Debug.Log("[Debugger] Step 4: controllersDownPrompt.SetActive(true)");
        if (rm.controllersDownPrompt == null) { Debug.LogError("[Debugger] FAIL — controllersDownPrompt is null"); return; }
        rm.controllersDownPrompt.SetActive(true);
        Debug.Log("[Debugger] Step 4 OK — controllersDownPrompt is now: " + rm.controllersDownPrompt.activeSelf);

        Debug.Log("[Debugger] Step 5: audioSource.Play() with welcomeAudio");
        var audio = GetComponent<AudioSource>();
        if (audio == null) { Debug.LogError("[Debugger] FAIL — AudioSource is null"); return; }
        if (rm.welcomeAudio == null) { Debug.LogWarning("[Debugger] WARNING — welcomeAudio clip is null, skipping play"); }
        else
        {
            audio.clip = rm.welcomeAudio;
            audio.Play();
            Debug.Log("[Debugger] Step 5 OK — playing: " + rm.welcomeAudio.name);
        }

        Debug.Log("[Debugger] === All ControllersDown steps completed successfully ===");
    }

    void LogRef(string name, Object obj)
    {
        if (obj == null)
            Debug.LogError($"[Debugger] ❌ {name} is NULL — not assigned in Inspector!");
        else
            Debug.Log($"[Debugger] ✅ {name} = {obj.name}");
    }

    void LogRef(string name, AudioClip clip)
    {
        if (clip == null)
            Debug.LogWarning($"[Debugger] ⚠️ {name} is NULL — audio clip not assigned");
        else
            Debug.Log($"[Debugger] ✅ {name} = {clip.name}");
    }
}
