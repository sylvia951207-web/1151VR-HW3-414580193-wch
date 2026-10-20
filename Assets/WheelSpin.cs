using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(CircleCollider2D))]
public class WheelSpin : MonoBehaviour
{
    public float duration = 6f;

    private bool isSpinning;
    private float elapsed;
    private float startAngle;
    private float totalAngle;

    private Camera mainCamera;
    private CircleCollider2D wheelCollider;

    private AudioSource spinAudio;

    private void Awake()
{
    mainCamera = Camera.main;
    wheelCollider = GetComponent<CircleCollider2D>();
    spinAudio = GetComponent<AudioSource>();
}

    private void Update()
    {
        if (!isSpinning)
        {
            CheckClick();
            return;
        }

        elapsed += Time.deltaTime;

        float t = Mathf.Clamp01(
            elapsed / Mathf.Max(duration, 0.1f)
        );

        // 先加速，再慢慢減速
        float progress = t
            - Mathf.Sin(2f * Mathf.PI * t) / (2f * Mathf.PI);

        float angle = startAngle + totalAngle * progress;

        transform.localRotation =
            Quaternion.Euler(0f, 0f, angle);

        if (t >= 1f)
        {
            isSpinning = false;
        }
    }

    private void CheckClick()
    {
        Vector2 screenPosition;

#if ENABLE_INPUT_SYSTEM
        // 手機觸碰
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // 滑鼠或觸控板點擊
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

#else
        // 支援舊版輸入設定
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;
        }
        else
        {
            return;
        }
#endif

        if (mainCamera == null)
        {
            return;
        }

        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(screenPosition);

        // 只有點到輪盤範圍才啟動
        if (wheelCollider.OverlapPoint(worldPosition))
        {
            Spin();
        }
    }

    public void Spin()
{
    if (isSpinning)
    {
        return;
    }

    startAngle = transform.localEulerAngles.z;

    int selected = Random.Range(0, 8);
    float targetAngle = 22.5f + selected * 45f;
    int turns = Random.Range(6, 9);

    totalAngle = turns * 360f
        + Mathf.Repeat(targetAngle - startAngle, 360f);

    // 有音效時，旋轉時間自動配合音效長度
    if (spinAudio != null && spinAudio.clip != null)
    {
        spinAudio.loop = false;
        spinAudio.pitch = 1f;
        duration = spinAudio.clip.length;
        spinAudio.Play();
    }

    elapsed = 0f;
    isSpinning = true;
}
}