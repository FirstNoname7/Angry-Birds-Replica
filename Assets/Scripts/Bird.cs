using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //библиотека для перезапуска сцены

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject nextBird; //игровой объект следующей птички
    [SerializeField] private Rigidbody2D rb; //подключаем жёсткое тело через переменную rb
    [SerializeField] private Rigidbody2D hook; //подключаем жёсткое тело крюка, к которому прикреплена птичка до её запуска
    private bool isPressed = false; //переменная для проверки, нажата кнопка мыши или нет
    private float releaseTime = 0.15f; //время для выпуска птички
    private float maxDragDistance = 2f; //максимальное расстояние, на которое игрок может оттянуть птичку от точки Hook

    void OnMouseDown() //этот метод сработает при нажатии на кнопку мыши
    {
        isPressed = true; //кнопка мыши нажата
        rb.isKinematic = true; //в компоненте Rigidbody2D меняем BodyType с Dinamic на Kinematic, поскольку с этого момента физикой птички управляет этот скрипт
    }

    void OnMouseUp() //сработает, когда игрок отпустил кнопку мыши
    {
        isPressed = false; //кнопка мыши не нажата
        rb.isKinematic = false; //в компоненте Rigidbody2D отключаем Kinematic у BodyType, поскольку с этого момента физикой птички управляет не этот скрипт
        StartCoroutine(Release()); //вызывается корутина с именем Release
    }

    private void Update() //вызывается каждый фрейм
    {
        if (isPressed) //если сейчас кнопка мыши нажата, то:
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //позиция физического тела птички зависит от позиции курсора мыши (Input.mousePosition). main - ссылка на объект MainCamera
            if (Vector2.Distance(mousePos, hook.position) > maxDragDistance) //если расстояние позиции курсора мыши (mousePos) относительно крюка, на котором висит птичка (hook.position), больше максимальной дистанции, на которую может отклониться птичка от крюка (maxDragDistance), то:
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance; //позиция птички = позиция крюка + от позиции курсора мыши отнимается позиция крюка (по факту птица начинает медленнее оттягиваться) * макс. расстояние, на которое игрок может оттянуть птичку от точки Hook
            }
            else //в обратном случае
            {
                rb.position = mousePos; //позиция птички = позиции курсора мыши
            }
        }
    }

    IEnumerator Release() //последовательность действий для выпуска птички
    {
        yield return new WaitForSeconds(releaseTime); //ждем releaseTime секунд
        GetComponent<SpringJoint2D>().enabled = false; //отключаем компонент пружины, чтобы птицу нельзя было запустить больше 1 раза
        this.enabled = false; //этот скрипт отключается (т.е. теперь когда птичка уже вылетела, мы не можем ее взять и снова запустить)
        yield return new WaitForSeconds(2f); //ждем 2 секунды
        if (nextBird != null) //если есть следующая птичка, то:
        {
            nextBird.SetActive(true); //активируется следующая птичка
        }
        else //в обратном случае
        {
            Pig.enemiesAlive = 0; //сбрасываем кол-во врагов на 0 (ставим по дефолту)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //перезапускаем текущую сцену
        }
    }
}
