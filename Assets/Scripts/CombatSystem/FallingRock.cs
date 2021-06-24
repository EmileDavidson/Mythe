using UnityEngine;
using Random = UnityEngine.Random;

public class FallingRock : MonoBehaviour
{
    public int minDamage = 10;
    public int maxDamage = 20;

    public int destroyAfter = 5;
    public Rigidbody rigidbody;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLISION");
        //logic
        Destroy(rigidbody);
        //spawn explosion particle
        
        //remove health if its the player.
        if (other.gameObject.layer == (int) Layer.Player)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.RemoveHealth(Random.Range(minDamage, maxDamage));
            }
        }

        Destroy(this.gameObject);
    }
}
