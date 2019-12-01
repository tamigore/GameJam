using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Item : MonoBehaviour
{
    [SerializeField] GameObject Effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effect.GetComponent<Effect>().ApplyEffect(collision.gameObject);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
