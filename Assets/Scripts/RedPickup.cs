using UnityEngine;

public class RedPickup : Pickup
{
    [SerializeField]
    private float slideDistance = 0.2f;

    [SerializeField]
    private int points = 10;



    [SerializeField]
    private float slideSpeed = 2f;

    private Vector3 _startPosition;

    private float _timeOffset;

    protected override int Points => points;

    protected override void Awake()
    {
        base.Awake();

        _startPosition = transform.position;
        _timeOffset = Random.Range(0f, 10f);
    }

    protected override void Move()
    {
        float offset = slideDistance * Mathf.Sin((Time.time) * slideSpeed);

        transform.position = _startPosition + transform.up * offset;
    }
}
