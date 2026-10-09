using UnityEngine;

public class CustomerAgent : GAgent
{
    protected override void Start()
    {
        base.Start();

        // Goal 1: Ưu tiên tìm bàn ngồi trước (Priority = 3)
        SubGoal s1 = new SubGoal("isSeated", 1, true);
        goals.Add(s1, 3);

        // Goal 2: Khi đã có cà phê thì mới đi về (Priority = 5)
        SubGoal s2 = new SubGoal("isHome", 1, true);
        goals.Add(s2, 5);
    }
}