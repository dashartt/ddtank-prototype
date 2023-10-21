using UnityEngine;

public class BulletController : MonoBehaviour
{
    private SystemController system;
    public int explosionRadius = 40;
    private void Start()
    {
        system = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SystemController>();        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Damageable") )
        {
            Debug.Log("Bounds contain the point : ");
            system.PassRound();
            Destroy(gameObject, 0.1f);
        }
    }

    public void OnDestroy()
    {

        Vector2 explosionPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (float)explosionRadius / 100);

        for (int i = 0; i < colliders.Length; i++)
        {
            // TODO: two calls for getcomponent is bad
            if (colliders[i].GetComponent<DestructibleSprite>())
                colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(explosionPos, explosionRadius);
        }
    }

}
