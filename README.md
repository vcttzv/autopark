# Информационная система «Автопарк»

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-0078D6?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)
[![Tests](https://img.shields.io/badge/Tests-6%20passed%2C%200%20failed-brightgreen)](docs/screenshots/test_results.png)
![License](https://img.shields.io/badge/License-MIT-blue.svg)

## Содержание

- [О проекте](#о-проекте)
- [Методология разработки](#методология-разработки)
- [Функциональность](#функциональность)
- [Технологический стек](#технологический-стек)
- [Архитектура и проектирование](#архитектура-и-проектирование)
- [Интерфейс программы](#интерфейс-программы)
- [Тестирование](#тестирование)

---

## О проекте

**Информационная система «Автопарк»** — это десктопное приложение для автоматизации учёта и управления автопарком организации. Разработано в рамках курсовой работы для колледжа (ЧУПО "ТЭТК") с целью демонстрации навыков проектирования и разработки информационных систем.

### Ключевые задачи

| Задача | Описание |
|--------|----------|
| Учёт ТС | Хранение информации о транспортных средствах, их техническом состоянии и статусе |
| Техобслуживание | Планирование и ведение истории технического обслуживания и ремонтов |
| Клиенты и сотрудники | Ведение базы клиентов и сотрудников автопарка |
| Аренда | Оформление и учёт операций аренды транспортных средств |
| Отчётность | Формирование отчётов по эксплуатации и ремонтам |

Проект включает **полный цикл разработки**: от составления технического задания и моделирования бизнес-процессов до реализации, тестирования и документирования.

---

## Методология разработки

| Этап | Инструменты | Результат |
|------|-------------|-----------|
| 1. Анализ требований | RAMUS (IDEF0) | Функциональные модели бизнес-процессов |
| 2. Проектирование | StarUML (UML) | Диаграммы классов, прецедентов, последовательности |
| 3. База данных | MySQL Workbench | ER-диаграмма, SQL-скрипты, триггеры |
| 4. Разработка | Visual Studio (C# + WPF) | Десктопное приложение |
| 5. Тестирование | MSTest | Модульные тесты (Unit Tests) |
| 6. Документация | MS Word | ТЗ, пояснительная записка, руководство пользователя |

---

## Функциональность

### Пользовательские роли

- **Сотрудник автопарка** — просмотр, добавление, редактирование и удаление данных об автомобилях, клиентах и операциях, оформление аренды.

### Основные модули

| Модуль | Возможности |
|--------|-------------|
| Авторизация и регистрация | Безопасный вход и создание учётных записей сотрудников |
| Управление ТС | Добавление автомобилей (марка, госномер, тип, статус, стоимость), фильтрация по статусу, типу и автопарку |
| Управление арендой | Выбор клиента и ТС, установка дат аренды, расчёт стоимости |
| Клиентская база | Хранение персональных данных, паспортной информации и контактов |
| Управление автопарками | Ведение базы мест хранения с указанием вместимости и адресов |

---

## Технологический стек

| Компонент | Технология |
|-----------|------------|
| **Язык программирования** | C# (.NET Framework) |
| **UI-фреймворк** | Windows Presentation Foundation (WPF) + XAML |
| **Архитектура** | Паттерн MVVM (реализован через код-бэхайнд) |
| **База данных** | MySQL Community Edition |
| **Доступ к данным** | ADO.NET + `MySql.Data.MySqlClient` (ручное написание SQL) |
| **IDE** | Microsoft Visual Studio 2022 |
| **СУБД** | MySQL Workbench |
| **Моделирование** | StarUML, RAMUS |

---

## Архитектура и проектирование

Проект начинался с анализа и проектирования. Функциональная модель предметной области разрабатывалась на основе методологии **IDEF0**.

<details>
<summary><b>Диаграммы IDEF0 (нажмите, чтобы развернуть)</b></summary>

### Контекстная диаграмма
Взаимодействие системы с внешним миром.

![Контекстная диаграмма](docs/diagram/IDEF0/01_A-0.png)

### Детализация первого уровня

![Детализация A0](docs/diagram/IDEF0/02_A0.png)

### Детализация процессов

| Процесс | Диаграмма |
|---------|-----------|
| Регистрация ТС | ![A1](docs/diagram/IDEF0/03_A1.png) |
| Учёт ТС | ![A2](docs/diagram/IDEF0/04_A2.png) |
| Аренда ТС | ![A3](docs/diagram/IDEF0/05_A3.png) |
| Формирование отчётов | ![A4](docs/diagram/IDEF0/06_A4.png) |

</details>

<details>
<summary><b>UML-диаграммы (нажмите, чтобы развернуть)</b></summary>

### Диаграмма прецедентов (Use Case)

| До автоматизации | После автоматизации |
|------------------|---------------------|
| ![Use Case до](docs/diagram/UML/UseCaseDiagram1.png) | ![Use Case после](docs/diagram/UML/UseCaseDiagram2.png) |

### Диаграмма классов

![Диаграмма классов](docs/diagram/UML/ClassDiagram1.png)

### Диаграмма последовательности (Sequence)

| До автоматизации | После автоматизации |
|------------------|---------------------|
| ![Sequence до](docs/diagram/UML/SequenceDiagram1.png) | ![Sequence после](docs/diagram/UML/SequenceDiagram2.png) |

</details>

*Полный набор диаграмм (Activity, State, Component, Deployment) доступен в папке [`docs/diagram/`](docs/diagram/).*

---

## Интерфейс программы

### Авторизация и регистрация

| Авторизация | Регистрация |
|-------------|-------------|
| ![Авторизация](docs/screenshots/auth_window.png) | ![Регистрация](docs/screenshots/reg.png) |

---

### Панель управления

Главное окно для навигации по разделам системы.

| Панель управления | Выбор таблиц БД |
|-------------------|-----------------|
| ![Главное меню](docs/screenshots/main.png) | ![Выбор таблиц](docs/screenshots/tables.png) |

---

### Регистрация новых записей

Формы для добавления транспортных средств, клиентов и операций аренды.

| Регистрация ТС | Регистрация клиента | Регистрация операции |
|----------------|---------------------|----------------------|
| ![Регистрация ТС](docs/screenshots/reg_transport.png) | ![Регистрация клиента](docs/screenshots/reg_client.png) | ![Регистрация операции](docs/screenshots/reg_operation.png) |

---

## Тестирование

В проекте реализованы **модульные тесты (Unit Tests)** для проверки ключевой логики приложения. Тесты написаны с использованием фреймворка **MSTest**.

### Покрытие тестами

- ✅ Валидация пароля (длина, заглавные/строчные буквы, цифры, спецсимволы)
- ✅ Проверка корректности вводимых данных
- ✅ Граничные случаи и обработка ошибок

### Результаты тестов

Все 6 тестов успешно пройдены ✅

![Результаты тестирования](docs/screenshots/test_results.png)

### Пример тестового метода

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibraryMetrology.UnitTestProjectMetrology
{
    [TestClass()]
    public class PasswordCheckerTests
    {
        [TestMethod()]
        public void CheckSymbols()
        {
            // Arrange
            string password = "abcD#$";
            bool expected = true;

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckLength_ShortPassword_ReturnsFalse()
        {
            // Arrange
            string password = "aD45";

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void CheckLowerSymbols()
        {
            // Arrange
            string password = "ABCD3F!$";
            bool expected = true;

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckNoLowerSymbols_ReturnsFalse()
        {
            // Arrange
            string password = "ABCD3F!$";
            bool expected = false;

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckDigits_ReturnsTrue()
        {
            // Arrange
            string password = "ABcD3F!$";
            bool expected = true;

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckSpecialChars_ReturnsTrue()
        {
            // Arrange
            string password = "ABcD3F!$";
            bool expected = true;

            // Act
            bool actual = PasswordChecker.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
