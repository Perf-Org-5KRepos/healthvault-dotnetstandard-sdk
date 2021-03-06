// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using Microsoft.HealthVault.Helpers;
using NodaTime;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// Represents a HealthVault date.
    /// </summary>
    ///
    /// <remarks>
    /// A HealthVault date pertains to dates only, not times. The year, month, and day
    /// must be specified.
    /// </remarks>
    ///
    public class HealthServiceDate
        : ItemBase,
            IComparable,
            IComparable<HealthServiceDate>,
            IComparable<LocalDate>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="HealthServiceDate"/> class
        /// with values defaulting to the current year, month, and day.
        /// </summary>
        ///
        public HealthServiceDate()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="HealthServiceDate"/> class
        /// with the specified year, month, and day.
        /// </summary>
        ///
        /// <param name="year">
        /// The year between 1000 and 9999.
        /// </param>
        ///
        /// <param name="month">
        /// The month between 1 and 12.
        /// </param>
        ///
        /// <param name="day">
        /// The day between 1 and 31.
        /// </param>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="year"/> parameter is less than 1000 or greater than 9999,
        /// or the <paramref name="month"/> parameter is less than 1 or greater than 12,
        /// or the <paramref name="day"/> parameter is less than 1 or greater than 31.
        /// </exception>
        ///
        public HealthServiceDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Populates the data for the date from the XML.
        /// </summary>
        ///
        /// <param name="navigator">
        /// The XML node representing the date.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="navigator"/> parameter is <b>null</b>.
        /// </exception>
        ///
        public override void ParseXml(XPathNavigator navigator)
        {
            Validator.ThrowIfNavigatorNull(navigator);

            XPathNavigator yearNav = navigator.SelectSingleNode("y");
            if (yearNav != null)
            {
                _year = yearNav.ValueAsInt;
            }

            XPathNavigator monthNav = navigator.SelectSingleNode("m");
            if (monthNav != null)
            {
                _month = monthNav.ValueAsInt;
            }

            XPathNavigator dayNav = navigator.SelectSingleNode("d");
            if (dayNav != null)
            {
                _day = dayNav.ValueAsInt;
            }
        }

        /// <summary>
        /// Writes the date to the specified XML writer.
        /// </summary>
        ///
        /// <param name="nodeName">
        /// The name of the outer element for the date.
        /// </param>
        ///
        /// <param name="writer">
        /// The XmlWriter to write the date to.
        /// </param>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="nodeName"/> parameter is <b>null</b> or empty.
        /// </exception>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="writer"/> parameter is <b>null</b>.
        /// </exception>
        ///
        public override void WriteXml(string nodeName, XmlWriter writer)
        {
            Validator.ThrowIfStringNullOrEmpty(nodeName, "nodeName");
            Validator.ThrowIfWriterNull(writer);

            writer.WriteStartElement(nodeName);

            writer.WriteElementString(
                "y",
                _year.ToString(CultureInfo.InvariantCulture));

            writer.WriteElementString(
                "m",
                _month.ToString(CultureInfo.InvariantCulture));

            writer.WriteElementString(
                "d",
                _day.ToString(CultureInfo.InvariantCulture));

            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets or sets the year of the date.
        /// </summary>
        ///
        /// <returns>
        /// An integer representing the year.
        /// </returns>
        ///
        /// <remarks>
        /// This value defaults to the current year.
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than 1000 or greater than 9999.
        /// </exception>
        ///
        public int Year
        {
            get { return _year; }

            set
            {
                if (value < 1000 || value > 9999)
                {
                    throw new ArgumentOutOfRangeException(nameof(Year), Resources.DateYearOutOfRange);
                }

                _year = value;
            }
        }

        private int _year = DateTime.Now.Year;

        /// <summary>
        /// Gets or sets the month of the date.
        /// </summary>
        ///
        /// <returns>
        /// An integer representing the month.
        /// </returns>
        ///
        /// <remarks>
        /// This value defaults to the current month.
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than 1 or greater than 12.
        /// </exception>
        ///
        public int Month
        {
            get { return _month; }

            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentOutOfRangeException(nameof(Month), Resources.DateMonthOutOfRange);
                }

                _month = value;
            }
        }

        private int _month = DateTime.Now.Month;

        /// <summary>
        /// Gets or sets the day of the date.
        /// </summary>
        ///
        /// <returns>
        /// An integer representing the day.
        /// </returns>
        ///
        /// <remarks>
        /// This value defaults to the current day.
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than 1 or greater than 31.
        /// </exception>
        ///
        public int Day
        {
            get { return _day; }

            set
            {
                if (value < 1 || value > 31)
                {
                    throw new ArgumentOutOfRangeException(nameof(Day), Resources.DateDayOutOfRange);
                }

                _day = value;
            }
        }

        private int _day = DateTime.Now.Day;

        #region IComparable

        /// <summary>
        /// Compares the specified object to this HealthServiceDate object.
        /// </summary>
        ///
        /// <param name="obj">
        /// The object to be compared.
        /// </param>
        ///
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the
        /// objects being compared. If the result is less than zero, the
        /// instance is less than <paramref name="obj"/>. If the result is zero
        /// the instance is equal to <paramref name="obj"/>. If the result is
        /// greater than zero, the instance is greater than
        /// <paramref name="obj"/>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="obj"/> parameter is not a <see cref="HealthServiceDate"/>
        /// or <see cref="LocalDate"/> object.
        /// </exception>
        ///
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            HealthServiceDate hsDate = obj as HealthServiceDate;
            if (hsDate == null)
            {
                try
                {
                    LocalDate date = (LocalDate)obj;
                    return CompareTo(date);
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentException(Resources.DateCompareToInvalidType, nameof(obj));
                }
            }

            return CompareTo(hsDate);
        }

        /// <summary>
        /// Compares the specified object to this HealthServiceDate object.
        /// </summary>
        ///
        /// <param name="other">
        /// The date to be compared.
        /// </param>
        ///
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the
        /// objects being compared. If the result is less than zero, the
        /// instance is less than <paramref name="other"/>. If the result is zero
        /// the instance is equal to <paramref name="other"/>. If the result is
        /// greater than zero, the instance is greater than
        /// <paramref name="other"/>.
        /// </returns>
        ///
        public int CompareTo(HealthServiceDate other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var yearComparison = _year.CompareTo(other._year);
            if (yearComparison != 0) return yearComparison;
            var monthComparison = _month.CompareTo(other._month);
            if (monthComparison != 0) return monthComparison;
            return _day.CompareTo(other._day);
        }

        /// <summary>
        /// Compares the specified object to this HealthServiceDate object.
        /// </summary>
        ///
        /// <param name="other">
        /// The date to be compared.
        /// </param>
        ///
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the
        /// objects being compared. If the result is less than zero, the
        /// instance is less than <paramref name="other"/>. If the result is zero
        /// the instance is equal to <paramref name="other"/>. If the result is
        /// greater than zero, the instance is greater than
        /// <paramref name="other"/>.
        /// </returns>
        ///
        public int CompareTo(LocalDate other)
        {
            if (Year > other.Year)
            {
                return 1;
            }

            if (Year < other.Year)
            {
                return -1;
            }

            if (Month > other.Month)
            {
                return 1;
            }

            if (Month < other.Month)
            {
                return -1;
            }

            if (Day > other.Day)
            {
                return 1;
            }

            if (Day < other.Day)
            {
                return -1;
            }

            return 0;
        }

        #endregion IComparable

        #region Equals

        /// <summary>
        /// Retrieves a value indicating whether the specified object is equal to this object.
        /// </summary>
        ///
        /// <param name="obj">
        /// The object to be compared.
        /// </param>
        ///
        /// <returns>
        /// <b>true</b> if the <paramref name="obj"/> is a
        /// <see cref="HealthServiceDate"/> object and the year, month, and
        /// day exactly match the year, month, and day of this object; otherwise,
        /// <b>false</b>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="obj"/> parameter is not a <see cref="HealthServiceDate"/>
        /// object.
        /// </exception>
        ///
        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        #endregion Equals

        #region Operators

        /// <summary>
        /// Retrieves a value indicating whether the specified object is equal
        /// to the specified date.
        /// </summary>
        ///
        /// <param name="date">
        /// The date object to be compared.
        /// </param>
        ///
        /// <param name="secondInstance">
        /// The second object to be compared.
        /// </param>
        ///
        /// <returns>
        /// <b>true</b> if the year, month, and day of the <paramref name="date"/>
        /// exactly match the year, month, and day of <paramref name="secondInstance"/> ;
        /// otherwise, <b>false</b>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="secondInstance"/> parameter is not a
        /// <see cref="HealthServiceDate"/> object.
        /// </exception>
        ///
        public static bool operator ==(HealthServiceDate date, object secondInstance)
        {
            if ((object)date == null)
            {
                return secondInstance == null;
            }

            return date.Equals(secondInstance);
        }

        /// <summary>
        /// Retrieves a value indicating whether the specified object is not
        /// equal to the specified date.
        /// </summary>
        ///
        /// <param name="date">
        /// The date object to be compared.
        /// </param>
        ///
        /// <param name="secondInstance">
        /// The second object to be compared.
        /// </param>
        ///
        /// <returns>
        /// <b>false</b> if the year, month, and day of the <paramref name="date"/>
        /// exactly match the year, month, and day of <paramref name="secondInstance"/> ;
        /// otherwise, <b>true</b>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="secondInstance"/> parameter is not a <see cref="HealthServiceDate"/> object.
        /// </exception>
        ///
        public static bool operator !=(HealthServiceDate date, object secondInstance)
        {
            if (date == null)
            {
                return secondInstance != null;
            }

            return !date.Equals(secondInstance);
        }

        /// <summary>
        /// Retrieves a value indicating whether the specified date is greater
        /// than the specified object.
        /// </summary>
        ///
        /// <param name="date">
        /// The date object to be compared.
        /// </param>
        ///
        /// <param name="secondInstance">
        /// The second object to be compared.
        /// </param>
        ///
        /// <returns>
        /// <b>true</b> if the year, month, and day of the <paramref name="date"/>
        /// is greater than the year, month, and day of <paramref name="secondInstance"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="secondInstance"/> parameter is not a <see cref="HealthServiceDate"/> object.
        /// </exception>
        ///
        public static bool operator >(HealthServiceDate date, object secondInstance)
        {
            if (date == null)
            {
                return secondInstance != null;
            }

            return date.CompareTo(secondInstance) > 0;
        }

        /// <summary>
        /// Retrieves a value indicating whether the specified date is less than
        /// the specified object.
        /// </summary>
        ///
        /// <param name="date">
        /// The date object to be compared.
        /// </param>
        ///
        /// <param name="secondInstance">
        /// The second object to be compared.
        /// </param>
        ///
        /// <returns>
        /// <b>true</b> if the year, month, and day of the <paramref name="date"/>
        /// is less than the year, month, and day of <paramref name="secondInstance"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="secondInstance"/> parameter is not a <see cref="HealthServiceDate"/> object.
        /// </exception>
        ///
        public static bool operator <(HealthServiceDate date, object secondInstance)
        {
            if (date == null)
            {
                return secondInstance != null;
            }

            return date.CompareTo(secondInstance) < 0;
        }

        #endregion Operators

        internal string ToString(IFormatProvider formatProvider)
        {
            LocalDate localDate = new LocalDate(Year, Month, Day);
            return localDate.ToString("d", formatProvider);
        }

        /// <summary>
        /// See the base class documentation.
        /// </summary>
        ///
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
