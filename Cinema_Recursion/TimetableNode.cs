using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Recursion
{
    class TimetableNode
    {
        public List<Movie> MoviesToShow { get; private set; } //фильмы, из которых нужно составить расписание

        private List<Movie> _currentMovies;
        private int _workingTimeLeft;
        private List<TimetableNode> _next;

        public TimetableNode(int workingTimeLeft, List<Movie> moviesToShow, List<Movie> currentMovies = null)
        {
            _workingTimeLeft = workingTimeLeft;
            MoviesToShow = moviesToShow;
            if (currentMovies == null)
            {
                _currentMovies = new List<Movie>();
            }
            else
            {
                _currentMovies = currentMovies;
            }
            _next = new List<TimetableNode>();
        }

        public void CreateGraph()
        {
            foreach (Movie movie in MoviesToShow)
            {
                if (movie.Duration <= _workingTimeLeft)
                {
                    List<Movie> tmp = new List<Movie>();
                    foreach (Movie currentMovie in _currentMovies)
                    {
                        tmp.Add(currentMovie);
                    }
                    tmp.Add(movie);
                    TimetableNode node = new TimetableNode(_workingTimeLeft - movie.Duration, MoviesToShow, tmp);
                    _next.Add(node);
                    node.CreateGraph();
                }
            }
        }

        public List<ResultTimetable> GetAllTimetables(List<ResultTimetable> results = null)
        {
            if (results is null)
            {
                results = new List<ResultTimetable>();
            }
            if (_next.Count == 0)
            {
                results.Add(new ResultTimetable(_currentMovies, _workingTimeLeft));
            }
            else
            {
                foreach (TimetableNode next in _next)
                {
                    next.GetAllTimetables(results);
                }
            }
            return results;
        }

        public void Sort(List<ResultTimetable> results)
        {
            for (int i = 0; i < results.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (results[j]._workingTimeLeft < results[j - 1]._workingTimeLeft)
                    {
                        ResultTimetable temp = results[j - 1];
                        results[j - 1] = results[j];
                        results[j] = temp;
                    }
                }
            }
        }

        private bool CheckAllMoviesToShowIncluded(ResultTimetable crntTimetable)
        {
            int uniqueMoviesCount = 0;
            foreach(Movie movie in MoviesToShow)
            {
                if (crntTimetable._currentMovies.Contains(movie))
                {
                    uniqueMoviesCount++;
                }
            }
            if (uniqueMoviesCount == MoviesToShow.Count)
            {
                return true;
            }
            return false;
        }

        public List<ResultTimetable> GetOptimalTimetables()
        {
            List<ResultTimetable> results = GetAllTimetables();
            Sort(results);
            List<ResultTimetable> optimal = new List<ResultTimetable>();

            ResultTimetable tmp = results[0];
            foreach (ResultTimetable tmtbl in results)
            {
                if (CheckAllMoviesToShowIncluded(tmtbl))
                {
                    tmp = tmtbl;
                    break;
                }
            }
            foreach (ResultTimetable tmtbl in results)
            {
                if (tmtbl._workingTimeLeft <= tmp._workingTimeLeft && CheckAllMoviesToShowIncluded(tmtbl))
                {
                    optimal.Add(tmtbl);
                }
            }
            return optimal;
        }

       
    }
}
