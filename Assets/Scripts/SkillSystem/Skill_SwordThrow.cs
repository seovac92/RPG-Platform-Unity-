using UnityEngine;

public class Skill_SwordThrow : Skill_Base
{
    [Range(0, 10)]
    [SerializeField] private float throwPower = 6;
    [SerializeField] private float swordGravity = 4.5f;
    [Header("Trajectory prediction")]
    [SerializeField] private GameObject predictionDot;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float spaceBetweenDots = 0.05f;
    private Transform[] dots;
    private Vector2 confirmedDirection;

    protected override void Awake()
    {
        base.Awake();


        dots = GenerateDots();
    }
    public void ThrowSword()
    {
        Debug.Log("new sword");
    }
    public void PredictTrajectory(Vector2 direction)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].position = GetTrajectoryPoint(direction, i * spaceBetweenDots);
        }
    }
    private Vector2 GetTrajectoryPoint(Vector2 direction, float t)
    {
        float scaledThrowPower = throwPower * 10;

        Vector2 initialVelocity = direction * scaledThrowPower;

        Vector2 gravityEffect = 0.5f * Physics2D.gravity * swordGravity * (t * t);

        Vector2 predictedPoint = (initialVelocity * t) + gravityEffect;

        Vector2 playerPosition = transform.root.position;

        return playerPosition + predictedPoint;
    }
    public void ConfirmTrajectory(Vector2 direction) => confirmedDirection = direction;
    public void EnableDots(bool enable)
    {
        foreach (Transform t in dots)
        {
            t.gameObject.SetActive(enable);
        }
    }
    private Transform[] GenerateDots()
    {
        Transform[] newDots = new Transform[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            newDots[i] = Instantiate(predictionDot, transform.position, Quaternion.identity, transform).transform;
            newDots[i].gameObject.SetActive(false);
        }
        return newDots;
    }
}
