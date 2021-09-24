using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float enemyPoint = 100f;
    [SerializeField] float maxHit = 3f;
    [SerializeField] Transform parent;
    [SerializeField] GameObject enemyExplotionFX;

    bool isDead = false;
    PlayerController playerController;

    // Use this for initialization
    void Start () {
        AddNonTriggerCollider();
	}

    private void AddNonTriggerCollider()
    {
        MeshCollider meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshCollider.convex = true;
        meshCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(isDead == false)
        {
            if(maxHit <= Mathf.Epsilon)
            {
                ProcessDeath();
            } else
            {
                maxHit--;
            }
        }
    }

    private void ProcessDeath()
    {
        Destroy(gameObject);
        isDead = true;
        GameObject explotionInstance = Instantiate(enemyExplotionFX, transform.position, Quaternion.identity);
        explotionInstance.transform.parent = parent;
        FindObjectOfType<GameSession>().playerScore += enemyPoint;
    }
}
