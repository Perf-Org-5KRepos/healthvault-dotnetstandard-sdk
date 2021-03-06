// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using Microsoft.HealthVault.Clients;
using Microsoft.HealthVault.Exceptions;
using Microsoft.HealthVault.Helpers;
using Microsoft.HealthVault.Thing;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// Represents a thing type that encapsulates a sleep journal
    /// morning entry.
    /// </summary>
    ///
    public class SleepJournalAM : ThingBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SleepJournalAM"/> class with
        /// default values.
        /// </summary>
        ///
        /// <remarks>
        /// The item is not added to the health record until the <see cref="IThingClient.CreateNewThingsAsync{ThingBase}(Guid, ICollection{ThingBase})"/> method is called.
        /// </remarks>
        ///
        public SleepJournalAM()
            : base(TypeId)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SleepJournalAM"/> class
        /// specifying the mandatory values.
        /// </summary>
        ///
        /// <param name="when">
        /// The date and time when the AM sleep journal entry was taken.
        /// </param>
        ///
        /// <param name="bedtime">
        /// The approximate time the person went to bed.
        /// </param>
        ///
        /// <param name="wakeTime">
        /// The approximate time the person woke up.
        /// </param>
        ///
        /// <param name="sleepMinutes">
        /// The number of minutes the person slept.
        /// </param>
        ///
        /// <param name="settlingMinutes">
        /// The number of minutes it took the person to fall asleep after
        /// going to bed.
        /// </param>
        ///
        /// <param name="wakeState">
        /// The state of the person when they awoke.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="when"/> parameter is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="sleepMinutes"/> parameter or
        /// <paramref name="settlingMinutes"/> parameter is negative.
        /// </exception>
        ///
        public SleepJournalAM(
            HealthServiceDateTime when,
            ApproximateTime bedtime,
            ApproximateTime wakeTime,
            int sleepMinutes,
            int settlingMinutes,
            WakeState wakeState)
            : base(TypeId)
        {
            When = when;
            Bedtime = bedtime;
            WakeTime = wakeTime;
            SleepMinutes = sleepMinutes;
            SettlingMinutes = settlingMinutes;
            WakeState = wakeState;
        }

        /// <summary>
        /// Retrieves the unique identifier for the item type.
        /// </summary>
        ///
        public static new readonly Guid TypeId =
            new Guid("11c52484-7f1a-11db-aeac-87d355d89593");

        /// <summary>
        /// Populates this SleepJournalAM instance from the data in the XML.
        /// </summary>
        ///
        /// <param name="typeSpecificXml">
        /// The XML to get the morning sleep journal data from.
        /// </param>
        ///
        /// <exception cref="InvalidOperationException">
        /// The first node in the <paramref name="typeSpecificXml"/> parameter
        /// is not a sleep-am node.
        /// </exception>
        ///
        protected override void ParseXml(IXPathNavigable typeSpecificXml)
        {
            XPathNavigator sleepNav =
                typeSpecificXml.CreateNavigator().SelectSingleNode("sleep-am");

            Validator.ThrowInvalidIfNull(sleepNav, Resources.SleepJournalAMUnexpectedNode);

            _when = new HealthServiceDateTime();
            _when.ParseXml(sleepNav.SelectSingleNode("when"));

            _bedtime = new ApproximateTime();
            _bedtime.ParseXml(sleepNav.SelectSingleNode("bed-time"));

            _wakeTime = new ApproximateTime();
            _wakeTime.ParseXml(sleepNav.SelectSingleNode("wake-time"));

            _sleepMinutes =
                sleepNav.SelectSingleNode("sleep-minutes").ValueAsInt;

            _settlingMinutes =
                sleepNav.SelectSingleNode("settling-minutes").ValueAsInt;

            XPathNodeIterator awakeningsIterator =
                sleepNav.Select("awakening");

            foreach (XPathNavigator awakeningNav in awakeningsIterator)
            {
                Occurrence awakening = new Occurrence();
                awakening.ParseXml(awakeningNav);
                _awakenings.Add(awakening);
            }

            XPathNavigator medNav = sleepNav.SelectSingleNode("medications");

            if (medNav != null)
            {
                _medications = new CodableValue();
                _medications.ParseXml(medNav);
            }

            _wakeState =
                (WakeState)sleepNav.SelectSingleNode("wake-state").ValueAsInt;
        }

        /// <summary>
        /// Writes the morning sleep journal data to the specified XmlWriter.
        /// </summary>
        ///
        /// <param name="writer">
        /// The XmlWriter to write the morning sleep journal data to.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="writer"/> is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ThingSerializationException">
        /// The <see cref="When"/>, <see cref="Bedtime"/>,
        /// <see cref="WakeTime"/>, <see cref="SleepMinutes"/>,
        /// <see cref="SettlingMinutes"/>, or <see cref="WakeState"/> parameter
        /// has not been set.
        /// </exception>
        ///
        public override void WriteXml(XmlWriter writer)
        {
            Validator.ThrowIfWriterNull(writer);

            Validator.ThrowSerializationIfNull(_when, Resources.SleepJournalAMWhenNotSet);
            Validator.ThrowSerializationIfNull(_bedtime, Resources.SleepJournalAMBedTimeNotSet);
            Validator.ThrowSerializationIfNull(_wakeTime, Resources.SleepJournalAMWakeTimeNotSet);
            Validator.ThrowSerializationIfNull(_sleepMinutes, Resources.SleepJournalAMSleepMinutesNotSet);
            Validator.ThrowSerializationIfNull(_settlingMinutes, Resources.SleepJournalAMSettlingMinutesNotSet);

            if (_wakeState == WakeState.Unknown)
            {
                throw new ThingSerializationException(Resources.SleepJournalAMWakeStateNotSet);
            }

            // <sleep-am>
            writer.WriteStartElement("sleep-am");

            _when.WriteXml("when", writer);
            _bedtime.WriteXml("bed-time", writer);
            _wakeTime.WriteXml("wake-time", writer);

            writer.WriteElementString(
                "sleep-minutes",
                _sleepMinutes.ToString());

            writer.WriteElementString(
                "settling-minutes",
                _settlingMinutes.ToString());

            foreach (Occurrence awakening in _awakenings)
            {
                awakening.WriteXml("awakening", writer);
            }

            _medications?.WriteXml("medications", writer);

            writer.WriteElementString(
                "wake-state",
                ((int)_wakeState).ToString(CultureInfo.InvariantCulture));

            // </sleep-am>
            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets or sets the when the journal entry is made.
        /// </summary>
        ///
        /// <returns>
        /// A <see cref="HealthServiceDateTime"/> instance representing the
        /// date and time.
        /// </returns>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="value"/> parameter is <b>null</b> when set.
        /// </exception>
        ///
        public HealthServiceDateTime When
        {
            get { return _when; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(When), Resources.SleepJournalAMWhenMandatory);
                _when = value;
            }
        }

        private HealthServiceDateTime _when;

        /// <summary>
        /// Gets or sets the when the person went to bed.
        /// </summary>
        ///
        /// <returns>
        /// An <see cref="ApproximateTime"/> instance representing the time.
        /// </returns>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="value"/> parameter is <b>null</b> when set.
        /// </exception>
        ///
        public ApproximateTime Bedtime
        {
            get { return _bedtime; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(Bedtime), Resources.SleepJournalAMBedTimeMandatory);
                _bedtime = value;
            }
        }

        private ApproximateTime _bedtime;

        /// <summary>
        /// Gets or sets the when the person woke up.
        /// </summary>
        ///
        /// <returns>
        /// An <see cref="ApproximateTime"/> instance representing the time.
        /// </returns>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="value"/> parameter is <b>null</b> when set.
        /// </exception>
        ///
        public ApproximateTime WakeTime
        {
            get { return _wakeTime; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(WakeTime), Resources.SleepJournalAMWakeTimeMandatory);
                _wakeTime = value;
            }
        }

        private ApproximateTime _wakeTime;

        /// <summary>
        /// Gets or sets the number of minutes slept.
        /// </summary>
        ///
        /// <returns>
        /// An integer representing the number of minutes.
        /// </returns>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than or equal to zero.
        /// </exception>
        ///
        public int SleepMinutes
        {
            get { return _sleepMinutes ?? 0; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(SleepMinutes), Resources.SleepJournalAMSleepMinutesNotPositive);
                }

                _sleepMinutes = value;
            }
        }

        private int? _sleepMinutes;

        /// <summary>
        /// Gets or sets the number of minutes spent settling into sleep.
        /// </summary>
        ///
        /// <returns>
        /// An integer representing the number of minutes.
        /// </returns>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than or equal to zero.
        /// </exception>
        ///
        public int SettlingMinutes
        {
            get { return _settlingMinutes ?? 0; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(SettlingMinutes), Resources.SleepJournalAMSettlingMinutesMandatory);
                }

                _settlingMinutes = value;
            }
        }

        private int? _settlingMinutes;

        /// <summary>
        /// Gets the occurrences of awakenings that occurred during
        /// sleeping period.
        /// </summary>
        ///
        /// <returns>
        /// A collection representing the number of awakenings.
        /// </returns>
        ///
        /// <remarks>
        /// To add awakenings, add new <see cref="Occurrence"/> instances to the returned
        /// collection.
        /// </remarks>
        ///
        public Collection<Occurrence> Awakenings => _awakenings;

        private readonly Collection<Occurrence> _awakenings =
            new Collection<Occurrence>();

        /// <summary>
        /// Gets or sets a description of the medications taken before bed.
        /// </summary>
        ///
        /// <returns>
        /// A <see cref="CodableValue"/> instance representing the description.
        /// </returns>
        ///
        /// <remarks>
        /// Set the value to <b>null</b> if the medications should not be stored.
        /// </remarks>
        ///
        public CodableValue Medications
        {
            get { return _medications; }
            set { _medications = value; }
        }

        private CodableValue _medications;

        /// <summary>
        /// Gets or sets the state of the person when they awoke.
        /// </summary>
        ///
        /// <returns>
        /// A <see cref="WakeState"/> value representing the state.
        /// </returns>
        ///
        public WakeState WakeState
        {
            get { return _wakeState; }
            set { _wakeState = value; }
        }

        private WakeState _wakeState = WakeState.Unknown;

        /// <summary>
        /// Gets a string representation of the sleep journal entry.
        /// </summary>
        ///
        /// <returns>
        /// A string representing the sleep journal entry.
        /// </returns>
        ///
        public override string ToString()
        {
            string result = When.ToString();

            if (SleepMinutes > 0)
            {
                result =
                    string.Format(
                        Resources.SleepJournalAmToStringFormat,
                    When.ToString(),
                    SleepMinutes);
            }

            return result;
        }
    }
}
