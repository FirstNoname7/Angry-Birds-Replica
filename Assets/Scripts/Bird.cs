using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //���������� ��� ����������� �����

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject nextBird; //������� ������ ��������� ������
    [SerializeField] private Rigidbody2D rb; //���������� ������ ���� ����� ���������� rb
    [SerializeField] private Rigidbody2D hook; //���������� ������ ���� �����, � �������� ����������� ������ �� � �������
    private bool isPressed = false; //���������� ��� ��������, ������ ������ ���� ��� ���
    private float releaseTime = 0.15f; //����� ��� ������� ������
    private float maxDragDistance = 2f; //������������ ����������, �� ������� ����� ����� �������� ������ �� ����� Hook

    void OnMouseDown() //���� ����� ��������� ��� ������� �� ������ ����
    {
        isPressed = true; //������ ���� ������
        rb.isKinematic = true; //� ���������� Rigidbody2D ������ BodyType � Dinamic �� Kinematic, ��������� � ����� ������� ������� ������ ��������� ���� ������
    }

    void OnMouseUp() //���������, ����� ����� �������� ������ ����
    {
        isPressed = false; //������ ���� �� ������
        rb.isKinematic = false; //� ���������� Rigidbody2D ��������� Kinematic � BodyType, ��������� � ����� ������� ������� ������ ��������� �� ���� ������
        StartCoroutine(Release()); //���������� �������� � ������ Release
    }

    private void Update() //���������� ������ �����
    {
        if (isPressed) //���� ������ ������ ���� ������, ��:
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������� ����������� ���� ������ ������� �� ������� ������� ���� (Input.mousePosition). main - ������ �� ������ MainCamera
            if (Vector2.Distance(mousePos, hook.position) > maxDragDistance) //���� ���������� ������� ������� ���� (mousePos) ������������ �����, �� ������� ����� ������ (hook.position), ������ ������������ ���������, �� ������� ����� ����������� ������ �� ����� (maxDragDistance), ��:
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance; //������� ������ = ������� ����� + �� ������� ������� ���� ���������� ������� ����� (�� ����� ����� �������� ��������� ������������) * ����. ����������, �� ������� ����� ����� �������� ������ �� ����� Hook
            }
            else //� �������� ������
            {
                rb.position = mousePos; //������� ������ = ������� ������� ����
            }
        }
    }

    IEnumerator Release() //������������������ �������� ��� ������� ������
    {
        yield return new WaitForSeconds(releaseTime); //���� releaseTime ������
        GetComponent<SpringJoint2D>().enabled = false; //��������� ��������� �������, ����� ����� ������ ���� ��������� ������ 1 ����
        this.enabled = false; //���� ������ ����������� (�.�. ������ ����� ������ ��� ��������, �� �� ����� �� ����� � ����� ���������)
        yield return new WaitForSeconds(2f); //���� 2 �������
        if (nextBird != null) //���� ���� ��������� ������, ��:
        {
            nextBird.SetActive(true); //������������ ��������� ������
        }
        else //� �������� ������
        {
            Pig.enemiesAlive = 0; //���������� ���-�� ������ �� 0 (������ �� �������)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //������������� ������� �����
        }
    }
}
