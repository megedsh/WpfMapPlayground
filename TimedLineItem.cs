using System;
using System.Collections.ObjectModel;

namespace WpfMapPlayground
{

    public class TimedLineItem
    {
        public string Name { get; set; }        
        public int PositionCount => Positions?.Count ?? 0;
        public ObservableCollection<TimedLlaPosition> Positions { get; init; }
    }

    public class TimedLlaPosition : Timed<LlaPosition>
    {
    }
    public class LlaPosition
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public double Altitude { get; init; }
        public static LlaPosition FromWktPoint(string wkt)
        {
            if (string.IsNullOrWhiteSpace(wkt) || !wkt.StartsWith("POINT(") || !wkt.EndsWith(")"))
            {
                throw new ArgumentException("Invalid WKT format");
            }

            string content = wkt.Substring(6, wkt.Length - 7); // Remove "POINT(" and ")"
            string[] parts = content.Split(' ');

            if (parts.Length != 2 ||
                !double.TryParse(parts[0], out double lon) ||
                !double.TryParse(parts[1], out double lat))
            {
                throw new FormatException("WKT point must contain two numeric values");
            }

            return new LlaPosition
            {
                Longitude = lon,
                Latitude = lat
            };
        }
    }
    public class Timed<T>
    {
        public DateTime Time { get; init; }
        public T Value { get; init; }
    }
}