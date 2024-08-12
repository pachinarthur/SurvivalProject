using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMA_Monster_Brain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] FSMA_Monster_EatComponent eat = null;
    [SerializeField] FSMA_Monster_IdleComponent sleep = null;
    [SerializeField] FSMA_Monster_MovementComponent movement = null;
    [SerializeField] FSMA_Monster_DetectionComponent detection = null;
    [SerializeField] Color debugColor = Color.white;
    [SerializeField] FSMA_Monster_Base[] behaviours = new FSMA_Monster_Base[] { };

    public FSMA_Monster_EatComponent Eat => eat;
    public FSMA_Monster_IdleComponent Sleep => sleep;
    public FSMA_Monster_MovementComponent Movement => movement;
    public FSMA_Monster_DetectionComponent Detection => detection;

    public bool IsValid => fsm && eat && sleep && movement;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        InitComponents();
        InitFSM();
        InitEvents();

    }

    void InitFSM()
    {
        if (!IsValid) return;
        behaviours = fsm.GetBehaviours<FSMA_Monster_Base>();
        int _size = behaviours.Length;
        //Debug.Log(behaviours);
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);
        }
    }

    void InitComponents()
    {
        fsm = GetComponent<Animator>();
        eat = GetComponent<FSMA_Monster_EatComponent>();
        sleep = GetComponent<FSMA_Monster_IdleComponent>();
        movement = GetComponent<FSMA_Monster_MovementComponent>();
        detection = GetComponent<FSMA_Monster_DetectionComponent>();
    }

    void InitEvents()
    {
        sleep.OnTimerElapsed += () =>
        {
            if (!detection.IsValid) return;
            fsm.SetBool(Monster_AnimParameter.IDLE_DONE, true);
            fsm.SetBool(Monster_AnimParameter.EAT_DONE, false);
        };

        detection.OnEntityDetected += movement.SetTarget;

        movement.OnDestinationReached += () =>
        {
            eat.Eat(detection.currentDetected);
            detection.RemoveEntity(detection.currentDetected);
            fsm.SetBool(Monster_AnimParameter.IDLE_DONE, false);
            fsm.SetBool(Monster_AnimParameter.EAT_DONE, true);
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetInstanceID() !=
            eat.Target.gameObject.GetInstanceID()) return;
        eat.Eat(detection.currentDetected);
        fsm.SetBool(Monster_AnimParameter.IDLE_DONE, false);
        fsm.SetBool(Monster_AnimParameter.EAT_DONE, true);
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawSphere(transform.position + Vector3.up, .5f);
        Gizmos.color = Color.white;
    }
}
