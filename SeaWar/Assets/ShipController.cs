using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Rigidbody rb;
    public float velocidad = 500f;
    public float velocidadRotacion = 1f;

    public float maxAngularVelocity = 1.5f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Limitamos la velocidad máxima de giro en las físicas
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    void FixedUpdate()
    {
        // Obtenemos el teclado actual del nuevo Input System
        Keyboard teclado = Keyboard.current;
        if (teclado == null) return; // Si no hay teclado conectado, salimos

        float moveVertical = 0f;
        float turnHorizontal = 0f;

        // Comprobación de teclas con el nuevo sistema
        if (teclado.wKey.isPressed || teclado.upArrowKey.isPressed) moveVertical = 1f;
        if (teclado.sKey.isPressed || teclado.downArrowKey.isPressed) moveVertical = -1f;

        if (teclado.aKey.isPressed || teclado.leftArrowKey.isPressed) turnHorizontal = -1f;
        if (teclado.dKey.isPressed || teclado.rightArrowKey.isPressed) turnHorizontal = 1f;

        // 1. Cálculo de traslación (avance y retroceso)
        float traslacion = moveVertical * velocidad * Time.fixedDeltaTime;
        Vector3 fuerzaMovimiento = new Vector3(0f, 0f, traslacion);
        rb.AddRelativeForce(fuerzaMovimiento, ForceMode.Force);

        // 2. Efecto Timón Real: El giro depende de que el barco se esté moviendo
        float forwardSpeed = Vector3.Dot(rb.velocity, transform.forward);
        
        // Si está parado (velocidad 0), el factor de giro es 0 para evitar giros sobre su propio eje
        float factorGiro = Mathf.Clamp01(Mathf.Abs(forwardSpeed) / 3f);

        float rotacion = turnHorizontal * velocidadRotacion * factorGiro * Time.fixedDeltaTime;

        // Aplicamos torque relativo al barco (eje Y para girar con inercia)
        Vector3 torqueRotacion = new Vector3(0f, rotacion, 0f);
        rb.AddRelativeTorque(torqueRotacion, ForceMode.Force);
    }
}
