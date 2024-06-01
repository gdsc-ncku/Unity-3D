using UnityEngine;

[CreateAssetMenu(fileName = "EEStudent", menuName = "Character/EEStudent")]
public class EEStudent : CharacterBaseData
{
    #region Hero_Q_Skill
    public float throwForce;
    public float throwUpwardForce;
    #endregion

    #region Hero_E_Skill

    #endregion

    // throwing run time error bomb
    public void UseQSkill(Transform cam, Transform attackPoint, GameObject objectToThrow)
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + cam.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    public void UseingE_Skill()
    {
        
    }
}
