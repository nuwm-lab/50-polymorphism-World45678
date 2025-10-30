using System;

// ✅ Клас простого дробу
// (Ваш код класу Fraction залишається без змін,
// оскільки він вже ідеально налаштований для поліморфізму)
public class Fraction
{
    private double _a;

    public double A
    {
        get => _a;
        set
        {
            if (Math.Abs(value) < 1e-12)
                Console.WriteLine("⚠️ Попередження: коефіцієнт 'a' дуже малий, можливі помилки при діленні.");
            _a = value;
        }
    }

    public Fraction()
    {
        _a = 1.0;
    }

    public virtual void SetCoefficients()
    {
        Console.WriteLine("--- Налаштування простого дробу ---");
        Console.Write("Введіть коефіцієнт 'a' для дробу виду 1/(a*x): ");

        double value;
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
            Console.Write("Введіть коефіцієнт 'a': ");
        }
        A = value;
    }

    public virtual void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про простий дріб ---");
        Console.WriteLine("Тип: 'дріб'");
        Console.WriteLine($"Формула: 1 / ({_a} * x)");
        Console.WriteLine($"Коефіцієнт a = {_a}");
    }

    public virtual double Calculate(double x)
    {
        double denominator = _a * x;
        const double eps = 1e-12;
        if (Math.Abs(denominator) < eps)
        {
            throw new DivideByZeroException("Знаменник (a*x) занадто малий або дорівнює нулю.");
        }
        return 1.0 / denominator;
    }

    public void SetCoefficients(double a) => A = a;
}


// ✅ Клас складного (тригонометричного) дробу
// (Ваш код класу ComplexFraction також залишається без змін)
public class ComplexFraction : Fraction
{
    private double _a1, _a2, _a3;

    public double A1
    {
        get => _a1;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a1 не може дорівнювати 3.");
            _a1 = value;
        }
    }
    public double A2
    {
        get => _a2;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a2 не може дорівнювати 3.");
            _a2 = value;
        }
    }
    public double A3
    {
        get => _a3;
        set
        {
            if (Math.Abs(value - 3.0) < 1e-12)
                throw new ArgumentException("Коефіцієнт a3 не може дорівнювати 3.");
            _a3 = value;
        }
    }

    private double ReadCoefficient(string name)
    {
        double value;
        while (true)
        {
            Console.Write($"Введіть коефіцієнт '{name}' (не може дорівнювати 3): ");
            if (double.TryParse(Console.ReadLine(), out value))
            {
                if (Math.Abs(value - 3.0) < 1e-12)
                {
                    Console.WriteLine("Помилка: коефіцієнт не може дорівнювати 3. Спробуйте ще раз.");
                }
                else
                {
                    return value;
                }
            }
            else
            {
                Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
            }
        }
    }

    public override void SetCoefficients()
    {
        Console.WriteLine("\n--- Налаштування тригонометричного підхідного дробу ---");
        A1 = ReadCoefficient("a1");
        A2 = ReadCoefficient("a2");
        A3 = ReadCoefficient("a3");
  }

    public override void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про тригонометричний підхідний дріб ---");
        Console.WriteLine("Тип: 'тригонометричний підхідний дріб'");
        Console.WriteLine("Формула: 1 / (a1*x + 1 / (a2*x + 1 / (a3*x)))");
        Console.WriteLine($"Коефіцієнти: a1 = {A1}, a2 = {A2}, a3 = {A3}");
    }

    public override double Calculate(double x)
    {
        const double eps = 1e-12;

        double innerDenominator = A3 * x;
        if (Math.Abs(innerDenominator) < eps)
            throw new DivideByZeroException("Внутрішній знаменник (a3*x) занадто малий або дорівнює нулю.");

        double middleDenominator = A2 * x + (1.0 / innerDenominator);
        if (Math.Abs(middleDenominator) < eps)
            throw new DivideByZeroException("Середній знаменник занадто малий або дорівнює нулю.");

        double outerDenominator = A1 * x + (1.0 / middleDenominator);
        if (Math.Abs(outerDenominator) < eps)
            throw new DivideByZeroException("Зовнішній знаменник занадто малий або дорівнює нулю.");

        return 1.0 / outerDenominator;
    }

    public void SetCoefficients(double a1, double a2, double a3)
    {
        A1 = a1;
        A2 = a2;
        A3 = a3;
    }
}


// ✅ Головна програма (ОНОВЛЕНО ДЛЯ ДЕМОНСТРАЦІЇ ПОЛІМОРФІЗМУ)
public class Program
{
    public static void Main(string[] args)
    {
        while (true) // Головний цикл програми, щоб можна було тестувати
        {
            Console.WriteLine("\n--- Калькулятор Дробів ---");
            Console.WriteLine("Який дріб ви хочете створити?");
            Console.WriteLine("1. Простий дріб (1 / (a*x))");
            Console.WriteLine("2. Складний (тригонометричний) дріб");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break; // Вихід з циклу
            }

            // !!! ОСЬ ТУТ ПОЧАТОК ПОЛІМОРФІЗМУ !!!
            // Ми оголошуємо ОДНУ змінну базового типу Fraction
            Fraction myFraction;

            if (choice == "1")
            {
                // Присвоюємо їй об'єкт базового класу
                myFraction = new Fraction();
            }
            else if (choice == "2")
            {
                // ...АБО об'єкт похідного класу
                myFraction = new ComplexFraction();
            }
            else
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                continue; // Повертаємось на початок циклу
            }

            try
            {
                // --- ПОЛІМОРФІЗМ В ДІЇ ---
                // Ми викликаємо методи на змінній `myFraction`.
                // C# сам вирішить, яку версію методу (з Fraction чи ComplexFraction)
                // викликати, базуючись на реальному типі об'єкта, який в ній лежить.

                myFraction.SetCoefficients();  // Викличе або Fraction.SetCoefficients, або ComplexFraction.SetCoefficients
                myFraction.DisplayCoefficients(); // Викличе відповідний DisplayCoefficients

                // --- Загальна частина для обчислення ---
                Console.WriteLine("\n-------------------------------------------");
                Console.Write("Введіть значення 'x' для обчислення дробу: ");
                double x;
                while (!double.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine("Помилка. Будь ласка, введіть число для 'x'.");
                    Console.Write("Введіть значення 'x': ");
                }

                // Знову поліморфний виклик!
                double result = myFraction.Calculate(x);
                Console.WriteLine($"\n✅ Результат для обраного дробу при x = {x}: {result:F4}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"\n❌ Помилка обчислення: {ex.Message}");
            }
            catch (ArgumentException ex) // Обробка помилок з коефіцієнтами
            {
                Console.WriteLine($"\n❌ Помилка вводу: {ex.Message}");
            }
            catch (Exception ex) // Загальний обробник
            {
                Console.WriteLine($"\n❌ Сталася неочікувана помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
            Console.ReadLine();
        }
    }
}
