using System;
using System.Collections.Generic;

namespace ExamSystem
{
    public abstract class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public AnswerList Answers { get; set; } = new AnswerList();

        public abstract void Display();

        public abstract bool CheckAnswer(string input);

        public string GetCorrectAnswers()
        {
            List<string> correct = new();
            for (int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i].IsCorrect)
                    correct.Add($"{(char)('A' + i)}. {Answers[i].Text}");
            }
            return string.Join(" | ", correct);
        }
    }

    public class TrueFalseQuestion : Question
    {
        public override void Display()
        {
            Console.WriteLine($"{Header}\n{Body}\nA. True\nB. False");
        }

        public override bool CheckAnswer(string input)
        {
            input = input.ToUpper().Trim();
            return (input == "A" && Answers[0].IsCorrect) || (input == "B" && Answers[1].IsCorrect);
        }
    }

    public class ChooseOneQuestion : Question
    {
        public override void Display()
        {
            Console.WriteLine($"{Header}\n{Body}");
            Answers.Display();
        }

        public override bool CheckAnswer(string input)
        {
            input = input.ToUpper().Trim();
            int index = input[0] - 'A';
            return index >= 0 && index < Answers.Count && Answers[index].IsCorrect;
        }
    }

    public class ChooseAllQuestion : Question
    {
        public override void Display()
        {
            Console.WriteLine($"{Header}\n{Body}");
            Answers.Display();
            Console.WriteLine("Select all correct answers (e.g., A,C):");
        }

        public override bool CheckAnswer(string input)
        {
            var correctIndexes = new HashSet<int>();
            for (int i = 0; i < Answers.Count; i++)
                if (Answers[i].IsCorrect)
                    correctIndexes.Add(i);

            var selected = input.ToUpper().Replace(" ", "").Split(',');
            var userIndexes = new HashSet<int>();

            foreach (var s in selected)
                if (!string.IsNullOrEmpty(s) && s[0] >= 'A' && s[0] <= 'Z')
                    userIndexes.Add(s[0] - 'A');

            return correctIndexes.SetEquals(userIndexes);
        }
    }

    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class AnswerList : List<Answer>
    {
        public void Display()
        {
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"{(char)('A' + i)}. {this[i].Text}");
            }
        }
    }

    public class Subject
    {
        public string Name { get; set; }
        public event Action<string> OnExamStarted;

        public void StartExam()
        {
            OnExamStarted?.Invoke($"Exam for subject '{Name}' is starting!");
        }
    }

    public enum ExamMode { Starting, Queued, Finished }

    public abstract class Exam
    {
        public TimeSpan Duration { get; set; }
        public Dictionary<Question, AnswerList> QuestionAnswers { get; set; } = new();
        public Subject Subject { get; set; }
        public ExamMode Mode { get; set; }

        public abstract void Show();
    }

    public class PracticeExam : Exam
    {
        public override void Show()
        {
            int score = 0;

            foreach (var pair in QuestionAnswers)
            {
                var q = pair.Key;
                q.Display();
                Console.Write("Your answer: ");
                var input = Console.ReadLine();

                if (q.CheckAnswer(input))
                {
                    Console.WriteLine("✅ Correct!\n");
                    score += q.Marks;
                }
                else
                {
                    Console.WriteLine($"❌ Incorrect. Correct answer(s): {q.GetCorrectAnswers()}\n");
                }
            }

            Console.WriteLine($"Your final score: {score} out of {TotalMarks()}");
        }

        private int TotalMarks()
        {
            int total = 0;
            foreach (var q in QuestionAnswers.Keys)
                total += q.Marks;
            return total;
        }
    }

    public class FinalExam : Exam
    {
        public override void Show()
        {
            foreach (var pair in QuestionAnswers)
            {
                pair.Key.Display();
                Console.WriteLine(); // just display, no answer checking
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var subject = new Subject { Name = "Math" };
            subject.OnExamStarted += Console.WriteLine;

            var q1 = new TrueFalseQuestion
            {
                Header = "Q1",
                Body = "Is 2 + 2 = 4?",
                Marks = 1
            };
            q1.Answers.Add(new Answer { Text = "True", IsCorrect = true });
            q1.Answers.Add(new Answer { Text = "False", IsCorrect = false });

            var q2 = new ChooseOneQuestion
            {
                Header = "Q2",
                Body = "Capital of France?",
                Marks = 2
            };
            q2.Answers.Add(new Answer { Text = "Paris", IsCorrect = true });
            q2.Answers.Add(new Answer { Text = "Berlin", IsCorrect = false });
            q2.Answers.Add(new Answer { Text = "Madrid", IsCorrect = false });

            var q3 = new ChooseAllQuestion
            {
                Header = "Q3",
                Body = "Select even numbers:",
                Marks = 3
            };
            q3.Answers.Add(new Answer { Text = "1", IsCorrect = false });
            q3.Answers.Add(new Answer { Text = "2", IsCorrect = true });
            q3.Answers.Add(new Answer { Text = "3", IsCorrect = false });
            q3.Answers.Add(new Answer { Text = "4", IsCorrect = true });

            var practice = new PracticeExam { Subject = subject, Duration = TimeSpan.FromMinutes(15), Mode = ExamMode.Starting };
            practice.QuestionAnswers.Add(q1, q1.Answers);
            practice.QuestionAnswers.Add(q2, q2.Answers);
            practice.QuestionAnswers.Add(q3, q3.Answers);

            var final = new FinalExam { Subject = subject, Duration = TimeSpan.FromMinutes(10), Mode = ExamMode.Queued };
            final.QuestionAnswers.Add(q1, q1.Answers);
            final.QuestionAnswers.Add(q2, q2.Answers);
            final.QuestionAnswers.Add(q3, q3.Answers);

            Console.WriteLine("Select exam type:\n1. Practice (shows result)\n2. Final (no answers shown)");
            string input = Console.ReadLine();

            Exam selected = input == "1" ? practice : final;

            if (selected.Mode == ExamMode.Starting)
                subject.StartExam();

            selected.Show();
        }
    }
}
