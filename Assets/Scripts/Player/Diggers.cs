using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggers : MonoBehaviour
{
    InputReader inputReader;
    [SerializeField] float waitingTimeToActivateDigger = 0.4f;
    [SerializeField] GameObject diggerRX;
    [SerializeField] GameObject diggerSX;
    [SerializeField] GameObject diggerDown;

    Rigidbody2D myRigidbody;

    private bool isCheckingRight = false;
    private bool isCheckingLeft = false;
    private bool isCheckingDown = false;

    [SerializeField] DiggingCheck[] diggingChecks; 
    

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Drill.IsDigging) return;
        if (!Grounded.Instance.IsGrounded()) return;
        CheckDiggerActivation((inputReader.MovementInput.x == 1 || inputReader.MovementInput.x == -1) && inputReader.MovementInput.y == 0 && diggingChecks[0].PositiveCheckRight, ref isCheckingRight, diggerRX);
        CheckDiggerActivation(inputReader.MovementInput.x == -1 && inputReader.MovementInput.y == 0 && diggingChecks[1].PositiveCheckLeft, ref isCheckingLeft, diggerSX);
        CheckDiggerActivation(inputReader.MovementInput.y == -1 && inputReader.MovementInput.x == 0 && diggingChecks[2].PositiveCheckDown, ref isCheckingDown, diggerDown);
    }

    void CheckDiggerActivation(bool condition, ref bool isChecking, GameObject digger)
    {
        if (condition && !isChecking)
        {
            isChecking = true;
            //Debug.Log(myRigidbody.velocity.x + " " + myRigidbody.velocity.y);
            StartCoroutine(ActivateDiggerAfterDelay(digger, condition));
        }
        else if (!condition && isChecking)
        {
            isChecking = false;
            StopAllCoroutines();
            digger.SetActive(false);
        }

        else if (!condition && !isChecking)
        {
            digger.SetActive(false);
        }
    }

    IEnumerator ActivateDiggerAfterDelay(GameObject digger, bool condition)
    {

        yield return new WaitForSeconds(waitingTimeToActivateDigger);
        //if (Mathf.Abs(myRigidbody.velocity.x) > 0.1f || Mathf.Abs(myRigidbody.velocity.y) > 0.1f) yield return;
        digger.SetActive(true);
    }

    public void DisableAllDiggers()
    {
        StopAllCoroutines();
        isCheckingRight = false; isCheckingLeft = false; isCheckingDown = false;
        diggerDown.SetActive(false);
        diggerRX.SetActive(false);
        diggerSX.SetActive(false);
    }
}
