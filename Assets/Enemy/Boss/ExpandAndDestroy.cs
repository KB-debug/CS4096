using System.Collections;
using UnityEngine;

public class ExpandAndDestroy : MonoBehaviour
{
    public float expandSpeed = 1f;
    public float damage = 2f;

    public float maxSize = 20f;

    public float duration = 10f;

    public int phase;

    public float flashSpeed = 10f;
    public Color flashColor = Color.red;
    public Color warnignColor = Color.blue;

    private Renderer renderer;
    private Color originalColor;
    private bool canDamage;
    private bool isDestroying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        float clamped = Mathf.Min(transform.localScale.x, maxSize);
        transform.localScale = new Vector3(clamped, clamped, clamped);

        if (transform.localScale.x >= maxSize && !isDestroying)
        {
            isDestroying = true;
            StartCoroutine(StartDestroyCountdown());
        }
    }

    IEnumerator StartDestroyCountdown()
    {
        if (phase == 2)
        {
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
        else
        {

            yield return new WaitForSeconds(duration / 2);

            float warningTimer = 0f;

            while (warningTimer < duration / 5)
            {
                warningTimer += Time.deltaTime;


                float t = Mathf.PingPong(Time.time * flashSpeed/5, 1f);
                renderer.material.color = Color.Lerp(originalColor, warnignColor, t);


                yield return null;
            }


            canDamage = true;
            float timer = 0f;

            while (timer < duration/2)
            {
                timer += Time.deltaTime;

                
                float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
                renderer.material.color = Color.Lerp(originalColor, flashColor, t);
                

                yield return null;
            }

            Destroy(gameObject);
        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (!canDamage)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.PlayerTakeDamage(damage * Time.deltaTime);
        }
    }

 
}
