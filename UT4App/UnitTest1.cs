using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GradingStudents;

namespace UT4App
{
    [TestClass]
    public class PositiveTests
    {
        private readonly ExamCalculator _calculator = new ExamCalculator();

        [TestMethod]
        public void Test_ValidScores_CalculateGrade_Successfully()
        {
            // Arrange
            int module1Score = 20;
            int module2Score = 30;
            int module3Score = 15;

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(65, TotalScore);
            Assert.AreEqual("5 (отлично)", Grade);
            Assert.AreEqual("", ErrorMessage);
        }

        [TestMethod]
        public void Test_ValidScores_LowerBoundary()
        {
            // Arrange
            int module1Score = 0;
            int module2Score = 0;
            int module3Score = 0;

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(0, TotalScore);
            Assert.AreEqual("2 (неудовлетворительно)", Grade);
            Assert.AreEqual("", ErrorMessage);
        }

        [TestMethod]
        public void Test_ValidScores_UpperBoundary()
        {
            // Arrange
            int module1Score = 22; // Максимальное значение для модуля 1
            int module2Score = 38; // Максимальное значение для модуля 2
            int module3Score = 20; // Максимальное значение для модуля 3

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(80, TotalScore);
            Assert.AreEqual("5 (отлично)", Grade);
            Assert.AreEqual("", ErrorMessage);
        }
    }

    [TestClass]
    public class NegativeTests
    {
        private readonly ExamCalculator _calculator = new ExamCalculator();

        [TestMethod]
        public void Test_InvalidScores_ExceedMaximum()
        {
            // Arrange
            int module1Score = 30; // Превышает максимальное значение для модуля 1
            int module2Score = 38;
            int module3Score = 20;

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(0, TotalScore);
            Assert.AreEqual("", Grade);
            Assert.AreEqual("Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).", ErrorMessage);
        }

        [TestMethod]
        public void Test_InvalidScores_NegativeNumbers()
        {
            // Arrange
            int module1Score = -5; // Отрицательное число
            int module2Score = 30;
            int module3Score = 15;

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(0, TotalScore);
            Assert.AreEqual("", Grade);
            Assert.AreEqual("Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).", ErrorMessage);
        }

        [TestMethod]
        public void Test_InvalidScores_MixedBoundaries()
        {
            // Arrange
            int module1Score = 25; // Превышает максимум для модуля 1
            int module2Score = -10; // Отрицательное число для модуля 2
            int module3Score = 20;

            // Act
            var (TotalScore, Grade, ErrorMessage) = _calculator.Calculate(module1Score, module2Score, module3Score);

            // Assert
            Assert.AreEqual(0, TotalScore);
            Assert.AreEqual("", Grade);
            Assert.AreEqual("Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).", ErrorMessage);
        }

        [TestMethod]
        public void Test_InvalidScores_NonNumericInput()
        {
            // Arrange
            string module1Score = "abc"; // Некорректный ввод (текст)
            string module2Score = "xyz";
            string module3Score = "123";

            // Act
            var result = _calculator.Calculate(
                int.TryParse(module1Score, out int m1) ? m1 : -1,
                int.TryParse(module2Score, out int m2) ? m2 : -1,
                int.TryParse(module3Score, out int m3) ? m3 : -1
            );

            // Assert
            Assert.AreEqual(0, result.TotalScore);
            Assert.AreEqual("", result.Grade);
            Assert.AreEqual("Ошибка: Баллы должны быть в допустимых пределах (Модуль 1: 0-22, Модуль 2: 0-38, Модуль 3: 0-20).", result.ErrorMessage);
        }
    }
}
