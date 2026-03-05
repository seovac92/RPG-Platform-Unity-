using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Buff details")]
    [SerializeField] private float buffDuration = 4;
    [SerializeField] private bool canBeUsed = true;
    [Header("Floaty movement")]
    [SerializeField] private float floatSpeed = 1;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 startPosition;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed) return;

        StartCoroutine(BuffCo(buffDuration));
    }
    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        Debug.Log("Buff is applied for: " + duration + " seconds");

        yield return new WaitForSeconds(duration);

        Debug.Log("Buff is removed");
        Destroy(gameObject);
    }
}
