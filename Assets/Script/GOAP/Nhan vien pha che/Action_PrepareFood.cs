using UnityEngine;

// Đầu bếp nhận Order và làm đồ ăn đặt lên quầy
public class Action_PrepareFood : GAction
{
    private OrderInfo currentOrder;

    public override bool PrePerform()
    {
        currentOrder = GWorld.Instance.GetNextOrder();
        if (currentOrder == null) return false;

        // 1. Tìm chính xác điểm tàng hình để Đầu bếp đi tới đứng
        target = GameObject.Find("Diemlaydoan");
        if (target == null) return false;

        return true;
    }

    public override bool PostPerform()
    {
        // 2. Tìm cái quầy thật để gọi script hiển thị món ăn
        GameObject realCounter = GameObject.Find("PW_cupboard_corner");
        if (realCounter != null)
        {
            FoodDisplay counterFood = realCounter.GetComponent<FoodDisplay>();
            if (counterFood != null) counterFood.ShowFood(currentOrder.foodID);
        }

        // 3. Nấu xong, báo cho hệ thống để Nhân viên ra lấy
        GWorld.Instance.readyOrder = currentOrder;
        GWorld.Instance.GetWorld().ModifyState("foodReady", 1);
        GWorld.Instance.GetWorld().ModifyState("hasOrdered", -1);

        currentOrder = null;
        return true;
    }
}