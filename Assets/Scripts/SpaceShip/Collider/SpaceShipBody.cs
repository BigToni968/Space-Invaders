using UnityEngine;
using System;

public class SpaceShipBody : MonoBehaviour
{
    // It's not a good name for an event
    // It would have been better to call it EnteredCollision or CollisionStarted
    public event Action<Collider2D> EnterCollider = delegate { };
    // Same here
    public event Action<Collider2D> ExitCollider = delegate { };
    private void OnCollisionEnter2D(Collision2D collision) => EnterCollider?.Invoke(collision.collider);
    private void OnTriggerEnter2D(Collider2D collision) => EnterCollider?.Invoke(collision);
    private void OnTriggerExit2D(Collider2D collision) => ExitCollider?.Invoke(collision);
}
