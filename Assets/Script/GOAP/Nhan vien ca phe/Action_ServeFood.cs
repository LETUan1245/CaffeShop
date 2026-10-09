using UnityEngine;

public class Action_ServeFood : GAction
{
    private OrderInfo currentOrder;

    public override bool PrePerform()
    {
        // 1. Lấy thông tin đơn hàng đang cầm trên tay
        currentOrder = GWorld.Instance.readyOrder;
        if (currentOrder == null || currentOrder.customer == null) return false;

        // 2. Đích đến chính là vị Khách hàng
        target = currentOrder.table;
        return true;
    }

    public override bool PostPerform()
    {
        // 3. Hiện đúng món đồ ăn đó lên mặt bàn của Khách
        FoodDisplay tableFood = currentOrder.table.GetComponent<FoodDisplay>();
        if (tableFood != null)
        {
            tableFood.ShowFood(currentOrder.foodID);
        }

        // 4. Báo cho khách biết đã có đồ ăn (Bật cờ hasCoffee = 1)
        GAgent customerAgent = currentOrder.customer.GetComponent<GAgent>();
        if (customerAgent != null)
        {
            customerAgent.beliefs.ModifyState("hasCoffee", 1);
        }

        // 5. Phục vụ xong, xóa trí nhớ về đơn hàng này
        GWorld.Instance.readyOrder = null;
        return true;
    }
}