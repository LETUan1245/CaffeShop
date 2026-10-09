using UnityEngine;

public class EmployeIdleState : EmployeBaseState
{
    private readonly int BlendHash = Animator.StringToHash("Blend");
    private readonly int LocomotionHash = Animator.StringToHash("LocomotionHash");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EmployeIdleState(EmployeStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        // Ép hoạt ảnh về dáng đứng im (tốc độ 0)
        stateMachine.Animator.SetFloat(BlendHash, 0f, AnimatorDampTime, deltaTime);

        // KIỂM TRA: Nếu GOAP ra lệnh di chuyển (Agent có đường đi) -> Đổi sang Move
        if (stateMachine.Agent.hasPath && stateMachine.Agent.remainingDistance > stateMachine.Agent.stoppingDistance)
        {
            stateMachine.SwitchState(new EmployeMoveState(stateMachine));
        }
    }

    public override void Exit() { }
}
