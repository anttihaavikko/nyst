using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private GameObject arrow;
    
    private void Update()
    {
        var p = transform.position;
        var closest = targets.Where(t => t.gameObject.activeInHierarchy).OrderBy(t => Vector3.Distance(t.position, p)).FirstOrDefault();
        if (closest)
        {
            transform.LookAt(closest);   
        }
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
    }

    public void Show(bool state)
    {
        arrow.SetActive(state);
    }
}