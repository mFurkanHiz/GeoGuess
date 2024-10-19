using GeoGuess.Service.Interfaces;

namespace GeoGuess.Service.Services;

public class DateTimeHandlerService : IDateTimeHandlerService
{

    public DateTime StringToDateTime(string stringDate)
    {

        // Expected String Date Format: "dd/MM/yyyy HH:mm"

        try
        {
            if (string.IsNullOrEmpty(stringDate)) return DateTime.MinValue;

            stringDate = stringDate.Trim();

            if (stringDate.Length is not 16) return DateTime.MinValue;

            var charDateSpliter = '/';

            if (!stringDate.Contains(charDateSpliter))
            {

                charDateSpliter = '.';

                if (!stringDate.Contains(charDateSpliter))
                    throw new Exception("Wrong date format. split the date values by '/' or '.'");
            }

            string[] mainParts = stringDate.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] dateParts = mainParts[0].Split(charDateSpliter, StringSplitOptions.RemoveEmptyEntries);
            string[] timeParts = mainParts[1].Split(':', StringSplitOptions.RemoveEmptyEntries);

            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            DateTime formattedDate = new(year, month, day, hour, minute, 0);

            return formattedDate;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error message: {ex.Message}");
        }
    }



}