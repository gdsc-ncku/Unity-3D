using UnityEngine;

[CreateAssetMenu(fileName = "ChemStudent", menuName = "Character/ChemStudent")]
public class ChemStudent : CharacterBaseData
{
    #region Hero_Q_Skill
    
    public float throwForce;
    public float throwUpwardForce;

    #endregion
    // throwing molocov cocktail
    public void UseQSkill(Transform cam, Transform attackPoint, GameObject objectToThrow)
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate throw direction
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

    public void UseESkill()
    {
    }
}
