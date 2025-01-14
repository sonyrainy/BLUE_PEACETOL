using UnityEngine;

public class EnemyAI_OneHand_Attack : EnemyAI_Final
{
    [SerializeField]
    private GameObject muzzleFlash;

    protected override void Start()
    {
        base.Start();
        
        fireSoundName = "revolver_Shot_Enemy";
    }

    protected override GameObject GetMuzzleFlash()
    {
        return muzzleFlash;
    }
}
