using UnityEngine;

public abstract class Pickup : MonoBehaviour, IPickable
{
    protected abstract int Points { get; }

    [SerializeField]
    private AudioClip pickupClip;

    protected virtual void Awake()
    {
        if (pickupClip == null) {
            Debug.LogError("Ensure pick up audio clip has been added");
        }
    }

    protected virtual void Update()
    {
        Move();
    }

    // To be overrriden in child
    protected abstract void Move();

    public void OnPick()
    {        
        GameManager.Instance.HandleScore(Points);
        if (pickupClip != null)
        {
            AudioSource.PlayClipAtPoint(pickupClip,transform.position);
        }
        Destroy(gameObject);
    }   


}
