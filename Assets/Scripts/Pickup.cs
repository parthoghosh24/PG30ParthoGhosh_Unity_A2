using UnityEngine;

public abstract class Pickup : MonoBehaviour, IPickable
{
    public void OnPick()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void Update()
    {
        
    }


}
