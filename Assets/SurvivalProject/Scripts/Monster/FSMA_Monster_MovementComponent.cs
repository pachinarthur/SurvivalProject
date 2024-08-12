using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FSMA_Monster_MovementComponent : MonoBehaviour
{
    public event Action OnDestinationReached = null;
    [SerializeField] float moveSpeed = 10, rotationSpeed = 50, minDistanceAllowed = .5f;
    [SerializeField] bool canMove = true;
    [SerializeField] FSMA_Food target = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] List<Vector3> path = new List<Vector3>();
    [SerializeField] int pathIndex = 0;

    public bool IsAtLocation
    {
        get
        {
            if (!target || path.Count < 1) return true;
            return Vector3.Distance(transform.position, path[pathIndex]) <= minDistanceAllowed;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        RotateTo();
    }

    void Init()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(FSMA_Food _target)
    {
        target = _target;
        canMove = _target != null;
        UpdatePath();
    }

    void UpdatePath()
    {
        if (!target || !agent) return;
        NavMeshPath _path = new NavMeshPath();
        if (!agent.CalculatePath(target.transform.position, _path)) return;
        path.Clear();
        path = _path.corners.ToList();
        pathIndex = 0;
    }

    void UpdatePathIndex()
    {
        if (path.Count < 1) return;
        if (pathIndex + 1 >= path.Count) return;
        pathIndex++;
    }

    void MoveTo()
    {
        //Debug.Log("Move before return");
        if (!canMove || path.Count < 1) return;
        //Debug.Log("Move after return");
        transform.position = Vector3.MoveTowards(transform.position, path[pathIndex], Time.deltaTime * moveSpeed);
        if (IsAtLocation)
        {
            Debug.Log("IsAtLocaton");
            if (pathIndex >= path.Count - 1)
            {
                Debug.Log("End of Path");
                OnDestinationReached?.Invoke();
                return;
            }
            UpdatePathIndex();
        }
    }

    void RotateTo()
    {
        if (!canMove || path.Count < 1 || IsAtLocation) return;
        Vector3 _look = path[pathIndex] - transform.position;
        if (_look == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_look);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        if (path.Count < 1) return;
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(path[i+1], 0.5f);
            if (i + 1 >= path.Count - 1) return;
            Gizmos.DrawLine(path[i], path[i + 1]);

        }
    }
}
