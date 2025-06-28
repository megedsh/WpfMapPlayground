using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace WpfMapPlayground.Views
{
    public class ColorRoundRobin
    {
        private readonly List<Color> m_colors;
        private          int         m_currentIndex;

        public ColorRoundRobin()
        {
            m_colors = new List<Color>()
            {
                Colors.Red,
                Colors.Green,
                Colors.Blue,
                Colors.Yellow,
                Colors.Orange,
                Colors.Purple,
                Colors.Cyan,
                Colors.Magenta,
                Colors.Brown,
                Colors.Gray
            };
        }

        public Color GetNextColor()
        {
            Color color = m_colors[m_currentIndex];
            m_currentIndex = (m_currentIndex + 1) % m_colors.Count;
            return color;
        }
    }
}