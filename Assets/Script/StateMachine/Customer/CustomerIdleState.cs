using UnityEngine;

public class CustomerIdleState : CustomerBaseState
{
    private readonly int BlendHash = Animator.StringToHash("Blend");
    private readonly int LocomotionHash = Animator.StringToHash("LocomotionHash");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public CustomerIdleState(CustomerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(BlendHash, 0f, AnimatorDampTime, deltaTime);

        // THÊM ĐOẠN NÀY: Đang đứng rảnh mà thấy mông chạm ghế -> Chuyển sang tư thế ngồi luôn!
        if (stateMachine.SitDetector != null && stateMachine.SitDetector.currentSitPoint != null)
        {
            stateMachine.SwitchState(new CustomerSittingState(stateMachine));
            return;
        }

        // Logic kiểm tra GOAP ra lệnh di chuyển của bạn giữ nguyên ở dưới...
        if (stateMachine.Agent.hasPath && stateMachine.Agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            stateMachine.SwitchState(new CustomerMoveState(stateMachine));
        }
    }

    public override void Exit() { }
}
