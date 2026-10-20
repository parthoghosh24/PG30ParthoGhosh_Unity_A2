using UnityEngine;

public class GreenPickup : Pickup
{
    [SerializeField]
    private int points = 5;

    protected override int Points => points;

    protected override void Move()
    {
        //TODO: NADA
    }
}
