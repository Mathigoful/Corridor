using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter()
    {
            Debug.Log("Trigger entered");
            Destroy(this.gameObject);
    }
}
