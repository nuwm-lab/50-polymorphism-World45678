using System;

/// <summary>
/// ✅ Базовий клас, що представляє простий дріб виду 1 / (a*x).
/// </summary>
public class Fraction
{
    /// <summary>
    /// Точність для порівняння чисел з плаваючою комою.
    /// Доступна для похідних класів.
    /// </summary>
    protected const double Epsilon = 1e-12;

    private double _coefficientA;

    /// <summary>
    /// Коефіцієнт 'a' у дробі.
    /// Не може дорівнювати нулю.
    /// </summary>
    public double CoefficientA
    {
        get => _coefficientA;
        set
        {
            if (Math.Abs(value) < Epsilon)
            {
                // Замість попередження кидаємо виняток для суворої валідації
                throw new ArgumentException("Коефіцієнт 'a' не може дорівнювати нулю.", nameof(CoefficientA));
            }
            _coefficientA = value;
        }
    }

    /// <summary>
    /// Конструктор за замовчуванням.
    /// Ініціалізує коефіцієнт 'a' значенням 1.0.
    /// </summary>
    public Fraction()
    {
        _coefficientA = 1.0; // Значення за замовчуванням
    }

    /// <summary>
    /// Конструктор, що ініціалізує дріб заданим коефіцієнтом.
    /// </summary>
    /// <param name="coefficientA">Значення для коефіцієнта 'a'.</param>
    public Fraction(double coefficientA)
    {
        CoefficientA = coefficientA; // Використовуємо сетер для валідації
    }

    /// <summary>
    /// Віртуальний метод для встановлення коефіцієнтів через консоль.
    /// </summary>
    public virtual void SetCoefficients()
    {
        Console.WriteLine("--- Налаштування простого дробу ---");
        Console.Write("Введіть коефіцієнт 'a' для дробу виду 1/(a*x): ");

        double value;
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out value))
            {
                try
                {
                    CoefficientA = value; // Встановлюємо через властивість для валідації
                    break; // Успішно встановлено
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message} Спробуйте ще раз.");
                    Console.Write("Введіть коефіцієнт 'a': ");
                }
            }
            else
            {
                Console.WriteLine("Помилка. Будь ласка, введіть коректне число.");
                Console.Write("Введіть коефіцієнт 'a': ");
            }
        }
    }

    /// <summary>
    /// Віртуальний метод для відображення інформації про дріб.
    /// </summary>
    public virtual void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про простий дріб ---");
        Console.WriteLine("Тип: 'Простий дріб'");
        Console.WriteLine($"Формула: 1 / ({CoefficientA} * x)");
        Console.WriteLine($"Коефіцієнт a = {CoefficientA}");
    }

    /// <summary>
    /// Віртуальний метод для обчислення значення дробу 1/(a*x).
    /// </summary>
    /// <param name="x">Значення 'x'.</param>
    /// <returns>Результат обчислення.</returns>
    /// <exception cref="DivideByZeroException">Викидається, якщо знаменник (a*x) дорівнює нулю.</exception>
    public virtual double Calculate(double x)
    {
        double denominator = CoefficientA * x;

        if (Math.Abs(denominator) < Epsilon)
        {
            throw new DivideByZeroException("Знаменник (a*x) занадто малий або дорівнює нулю.");
        }
        return 1.0 / denominator;
    }
}


/// <summary>
/// ✅ Похідний клас, що представляє ланцюговий (підхідний) дріб
/// виду 1 / (a1*x + 1 / (a2*x + 1 / (a3*x))).
/// </summary>
public class ContinuedFraction : Fraction
{
    // Приватні поля
    private double _coefficientA1, _coefficientA2, _coefficientA3;

    /// <summary>
    /// Коефіцієнт 'a1'. Не може дорівнювати 3.
    /// </summary>
    public double CoefficientA1
    {
        get => _coefficientA1;
        set
        {
            if (Math.Abs(value - 3.0) < Epsilon)
                throw new ArgumentException("Коефіцієнт a1 не може дорівнювати 3.");
            _coefficientA1 = value;
        }
    }

    /// <summary>
    /// Коефіцієнт 'a2'. Не може дорівнювати 3.
    /// </summary>
    public double CoefficientA2
    {
        get => _coefficientA2;
        set
        {
            if (Math.Abs(value - 3.0) < Epsilon)
                throw new ArgumentException("Коефіцієнт a2 не може дорівнювати 3.");
            _coefficientA2 = value;
        }
    }

