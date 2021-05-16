using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Recursion
{
    class ResultTimetable
    {
        public List<Movie> _currentMovies;
        public int _workingTimeLeft;

        public ResultTimetable(List<Movie> currentMovies, int workingTimeLeft)
        {
            _currentMovies = currentMovies;
            _workingTimeLeft = workingTimeLeft;
        }

        public override string ToString()
        {
            DateTime start = DateTime.Today;
            int startOfWorkingDayInMinutes = 600; //10.00 - 00.00 => 10 *60 = 600
            start = start.AddMinutes(startOfWorkingDayInMinutes);
            DateTime end = start;
            string resultTimetableString = "";
            foreach (Movie movie in _currentMovies)
            {
                end = end.AddMinutes(movie.Duration);
                resultTimetableString += $"{start.ToShortTimeString()} - {end.ToShortTimeString()} {movie.Name}\n";
                start = end;
            }
            return resultTimetableString;
        }
    }
}
