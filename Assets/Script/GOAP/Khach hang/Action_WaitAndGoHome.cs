using UnityEngine;

public class Action_WaitAndGoHome : GAction
{
    public override bool PrePerform()
    {
        target = GameObject.FindWithTag("Home");
        if (target == null) return false;

        // 1. BẬT LẠI BỘ NÃO FSM (Để khách tự động dùng Blend Tree đi bộ)
        MonoBehaviour fsm = GetComponent("CustomerStateMachine") as MonoBehaviour;
        if (fsm != null) fsm.enabled = true;

        return true;
    }

    public override bool PostPerform()
    {
        // 2. Tắt đồ ăn trên bàn
        SitDetector sitDetector = this.GetComponentInChildren<SitDetector>();
        if (sitDetector != null && sitDetector.currentSitPoint != null)
        {
            FoodDisplay tableFood = sitDetector.currentSitPoint.parent.GetComponent<FoodDisplay>();
            if (tableFood != null) tableFood.HideAllFood();
        }

        // 3. Trả lại bàn trống cho quán
        GWorld.Instance.GetWorld().ModifyState("FreeTable", 1);

        // 4. CHỈ HỦY NHÂN VẬT KHI ĐÃ ĐI TỚI TẬN CỬA
        Destroy(this.gameObject, 0.5f);

        return true;
    }
}