    /// <summary>
    /// Коефіцієнт 'a3'. Не може дорівнювати 3.
    /// </summary>
    public double CoefficientA3
    {
        get => _coefficientA3;
        set
        {
            if (Math.Abs(value - 3.0) < Epsilon)
                throw new ArgumentException("Коефіцієнт a3 не може дорівнювати 3.");
            _coefficientA3 = value;
        }
    }

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public ContinuedFraction() { }

    /// <summary>
    /// Конструктор, що ініціалізує ланцюговий дріб заданими коефіцієнтами.
    /// </summary>
    public ContinuedFraction(double a1, double a2, double a3)
    {
        // Використовуємо сетери для валідації
        CoefficientA1 = a1;
        CoefficientA2 = a2;
        CoefficientA3 = a3;
    }
    
    /// <summary>
    /// Допоміжний метод для зчитування та валідації коефіцієнта з консолі.
    /// </summary>
    private double ReadCoefficient(string name)
    {
        double value;
        while (true)
        {
            Console.Write($"Введіть коефіцієнт '{name}' (не може дорівнювати 3): ");
            if (double.TryParse(Console.ReadLine(), out value))
            {
                if (Math.Abs(value - 3.0) < Epsilon)
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

    /// <summary>
    /// Перевизначений метод для встановлення коефіцієнтів ланцюгового дробу.
    /// </summary>
    public override void SetCoefficients()
    {
        Console.WriteLine("\n--- Налаштування ланцюгового дробу ---");
        // Ми не можемо використовувати сетери напряму тут, бо ReadCoefficient вже
        // містить валідацію. Якби валідація була тільки в сетерах,
        // ми б обгортали `CoefficientA1 = double.Parse(...)` в try-catch.
        CoefficientA1 = ReadCoefficient("a1");
        CoefficientA2 = ReadCoefficient("a2");
        CoefficientA3 = ReadCoefficient("a3");
    }

    /// <summary>
    /// Перевизначений метод для відображення інформації про ланцюговий дріб.
    /// </summary>
    public override void DisplayCoefficients()
    {
        Console.WriteLine("\n--- Інформація про ланцюговий дріб ---");
        Console.WriteLine("Тип: 'Ланцюговий дріб'");
        Console.WriteLine("Формула: 1 / (a1*x + 1 / (a2*x + 1 / (a3*x)))");
        Console.WriteLine($"Коефіцієнти: a1 = {CoefficientA1}, a2 = {CoefficientA2}, a3 = {CoefficientA3}");
    }

    /// <summary>
    /// Перевизначений метод для обчислення значення ланцюгового дробу.
    /// </summary>
    /// <param name="x">Значення 'x'.</param>
    /// <returns>Результат обчислення.</returns>
    /// <exception cref="DivideByZeroException">Викидається, якщо будь-який із знаменників дорівнює нулю.</exception>
    public override double Calculate(double x)
    {
        double innerDenominator = CoefficientA3 * x;
        if (Math.Abs(innerDenominator) < Epsilon)
            throw new DivideByZeroException("Внутрішній знаменник (a3*x) занадто малий або дорівнює нулю.");

        double middleDenominator = CoefficientA2 * x + (1.0 / innerDenominator);
        if (Math.Abs(middleDenominator) < Epsilon)
            throw new DivideByZeroException("Середній знаменник занадто малий або дорівнює нулю.");

        double outerDenominator = CoefficientA1 * x + (1.0 / middleDenominator);
        if (Math.Abs(outerDenominator) < Epsilon)
            throw new DivideByZeroException("Зовнішній знаменник занадто малий або дорівнює нулю.");

        return 1.0 / outerDenominator;
    }
}


/// <summary>
/// ✅ Головна програма (ОНОВЛЕНО ДЛЯ ДЕМОНСТРАЦІЇ ПОЛІМОРФІЗМУ)
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        while (true) // Головний цикл програми, щоб можна було тестувати
        {
            Console.WriteLine("\n--- Калькулятор Дробів ---");
            Console.WriteLine("Який дріб ви хочете створити?");
            Console.WriteLine("1. Простий дріб (1 / (a*x))");
            Console.WriteLine("2. Ланцюговий дріб"); // Оновлена назва
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
                // ...АБО об'єкт похідного класу (з новою назвою)
                myFraction = new ContinuedFraction();
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
                // C# сам вирішить, яку версію методу (з Fraction чи ContinuedFraction)
                // викликати, базуючись на реальному типі об'єкта, який в ній лежить.

                myFraction.SetCoefficients();   // Викличе або Fraction.SetCoefficients, або ContinuedFraction.SetCoefficients
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

