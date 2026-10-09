using UnityEngine;

public class ChefAgent : GAgent
{
    protected override void Start()
    {
        // 1. Bắt buộc gọi base.Start() để tự động nạp Action_PrepareFood
        base.Start();

        // 2. Mục tiêu của Đầu bếp là luôn tạo ra đồ ăn ("foodReady" = 1)
        // Tham số 'false' giúp mục tiêu này lặp đi lặp lại mãi mãi thay vì làm 1 lần rồi nghỉ
        SubGoal s1 = new SubGoal("foodReady", 1, false);
        goals.Add(s1, 1);
    }
}
