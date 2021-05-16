using System;
using System.Collections.Generic;

namespace Cinema_Recursion
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Movie> MoviesToShow = CreateMovieList();
            int minutesInWorkingDay = 840;
            bool isFinished = false;

            while (!isFinished)
            {
                TimetableNode timetable = new TimetableNode(minutesInWorkingDay, MoviesToShow);
                timetable.CreateGraph();
                List<ResultTimetable> allTimetables = timetable.GetOptimalTimetables();
                int count = 1;
                foreach (ResultTimetable tmtbl in allTimetables)
                {
                    Console.WriteLine($"Вариант {count}:");
                    Console.WriteLine(tmtbl.ToString());
                    count++;
                    if (count <= allTimetables.Count)
                    {
                        Console.WriteLine("Показать другой вариант? (+ / -)");
                        string choice = Console.ReadLine();
                        if (choice == "+")
                        {
                            continue;
                        }
                        else
                        {
                            isFinished = true;
                            break;
                        }
                    }
                    else
                    {
                        isFinished = true;
                        break;
                    }
                }
                if (allTimetables.Count == 0)
                {
                    Console.WriteLine("Прокатное время не вмещает все фильмы. Удалить что-нибудь из списка? (+ / -)");
                    string choice = Console.ReadLine();
                    if (choice == "+")
                    {
                        Console.WriteLine("Укажите фильм для удаления из списка:");
                        string movieToRemove = Console.ReadLine();
                        foreach (Movie m in MoviesToShow)
                        {
                            if(m.Name.Equals(movieToRemove))
                            {
                                Movie movie = m;
                                MoviesToShow.Remove(movie);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось составить расписание");
                        isFinished = true;
                    }
                }
            }
            
        }


        public static List<Movie> CreateMovieList()
        {
            List<Movie> MoviesToShow = new List<Movie>();
            Console.WriteLine("Введите количество фильмов:");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Введите название фильма:");
                string movieName = Console.ReadLine();
                Console.WriteLine("Введите продолжительность фильма в минутах:");
                
                int movieDuration;
                bool isCorrectInput = false;
                while (!(isCorrectInput))
                {
                    string result = Console.ReadLine();
                    isCorrectInput = int.TryParse(result, out movieDuration);
                    if (isCorrectInput)
                    {
                        Movie movie = new Movie(movieName, movieDuration);
                        MoviesToShow.Add(movie);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод, попробуйте еще раз:");
                    }
                }
            }
            return MoviesToShow;
        }

        static void CreateBookList(int number, List<string> books, List<string> current) 
        {
            if (current.Count == number)
            {
                foreach (string s in current)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
            }

            else
            {
                foreach (string s in books)
                {
                    List<string> tmp = new List<string>();
                    foreach (string q in current)
                    {
                        tmp.Add(q);
                    }
                    tmp.Add(s);
                    CreateBookList(number, books, tmp);
                }
            }
        }
    }
}
