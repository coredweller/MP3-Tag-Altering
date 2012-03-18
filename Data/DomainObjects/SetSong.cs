using System.Collections.Generic;
using System.Text;
using System;

namespace DomainObjects
{
    public partial class SetSong : ISetSong
    {

        public string GetSongName(double? length, DateTime? showDate, string city, string state)
        {
            if (length == null && showDate == null && city == null && state == null)
                return string.Empty;

            StringBuilder s = new StringBuilder();
            string showDateStr = showDate.Value.ToString("MM/dd/yyyy");

            if (length != null)
                s.Append(length.Value);

            if (showDate != null)
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(showDateStr);
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(city);

                if (!string.IsNullOrEmpty(state))
                {
                    s.Append(", " + state);
                }
            }

            return s.ToString();
        }
    }
}
