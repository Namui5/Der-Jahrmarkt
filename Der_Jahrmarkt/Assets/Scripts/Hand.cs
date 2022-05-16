using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]

public class Hand : MonoBehaviour
{
    // Animation

    private Animator animator;
    private SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    [SerializeField] private float animationSpeed;
    private const string animatorGripParam = "Grip";
    private const string animatorTriggerParam = "Trigger";
    private static readonly int Grip = Animator.StringToHash(animatorGripParam);
    private static readonly int Trigger = Animator.StringToHash(animatorTriggerParam); 


    // Physics Movement
    [Space]
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [Space]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotateOffset;
    [Space]
    [SerializeField] private Transform palm; //position mit der ein Objekt Kontakt hat
    [SerializeField] private float reachDistance = 0.1f, joinDistance = 0.05f; //reach = auf welche distanz wir obj aufheben können ; join = wie nah an der Hand das Obj sein soll
    [SerializeField] private LayerMask grabbableLayer; //welche Obj wir aufnehmen können

    private Transform followTarget;
    private Rigidbody body;

    private bool isGrabbing;
    private GameObject heldObject;
    private Transform grabPoint; // grabpoint als nähste distanz die obj zu Hand hat (Kontaktpunkt)
    private FixedJoint joint1, joint2; //joint der Hand mit Obj verbindet und joint der Obj mit Hand verbindet

    // Start is called before the first frame update
    void Start()
    {
        //Animation
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>(); 

        //Physics Movement
        followTarget = controller.gameObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        body.maxAngularVelocity = 20f;

        //Wenn Kontroller gedrückt / losgelassen wird
        // Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;





        // Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       // AnimateHand();
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        //Position
        //Offset kalkulieren
        var positionWithOffset = followTarget.TransformPoint(positionOffset);

        var distance = Vector3.Distance(positionWithOffset, transform.position); // distanz von meinem currenttarget und der pos von von meiner HAnd 
        body.velocity = (followTarget.position - transform.position).normalized * (followSpeed * distance);


        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotateOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation); //distanz zwischen der rotation zwischen dem target und dere current hand 
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    private void Grab(InputAction.CallbackContext context)
    {
        if (isGrabbing || heldObject) return;

        Collider[] cgrabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (cgrabbableColliders.Length < 1) return; // methode verlassen, wenn man etwas greifen möchte, es aber nicht nah genug ist

        var objectToGrab = cgrabbableColliders[0].transform.gameObject;

        //schauen ob das obj dass gegriffen werden soll einen Rigidbody hat
        var objectBody = objectToGrab.GetComponent<Rigidbody>();
        if(objectBody != null)
        {
            heldObject = objectBody.gameObject;
        }
        else
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
            if(objectBody != null)
            {
                heldObject = objectBody.gameObject;
            }
            else
            {
                return;
            }
        }

        StartCoroutine(GrabObject(cgrabbableColliders[0], objectBody)); //hilfe closest Objekt zu finden
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
    {
        isGrabbing = true;

        // Grab point erstellen
        grabPoint = new GameObject().transform; //die position dees obj tranformieren
        grabPoint.position = collider.ClosestPoint(palm.position); //eine Lienie zeichenen vom palm zur colliderposition und findet heraus was der nährste punkt vom collider zum palm ist
        grabPoint.parent = heldObject.transform;

        //Hand zum Grabpoint bewegen
        followTarget = grabPoint;

        //Warten bis die Hand den Grabpoint erreicht hat
        while (grabPoint != null && Vector3.Distance(grabPoint.position, palm.position) > joinDistance && isGrabbing)
        {
            yield return new WaitForEndOfFrame(); //Distanz konmtrollieren und wenn es in joinDistance ist, wird der code weiter ausgeführt, wenn nicht wartet es bis zum ende des frames und geht es wieder durch whileschleife
        }

        //Hand- und Objbewegung einfrieren (damit währendessen keine random Bewegungen vorkommen)
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;

        // immer die physik updaten
        targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        targetBody.interpolation = RigidbodyInterpolation.Interpolate;

        //joints anfügen
            //VON HAND ZUM OBJEKT
        joint1 = gameObject.AddComponent<FixedJoint>();
        joint1.connectedBody = targetBody;
        joint1.breakForce = float.PositiveInfinity;
        joint1.breakTorque = float.PositiveInfinity;
            // die masse auf 1 setzen, um es bei der originalen masse zu belassen
        joint1.connectedMassScale = 1;
        joint1.massScale = 1;
        joint1.enableCollision = false; //damit obj die in hand sind nicht collidieren oder physik haben
        joint1.enablePreprocessing = false;

            //VON OBJEKT ZUR HAND
        joint2 = heldObject.AddComponent<FixedJoint>();
        joint2.connectedBody = body;
        joint2.breakForce = float.PositiveInfinity;
        joint2.breakTorque = float.PositiveInfinity;

        // die masse auf 1 setzen, um es bei der originalen masse zu belassen
        joint2.connectedMassScale = 1;
        joint2.massScale = 1;
        joint2.enableCollision = false; //damit obj die in hand sind nicht collidieren oder physik haben
        joint2.enablePreprocessing = false;

        //follow target zurücksetzen
        followTarget = controller.gameObject.transform;
    }

    private void Release(InputAction.CallbackContext context)
    {
        // wenn spieler obj loslässt alle joints zerstören und alle physics droppen
        if(joint1 != null)
        {
            Destroy(joint1);
        }
        if (joint2 != null)
        {
            Destroy(joint2);
        }

        if (grabPoint != null)
        {
            Destroy(grabPoint.gameObject);
        }

        //checken ob es ein heldobj gibt
        if(heldObject != null)
        {
            var targetBody = heldObject.GetComponent<Rigidbody>(); //Rigidbody von gameobj kriegen
            targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete; // colisionen- und interpolationmodes zurücksetzen
            targetBody.interpolation = RigidbodyInterpolation.None;
            heldObject = null; // reference zum heldobject löschen
        }

        isGrabbing = false;
        followTarget = controller.gameObject.transform;


    }



    
    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

   void AnimateHand()
   {
        if(Mathf.Abs(gripCurrent - gripTarget) > 0)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (Mathf.Abs(triggerCurrent - triggerTarget) > 0)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    } 
}
