using UnityEngine;

public class BaristaAgent : GAgent
{
    protected override void Start()
    {
        base.Start(); // Tự động nạp Action_ServeCustomer

        // Goal 1: Nhận Order và đem về quầy nộp (Làm lặp đi lặp lại)
        SubGoal s1 = new SubGoal("OrderSubmitted", 1, false);
        goals.Add(s1, 3);

        // Goal 2: Bưng đồ ăn ra cho khách (Ưu tiên cao hơn)
        SubGoal s2 = new SubGoal("customerServed", 1, false);
        goals.Add(s2, 5);
    }
}