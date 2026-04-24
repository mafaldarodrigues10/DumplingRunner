using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform player;

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0f, 2.8f, -5.5f);
    public float smoothSpeed = 8f;
    public Vector3 lookOffset = new Vector3(0f, 1f, 8f);

    [Header("Jump Effect")]
    public float jumpTilt = 4f;
    public float jumpTiltSpeed = 6f;

    [Header("Shake Effect")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.15f;

    private Vector3 currentVelocity;
    private float currentTilt = 12f;
    private float targetTilt = 12f;
    private Vector3 shakeOffset = Vector3.zero;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
{
    if (GameManager.instance != null && GameManager.instance.isGameOver)
        return;

    if (player == null) return;

        Vector3 targetPosition = player.position + offset + shakeOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 1f / smoothSpeed);

        Quaternion targetRotation = Quaternion.LookRotation((player.position + lookOffset) - transform.position);
        Quaternion extraTilt = Quaternion.Euler(currentTilt, 0f, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * extraTilt, Time.deltaTime * smoothSpeed);

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * jumpTiltSpeed);
    }

    public void OnJump()
    {
        targetTilt = 16f;
        StopCoroutine(nameof(ResetJumpTilt));
        StartCoroutine(ResetJumpTilt());
    }

    IEnumerator ResetJumpTilt()
    {
        yield return new WaitForSeconds(0.15f);
        targetTilt = 12f;
    }

    public void ShakeCamera()
    {
        StopCoroutine(nameof(DoShake));
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            shakeOffset = new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
    }
}