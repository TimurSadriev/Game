using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using YG;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
   public TextMeshProUGUI questionText; // Текст для отображения вопроса
    public TextMeshProUGUI remainingQuestionsText; // Текст для отображения оставшихся вопросов
    public TextMeshProUGUI scoreText; // Текст для отображения количества правильных ответов
    public InputField answerInput; // Поле для ввода ответа
    public Image flashImage;

    private int totalQuestions = 10; // Общее количество вопросов
    private int questionsAnswered = 0; // Количество отвеченных вопросов
    private int correctAnswer; // Правильный ответ

    private float totalResponseTime; // Общее время ответов

    void Start()
    {

        totalResponseTime = Time.time; // Получаем текущее время
      
        UpdateRemainingQuestionsText(); // Обновляем текст оставшихся вопросов
        StartNewQuestion(); // Запускаем первый вопрос
    }

    void StartNewQuestion()
    {
        // Генерируем случайные числа для вопроса
        int num1 = Random.Range(10, 50);
        int num2 = Random.Range(10, 50);
        correctAnswer = num1 * num2;

        // Обновляем текст вопроса
        questionText.text = $"{num1} * {num2} = ?";
        answerInput.text = string.Empty; // Очищаем поле ввода
        answerInput.ActivateInputField();
        answerInput.Select();
    }

    void CheckAnswer()
    {
        // Проверяем, введен ли правильный ответ
        if (int.TryParse(answerInput.text, out int playerAnswer))
        {

            if (playerAnswer == correctAnswer)
            {
 
                remainingQuestionsText.text = "Правильно!";
                StartCoroutine(FlashScreen(Color.green));
            }
            else
            {
                // Неправильный ответ
                remainingQuestionsText.text = "Неправильно!";
                StartCoroutine(FlashScreen(Color.red));
                EndGame();
                return;
            }

            questionsAnswered++;
            UpdateRemainingQuestionsText(); // Обновляем текст оставшихся вопросов

            if (questionsAnswered >= totalQuestions)
            {
                EndGame(); // Завершаем игру, если все вопросы отвечены
            }
            else
            {
                // Переход к следующему вопросу
                StartNewQuestion();
            }
        }
    }

    void Update()
    {            
     
 
        // Проверяем, нажата ли клавиша Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer(); // Проверяем ответ
        }
    }


    void UpdateRemainingQuestionsText()
    {
        int remainingQuestions = totalQuestions - questionsAnswered; // Рассчитываем оставшиеся вопросы
        remainingQuestionsText.text = $"Оставшиеся вопросы: {remainingQuestions}"; // Обновляем текст оставшихся вопросов
    }

    void EndGame()
    {
        totalResponseTime = Time.time -totalResponseTime;
        float averageResponseTime = totalResponseTime / totalQuestions * 1000; // Рассчитываем среднее время ответа в миллисекундах
        remainingQuestionsText.text = "Игра окончена!";
        questionText.text = $"Среднее время ответа: {averageResponseTime:F2} мс.";
        answerInput.interactable = false; // Отключаем поле ввода
        YandexGame.FullscreenShow();
    }
     IEnumerator FlashScreen(Color flashColor)
    {
    Color transparentColor = flashColor;
    transparentColor.a = 0.5f; // Устанавливаем альфа-канал

    flashImage.color = transparentColor; // Устанавливаем цвет мигания
    flashImage.gameObject.SetActive(true); // Активируем экран

    // Задержка для мигания
    yield return new WaitForSeconds(0.5f);

    // Возвращаем цвет к прозрачному (альфа = 0)
    transparentColor.a = 0f; // Полностью прозрачный
    flashImage.color = transparentColor; 
    yield return new WaitForSeconds(0.5f);

    flashImage.gameObject.SetActive(false); // Деактивируем экран
    }
}