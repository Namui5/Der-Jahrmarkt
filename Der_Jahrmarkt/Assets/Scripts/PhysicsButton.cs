using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f; // wie viel druck gebraucht wird, um Button zu aktivieren
    [SerializeField] private float deadZone = 0.025f; // damit es kein press und release die ganze Zeit gibt (bounciness)

    private bool isPressed; // prüft ob button schon gedrückt wurde
    private Vector3 startPos; // hilft die startpos mit der currentpos zu vergleichen um zu zeigen wie weit der button sich bewegt 
    private ConfigurableJoint joint; // man kriegt das lineare Limit davon

   // public GameObject ball;

    public UnityEvent onPressed, onReleased;
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    //    ball.active = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // wenn der player den button gedrückt hat, es aber noch nicht registriert wurde
        if(!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if (!isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    // Versychiedene buttons, buttonsgrößen, buttontiefen
    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        //deadzone checken
        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }


    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
     //   ball.active = true;
      //  ball.transform.position = new Vector3((float)533.8, (float)140.212, (float)175.29);

    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    //    ball.active = false;
    }

}
