using UnityEngine;

public class FloatObjets : MonoBehaviour

{
    public float waterLevel = 20f;
    public float profundidadAntesDeSumergirse = 1f;
    public float cantidadDeDesplazamiento = 3f;
    public Rigidbody rb;

    void FixedUpdate()
    {
        if (rb == null) return;

        // Comprobamos si la posición Y del barco está por debajo del nivel del agua (20)
        if (transform.position.y < waterLevel)
        {
            float profundidad = waterLevel - transform.position.y;
            float multiplicadorDeDesplazamiento = Mathf.Clamp01(
                (profundidad - profundidadAntesDeSumergirse) * cantidadDeDesplazamiento
            );

            rb.AddForce(
                new Vector3(
                    0f,
                    Mathf.Abs(Physics.gravity.y) * multiplicadorDeDesplazamiento,
                    0f
                ),
                ForceMode.Acceleration
            );
        }
    }
    
}
