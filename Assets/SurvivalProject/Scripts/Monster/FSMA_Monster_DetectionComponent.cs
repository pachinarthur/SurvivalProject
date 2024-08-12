using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FSMA_Monster_DetectionComponent : MonoBehaviour
{
    public event Action<FSMA_Food> OnEntityDetected = null;
     [SerializeField] public List<FSMA_Food> allEntities = new List<FSMA_Food>();
    [SerializeField] public FSMA_Food currentDetected = null;
    [SerializeField] float detectionRange = 20;
    [SerializeField] bool debugCanDetect = true;
    [SerializeField] public bool hasDetected = false;

    public bool IsValid => allEntities.Count > 0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {

        List<FSMA_Food> _targets = FindObjectsOfType<FSMA_Food>().ToList();
        int _size = _targets.Count;
        for (int i = 0; i < _size; i++)
        {
            allEntities.Add(_targets[i]);
        }
    }

    FSMA_Food GetClosest()
    {
        if (!IsValid) return null;
        return allEntities.OrderBy(_entity => Vector3.Distance(_entity.transform.position, transform.position)).FirstOrDefault();
    }

    public void Detect()
    {
        FSMA_Food _target = GetClosest();

        if ((!_target && currentDetected) || !debugCanDetect || !_target)
        {
            OnEntityDetected?.Invoke(null);
            return;
        }
        Debug.Log("toto");
        
        float _dist = Vector3.Distance(_target.transform.position, transform.position);
        currentDetected = _dist <= detectionRange ? _target : null;
        OnEntityDetected?.Invoke(currentDetected);
    }

    public void RemoveEntity(FSMA_Food _gameObject)
    {
        if (!allEntities.Contains(_gameObject)) return;
        allEntities.Remove(_gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.white;
        int _size = allEntities.Count;
        if (_size < 1) return;
        for (int i = 0; i < _size; i++)
        {
            Gizmos.color = allEntities[i] == GetClosest() ? Color.magenta : Color.green;
            Gizmos.DrawLine(allEntities[i].transform.position, transform.position);
            Gizmos.color = Color.white;
        }
    }
}
