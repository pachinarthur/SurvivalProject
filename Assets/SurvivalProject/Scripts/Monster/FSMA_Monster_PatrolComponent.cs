using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMA_Monster_PatrolComponent : MonoBehaviour
{
    public event Action<Vector3> OnPatrolLocationFound;
    public event Action<Vector3> OnTargetFound;
    [SerializeField] Vector3 targetLocation = Vector3.zero;
    [SerializeField] float range = 10;

    [SerializeField] Player target = null;
    [SerializeField] float minDist = 10;

    void Start()
    {

    }

    void Update()
    {
        DetectTarget();
    }

    public void FindRandomLocationInRange()
    {
        Vector2 _pos = UnityEngine.Random.insideUnitCircle;
        targetLocation = transform.position + new Vector3(_pos.x, 0, _pos.y) * range;
        //OnPatrolLocationFound?.Invoke(targetLocation);
    }

    public void DetectTarget()
    {
        if (!target) return;
        if (Vector3.Distance(transform.position, target.transform.position) <= minDist)
        {
            Debug.Log("Detected");
            OnTargetFound?.Invoke(target.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetLocation, .5f);
        Gizmos.color = Color.white;
    }
}
