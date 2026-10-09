using UnityEngine;

public class Action_PickupFood : GAction
{
    public override bool PrePerform()
    {
        if (GWorld.Instance.readyOrder == null) return false;

        // 1. Tìm chính xác điểm tàng hình để Nhân viên chạy ra
        target = GameObject.Find("Diemlaydoan");
        if (target == null) return false;

        return true;
    }

    public override bool PostPerform()
    {
        // 2. Tìm cái quầy thật để tắt món ăn đi (giả lập việc cầm lên tay)
        GameObject realCounter = GameObject.Find("PW_cupboard_corner");
        if (realCounter != null)
        {
            FoodDisplay counterFood = realCounter.GetComponent<FoodDisplay>();
            if (counterFood != null) counterFood.HideAllFood();
        }

        // 3. Đã lấy đồ xong, báo cho thế giới biết quầy đã trống
        GWorld.Instance.GetWorld().ModifyState("foodReady", -1);
        return true;
    }
}