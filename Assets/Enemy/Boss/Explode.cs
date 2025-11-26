using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{

    public float explosionDelay = 2f;
    public float explosionRadius = 5f;
    public float flashSpeed = 10f;
    public Color flashColor = Color.red;

    public float damage = 5f;

    private Transform player;
    private bool isTriggered = false;
    private Renderer[] renderers;
    private Color[] originalColors;

    private SphereCollider collider;

    public GameObject particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null || isTriggered)
            return;


        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < explosionRadius) {
            StartCoroutine(ExplodeAfterDelay());
        }
    }


    private IEnumerator ExplodeAfterDelay()
    {
        float timer = 0f;

        while (timer < explosionDelay)
        {
            timer += Time.deltaTime;

            for (int i = 0; i < renderers.Length; i++)
            {
                float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
                renderers[i].material.color = Color.Lerp(originalColors[i], flashColor, t);
            }

            yield return null;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
        collider.enabled = true;
        Debug.Log("Blowing Up");
        
        yield return new WaitForSeconds(.5f);
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject ply = other.gameObject;
            PlayerStats logic = ply.GetComponent<PlayerStats>();
            if (logic != null)
            {
                PlayerStats.PlayerTakeDamage(damage);
            }
        }
    }
}
