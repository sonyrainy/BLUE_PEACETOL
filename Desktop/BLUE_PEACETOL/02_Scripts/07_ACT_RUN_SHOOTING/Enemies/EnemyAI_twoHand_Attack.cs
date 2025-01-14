using UnityEngine;

public class EnemyAI_TwoHand_Attack : EnemyAI_Final
{
    [SerializeField]
    private GameObject muzzleFlash;

    protected override void Start()
    {
        base.Start();
        
        fireSoundName = "rifle_Shot_Enemy";
    }

    protected override GameObject GetMuzzleFlash()
    {
        return muzzleFlash;
    }
}
