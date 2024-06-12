
using UnityEngine;

public class Solidrawr : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Apply a force to the colliding object to prevent it from passing through the wall
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-collision.gameObject.transform.up * 10, ForceMode2D.Impulse);
    }
}
