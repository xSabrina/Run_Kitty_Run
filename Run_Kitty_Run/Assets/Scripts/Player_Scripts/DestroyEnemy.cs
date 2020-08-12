using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject death;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Border")
        {
            //Destroy(gameObject);
        }
        if (collision.transform.tag == "Enemy" && !collision.isTrigger)
        {
            Debug.Log(collision.transform.name);
            Destroy(collision.gameObject);
            Instantiate(death, collision.transform.position, collision.transform.rotation);
        }
    }

}
