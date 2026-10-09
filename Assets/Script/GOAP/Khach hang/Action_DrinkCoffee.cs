using UnityEngine;

public class Action_DrinkCoffee : GAction
{
    public override bool PrePerform()
    {
        target = this.gameObject;

        // 1. TẠM TẮT BỘ NÃO FSM (Để nó không cãi lệnh GOAP)
        MonoBehaviour fsm = GetComponent("CustomerStateMachine") as MonoBehaviour;
        if (fsm != null) fsm.enabled = false;

        // 2. ÉP HOẠT ẢNH NGỒI
        Animator anim = this.GetComponent<Animator>();
        if (anim != null)
        {
            // Dùng Play để ép chạy ngay lập tức thay vì CrossFade
            anim.Play("Sitting");
        }

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}