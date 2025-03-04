using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementDrop : MonoBehaviour
{
    [SerializeField] private GameObject cement;

    private void Awake()
    {
        cement.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            cement.SetActive(true);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Cement"))
        {
            collision.gameObject.GetComponent<Cement>().CementDrop();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
