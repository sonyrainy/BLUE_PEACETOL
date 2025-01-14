using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    [Header("Answer Code (4 Digits, 0-9)")]
    public string answerCode = "0000"; 

    [Header("Disk Objects")]
    public Transform[] disks; 

    [Header("Answer Tolerance")]
    public float tolerance = 5f; 

    private float[] correctRotation; 
    private bool isSolved = false; 

    void Start()
    {
        // 정답 각도를 초기화
        SetCorrectRotations();
    }

    void Update()
    {
        if (!isSolved)
        {
            CheckAnswer();
        }
    }

    // 정답 코드에 따라 각 디스크의 목표 각도를 설정하는 함수
    void SetCorrectRotations()
    {
        correctRotation = new float[disks.Length];
        for (int i = 0; i < answerCode.Length && i < disks.Length; i++)
        {
            // 각 숫자에 해당하는 각도를 -180~180 범위로 설정
            int number = int.Parse(answerCode[i].ToString());
            correctRotation[i] = Mathf.Lerp(-180f, 180f, number / 9f);
            Debug.Log($"디스크 {i} 정답 각도: {correctRotation[i]}");
        }
    }

    // 각 디스크의 현재 회전 값이 정답 각도에 맞는지 검사
    void CheckAnswer()
    {
        for (int i = 0; i < disks.Length; i++)
        {
            float currentRotation = disks[i].localEulerAngles.x;

            // 현재 각도를 -180~180 범위로 변환
            currentRotation = NormalizeAngle(currentRotation);
            

            Debug.Log($"디스크 {i} 현재 각도: {currentRotation}, 정답 각도: {correctRotation[i]}, 오차 범위 내 여부: {Mathf.Abs(currentRotation - correctRotation[i]) <= tolerance}");
            
            if (Mathf.Abs(currentRotation - correctRotation[i]) > tolerance)
            {
                return;
            }
        }
        
        // 모든 디스크가 정답 위치에 도달한 경우
        ProcessCorrectAnswer();
    }

    // 각도를 -180도에서 180도로 정규화
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    // 정답을 처리
    void ProcessCorrectAnswer()
    {
        isSolved = true; 
        Debug.Log("정답 처리 완료!"); 

        // ex) 정답 처리를 위해 다른 스크립트 호출이나 애니메이션 실행, 효과음 재생 등
        OpenLock();
    }

    // 정답 처리가 완료되었을 때 실행할 추가 동작 (문을 여는 등)
    void OpenLock()
    {
        Debug.Log("문이 열렸습니다!");
    }
}
