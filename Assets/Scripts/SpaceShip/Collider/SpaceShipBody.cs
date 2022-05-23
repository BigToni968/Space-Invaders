using UnityEngine;
using System;

public class SpaceShipBody : MonoBehaviour
{
    public event Action<Collider2D> EnterCollider = delegate { };
    public event Action<Collider2D> ExitCollider = delegate { };
    private void OnCollisionEnter2D(Collision2D collision) => EnterCollider?.Invoke(collision.collider);
    private void OnTriggerEnter2D(Collider2D collision) => EnterCollider?.Invoke(collision);
    private void OnTriggerExit2D(Collider2D collision) => ExitCollider?.Invoke(collision);
}
