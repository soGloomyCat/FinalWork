using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    private void Start()
    {
        ChangeScale();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ChangeScale(float length = 7.5f, float width = 0.025f)
    {
        gameObject.transform.localScale = new Vector3(width, length, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float distance = Vector2.Distance(collision.transform.position, _startPoint.position);
        ChangeScale(distance);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeScale();
    }
}
