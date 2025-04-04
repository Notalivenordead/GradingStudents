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
        private readonly ExamCalculator _calculator = new ExamCalculator();

        public MainWindow() => InitializeComponent();

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
                    // Вызываем метод расчета из класса ExamCalculator
                    var result = _calculator.Calculate(module1Score, module2Score, module3Score);

                    // Если есть ошибка, выводим её
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ErrorMessage.Text = result.ErrorMessage;
                        ErrorMessage.Visibility = Visibility.Visible;
                        return;
                    }

                    // Обновляем метки с результатами
                    TotalScoreLabel.Text = result.TotalScore.ToString();
                    GradeLabel.Text = result.Grade;
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
    }
}
