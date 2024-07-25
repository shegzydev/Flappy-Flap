using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WorldManager : MonoBehaviour
{
    [SerializeField] List<GameObject> pipes;
    [SerializeField] List<GameObject> bases;
    [SerializeField] float speed;

    void Start()
    {

    }

    void Update()
    {
        if (!GameManager.Instance.Began) return;

        var toremove = new List<int>();
        for (int i = 0; i < pipes.Count; i++)
        {
            pipes[i].transform.position += Vector3.left * speed * Time.deltaTime;
            if (Camera.main.WorldToScreenPoint(pipes[i].transform.position).x < -200)
            {
                float y = Mathf.Clamp(pipes[pipes.Count - 1].transform.position.y + Random.Range(-1, 2), -2.5f, 3.5f);
                pipes[i].transform.position = new Vector3(pipes[pipes.Count - 1].transform.position.x + 2.5f, y);
                toremove.Add(i);
            }
        }
        foreach (int i in toremove)
        {
            var go = pipes[i];
            pipes.RemoveAt(i);
            pipes.Add(go);
        }

        toremove.Clear();
        for (int i = 0; i < bases.Count; i++)
        {
            bases[i].transform.position += Vector3.left * speed * Time.deltaTime;
            if (Camera.main.WorldToScreenPoint(bases[i].transform.position).x < -600)
            {
                bases[i].transform.position = bases[bases.Count - 1].transform.position + Vector3.right * 3.11f;
                toremove.Add(i);
            }
        }
        foreach (int i in toremove)
        {
            var go = bases[i];
            bases.RemoveAt(i);
            bases.Add(go);
        }
    }

    private void FixedUpdate()
    {

    }
}
