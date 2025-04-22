using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Robochat.Converters;

public class DateConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return "";
        }

        var dateTime = (DateTime)value;

        if (dateTime.Date == DateTime.UtcNow.Date)
        {
            return dateTime.ToString("HH:mm");
        }
        else
        {
            return dateTime.ToString("dd/MM/yy");
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
