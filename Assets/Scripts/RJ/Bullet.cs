using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += transform.up * speed * Time.deltaTime;

        Destroy(bullet, 6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
         Destroy(bullet);
        }
    }
}
