using UnityEngine;

public class GoldBoostAbility : Ability
{
    [SerializeField] private Player player;
    [SerializeField] private float duration = 2;
    [SerializeField] private float goldMultiplier = 1.5f;
    private int baseValue;
    private float timerGB = 0.0f;
    private bool activated = false;

    protected override void Update()
    {
        if (activated)
        {
            timerGB += Time.deltaTime;
            if (timerGB >= duration)
            {
                activated = false;
                timerGB = 0.0f;
                CancelGoldBoost();
            }
            return;
        }
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!usable || activated)
        {
            return;
        }
        baseValue = player.GetMurderAward();
        player.SetMurderAward(Mathf.RoundToInt(baseValue * goldMultiplier));
        activated = true;
        usable = false;
    }

    private void CancelGoldBoost()
    {
        player.SetMurderAward(baseValue);
    }
}
