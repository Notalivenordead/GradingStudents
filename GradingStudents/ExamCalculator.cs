using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingStudents
{
    public class ExamCalculator
    {
        // Максимальные значения для каждого модуля
        private const int MaxModule1Score = 22;
        private const int MaxModule2Score = 38;
        private const int MaxModule3Score = 20;

        // Метод для расчета общей суммы баллов и оценки
        public (int TotalScore, string Grade, string ErrorMessage) Calculate(int module1Score, int module2Score, int module3Score)
        {
            // Проверка на допустимые значения
            if (module1Score < 0 || module1Score > MaxModule1Score ||
                module2Score < 0 || module2Score > MaxModule2Score ||
                module3Score < 0 || module3Score > MaxModule3Score)
            {
                return (0, "", "Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).");
            }

            // Рассчитываем общую сумму баллов
            int totalScore = module1Score + module2Score + module3Score;

            // Определяем оценку по новой 5-балльной шкале
            string grade = CalculateGrade(totalScore);

            return (totalScore, grade, "");
        }

        // Метод для расчета оценки по новой 5-балльной шкале
        private string CalculateGrade(int totalScore)
        {
            if (totalScore >= 56 && totalScore <= 80)
                return "5 (отлично)";
            else if (totalScore >= 32 && totalScore <= 55)
                return "4 (хорошо)";
            else if (totalScore >= 16 && totalScore <= 31)
                return "3 (удовлетворительно)";
            else if (totalScore >= 0 && totalScore <= 15)
                return "2 (неудовлетворительно)";
            else
                return "Ошибка: Недопустимое значение баллов.";
        }
    }
}
