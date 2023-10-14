using System;
using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class PlayerThrower : NinjaMonoBehaviour {
    public Transform throwTarget; 
    public Transform throwInitialPos; 
    [SerializeField] float minForce = 3f;
    [SerializeField] float maxForce = 12f; // Desired speed for the throw // Will be the force used from the meter
    [SerializeField] float calculatedForce; // Desired speed for the throw // Will be the force used from the meter
    public float gravity = 9.81f;
    [SerializeField] float firstPathPointMultiplier = 2f;
    [SerializeField] float secondPathPointMultiplier = 3f;

    public bool IsThrowing {get; private set;}
    private Basketball currentBasketball;
    private Vector3[] throwPath;
    private float throwDuration;
    private float throwStartTime;
    private int currentThrowIndex = 0;
    public float lastThrowDistance;
    public void ThrowBasketball(Basketball basketball, Board targetBoard) {
        var logId = "ThrowBasketball";
        currentBasketball = basketball;
        throwTarget = targetBoard.ThrowTarget;
        lastThrowDistance = Vector3.Distance(transform.position, throwTarget.position);
        if (currentBasketball==null || targetBoard==null || IsThrowing) {
            logw(logId, "CurrentBasketball="+currentBasketball.logf() + " TargetBoard="+targetBoard.logf()+" IsThrowing="+IsThrowing);
            return;
        }
        IsThrowing = true;
        logd(logId, "Starting Throw!");
        StartCoroutine(SimulateThrow());
    }
    private List<Vector3> ballPath;
    private IEnumerator RecordBallPathRoutine() {
        var startTime = Time.fixedTime;
        while(currentBasketball && Time.fixedTime - startTime < 1) {
            ballPath.Add(currentBasketball.transform.position);
            yield return new WaitForSeconds(0.02f);
        }
    }
    private void OnDrawGizmos() {
        if (throwPath != null && throwPath.Length > 1)
        {
            Gizmos.color = Color.red;
            for (int i = 1; i < throwPath.Length; i++)
            {
                Gizmos.DrawLine(throwPath[i - 1], throwPath[i]);
            }
        }
        if (ballPath != null && ballPath.Count > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 1; i < ballPath.Count; i++)
            {
                Gizmos.DrawLine(ballPath[i - 1], ballPath[i]);
            }
        }
        Gizmos.DrawWireSphere(breakpoint, 0.2f);
    }

    private void CreateBezierPath(Vector3[] points)
    {
        for (int i = 0; i < throwPath.Length; i++)
        {
            float t = i / (float)(throwPath.Length - 1);
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * points[0];
            p += 3 * uu * t * points[1];
            p += 3 * u * tt * points[2];
            p += ttt * points[3];

            throwPath[i] = p;
        }
    }
    Vector3 breakpoint;
    IEnumerator SimulateThrow() {
        Vector3 initialPosition = throwInitialPos.position;
        Vector3 targetPosition = throwTarget.position;

        Vector3 direction = (targetPosition - initialPosition).normalized;

        float horizontalDistance = Vector3.Distance(initialPosition, targetPosition);

        float timeOfFlight = horizontalDistance / calculatedForce;

        Vector3[] points = new Vector3[4];
        points[0] = initialPosition;
        points[1] = initialPosition + direction * (horizontalDistance / 2f) + Vector3.up * firstPathPointMultiplier * timeOfFlight;
        points[2] = targetPosition - direction * (horizontalDistance / 2f) + Vector3.up * secondPathPointMultiplier * timeOfFlight;
        points[3] = targetPosition;

        throwDuration = timeOfFlight;
        throwPath = new Vector3[(int)(throwDuration * 50)]; // 60 points per second

        CreateBezierPath(points);

        throwStartTime = Time.time;
        currentThrowIndex = 0;
        currentBasketball.Throw();

        while (IsThrowing && currentThrowIndex < throwPath.Length) {
            float elapsedTime = Time.time - throwStartTime;
            float t = elapsedTime / throwDuration;
            currentThrowIndex = (int)(t * (throwPath.Length - 1));

            if (t >= breakpointPos) {
                IsThrowing = false;
                ballPath = new List<Vector3>();
                Vector3 finalVelocity = CalculateFinalVelocity();
                currentBasketball.OnThrowEnd(finalVelocity);
                StartCoroutine(RecordBallPathRoutine());
                yield break;
            }

            currentBasketball.transform.position = throwPath[currentThrowIndex];
            breakpoint = throwPath[currentThrowIndex];
            yield return new WaitForFixedUpdate();
        }

    }
    [Range(0.1f, 0.99f)]
    [SerializeField] float breakpointPos = 0.8f;
    private Vector3 CalculateFinalVelocity() {
        var logId = "CalculateFinalVelocity";
        var pathLength = throwPath.Length;
        if (currentThrowIndex + 1 < pathLength) {
            float deltaTime = throwDuration / (pathLength - 1);

            Vector3 firstPosition = throwPath[currentThrowIndex];
            Vector3 secondPosition = throwPath[currentThrowIndex + 1];
            Vector3 finalVelocity = (secondPosition - firstPosition) / deltaTime;

            logd(logId, "Calculated BallPos=" + currentBasketball.transform.position + " TargetPos=" + throwTarget.position + " FinalVelocity=" + finalVelocity);
            return finalVelocity;
        }

        Vector3 remainingPath = throwTarget.position - currentBasketball.transform.position;
        float remainingTime = remainingPath.magnitude / calculatedForce;
        remainingTime = Mathf.Max(remainingTime, 0.001f);
        Vector3 fallbackFinalVelocity = remainingPath / remainingTime;
        logw(logId, "Not enough path left => FinalVelocity=" + fallbackFinalVelocity);
        return fallbackFinalVelocity;
    }

    public void SetThrowForce(float force) {
        calculatedForce = Mathf.Lerp(minForce, maxForce, force);
        logd("SetThrowForce", "MinForce="+minForce+" MaxForce="+maxForce+" Force="+force+" CalculatedForce="+calculatedForce);
    }
}