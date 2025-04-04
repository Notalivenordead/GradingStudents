using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradingStudents
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события нажатия на кнопку "Рассчитать"
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Сброс сообщения об ошибке
                ErrorMessage.Visibility = Visibility.Collapsed;

                // Получаем баллы из текстовых полей
                if (int.TryParse(Module1ScoreInput.Text, out int module1Score) &&
                    int.TryParse(Module2ScoreInput.Text, out int module2Score) &&
                    int.TryParse(Module3ScoreInput.Text, out int module3Score))
                {
                    // Проверяем, что баллы не превышают максимальные значения
                    if (module1Score > 22 || module2Score > 38 || module3Score > 20 ||
                        module1Score < 0 || module2Score < 0 || module3Score < 0)
                    {
                        ErrorMessage.Text = "Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).";
                        ErrorMessage.Visibility = Visibility.Visible;
                        return;
                    }

                    int totalScore = module1Score + module2Score + module3Score;
                    TotalScoreLabel.Text = totalScore.ToString();
                    string grade = CalculateGrade(totalScore);
                    GradeLabel.Text = grade;
                }
                else
                {
                    ErrorMessage.Text = "Ошибка: Введите корректные целые числа.";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
