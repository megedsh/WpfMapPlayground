using System;
using System.Collections.ObjectModel;

namespace WpfMapPlayground;

public static class DataProvider
{
    public static ObservableCollection<TimedLineItem> Items { get; } = createItems();

    private static ObservableCollection<TimedLineItem> createItems() =>
        new()
        {
            new TimedLineItem
            {
                Name = "1[1111]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00Z"),
                        Value = LlaPosition.FromWktPoint("POINT(37.254638671875 31.010571059441716)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02Z"),
                        Value = LlaPosition.FromWktPoint("POINT(36.54052734375001 31.151707478133687)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04Z"),
                        Value = LlaPosition.FromWktPoint("POINT(35.99121093750001 31.25037814985572)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06Z"),
                        Value = LlaPosition.FromWktPoint("POINT(35.50781250000001 31.35832783341131)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08Z"),
                        Value = LlaPosition.FromWktPoint("POINT(35.24414062500001 31.419288124288357)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10Z"),
                        Value = LlaPosition.FromWktPoint("POINT(35.013427734375 31.48489338689015)")
                    }
                }
            },

            new TimedLineItem
            {
                Name = "2[2222]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.544 33.008)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.500 32.990)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.450 32.970)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.400 32.950)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.350 32.930)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.300 32.910)")
                    },
                }
            },

            new TimedLineItem
            {
                Name = "3[3333]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.800 31.000)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.750 30.950)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.700 30.900)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.650 30.850)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.600 30.800)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.550 30.750)")
                    },
                }
            },

            new TimedLineItem
            {
                Name = "4[4444]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.800 32.100)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.790 32.110)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.780 32.120)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.770 32.130)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.760 32.140)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.750 32.150)")
                    },
                }
            },

            new TimedLineItem
            {
                Name = "5[5555]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.200 31.800)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.190 31.810)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.180 31.820)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.170 31.830)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.160 31.840)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(35.150 31.850)")
                    },
                }
            },

            new TimedLineItem
            {
                Name = "6[6666]",
                Positions = new ObservableCollection<TimedLlaPosition>
                {
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:00+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.990 32.800)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:02+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.980 32.810)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:04+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.970 32.820)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:06+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.960 32.830)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:08+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.950 32.840)")
                    },
                    new TimedLlaPosition
                    {
                        Time = DateTime.Parse("2025-06-27T10:00:10+00:00"),
                        Value = LlaPosition.FromWktPoint("POINT(34.940 32.850)")
                    },
                }
            },
        };
}