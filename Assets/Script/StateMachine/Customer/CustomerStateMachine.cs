using UnityEngine;
using UnityEngine.AI;

public class CustomerStateMachine : StateMachine
{
    [field: SerializeField] public SitDetector SitDetector { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }


    [field: SerializeField] public float Speed { get; private set; }
    private void Start()
    {
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new CustomerIdleState(this));
    }
}