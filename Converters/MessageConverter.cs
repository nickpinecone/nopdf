using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Robochat.Converters;

public class MessageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            Config.UserName => Brushes.LightBlue,
            _ => Brushes.White
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
