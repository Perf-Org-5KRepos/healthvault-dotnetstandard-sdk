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
using Microsoft.HealthVault.Exceptions;
using Microsoft.HealthVault.Helpers;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// Represents a target glucose zone.
    /// </summary>
    ///
    public class TargetGlucoseZone : ItemBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TargetGlucoseZone"/> class with default
        /// values.
        /// </summary>
        ///
        public TargetGlucoseZone()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TargetGlucoseZone"/> class
        /// with the specified absolute glucose value boundaries and name.
        /// </summary>
        ///
        /// <param name="name">
        /// The name of the zone.
        /// </param>
        ///
        /// <param name="lowerBoundaryAbsoluteGlucose">
        /// The glucose value in millimoles per liter (mmol/L) for the lower
        /// boundary of the zone.
        /// </param>
        ///
        /// <param name="upperBoundaryAbsoluteGlucose">
        /// The glucose value in millimoles per liter (mmol/L) for the upper
        /// boundary of the zone.
        /// </param>
        ///
        public TargetGlucoseZone(
            string name,
            BloodGlucoseMeasurement lowerBoundaryAbsoluteGlucose,
            BloodGlucoseMeasurement upperBoundaryAbsoluteGlucose)
        {
            _name = name;
            _lowAbsolute = lowerBoundaryAbsoluteGlucose;
            _upperAbsolute = upperBoundaryAbsoluteGlucose;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TargetGlucoseZone"/> class
        /// with the specified relative glucose value boundaries and name.
        /// </summary>
        ///
        /// <param name="name">
        /// The name of the zone.
        /// </param>
        ///
        /// <param name="lowerBoundaryRelativeGlucose">
        /// The lower glucose boundary as a percentage of the person's
        /// maximum blood glucose level.
        /// </param>
        ///
        /// <param name="upperBoundaryRelativeGlucose">
        /// The upper glucose boundary as a percentage of the person's
        /// maximum blood glucose level.
        /// </param>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="lowerBoundaryRelativeGlucose"/> or
        /// <paramref name="upperBoundaryRelativeGlucose"/> parameter
        /// is less than 0.0 or greater than 1.0.
        /// </exception>
        ///
        public TargetGlucoseZone(
            string name,
            double lowerBoundaryRelativeGlucose,
            double upperBoundaryRelativeGlucose)
        {
            _name = name;
            RelativeLowerBoundary = lowerBoundaryRelativeGlucose;
            RelativeUpperBoundary = upperBoundaryRelativeGlucose;
        }

        /// <summary>
        /// Populates the data from the specified XML.
        /// </summary>
        ///
        /// <param name="navigator">
        /// The XML containing the glucose zone information.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="navigator"/> parameter is null.
        /// </exception>
        ///
        public override void ParseXml(XPathNavigator navigator)
        {
            Validator.ThrowIfNavigatorNull(navigator);

            string name = navigator.GetAttribute("name", string.Empty);
            if (name.Length != 0)
            {
                _name = name;
            }

            XPathNavigator lowNav =
                navigator.SelectSingleNode("lower-bound");
            if (lowNav != null)
            {
                XPathNavigator absoluteNav =
                    lowNav.SelectSingleNode("absolute-glucose");
                if (absoluteNav != null)
                {
                    _lowAbsolute = new BloodGlucoseMeasurement();
                    _lowAbsolute.ParseXml(absoluteNav);
                }
                else
                {
                    XPathNavigator relativeNav =
                        lowNav.SelectSingleNode("percent-max-glucose");
                    if (relativeNav != null)
                    {
                        _lowRelative = relativeNav.ValueAsDouble;
                    }
                }
            }

            XPathNavigator upperNav =
                navigator.SelectSingleNode("upper-bound");
            if (upperNav != null)
            {
                XPathNavigator absoluteNav =
                    upperNav.SelectSingleNode("absolute-glucose");
                if (absoluteNav != null)
                {
                    _upperAbsolute = new BloodGlucoseMeasurement();
                    _upperAbsolute.ParseXml(absoluteNav);
                }
                else
                {
                    XPathNavigator relativeNav =
                        upperNav.SelectSingleNode("percent-max-glucose");
                    if (relativeNav != null)
                    {
                        _upperRelative = relativeNav.ValueAsDouble;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the XML representation of the glucose zone into
        /// the specified XML writer.
        /// </summary>
        ///
        /// <param name="nodeName">
        /// The name of the outer node for the heart rate zone.
        /// </param>
        ///
        /// <param name="writer">
        /// The XML writer into which the glucose zone should be
        /// written.
        /// </param>
        ///
        /// <exception cref="ArgumentException">
        /// The <paramref name="nodeName"/> parameter is null or empty.
        /// </exception>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="writer"/> parameter is null.
        /// </exception>
        ///
        /// <exception cref="ThingSerializationException">
        /// The <see cref="RelativeLowerBoundary"/>, <see cref="AbsoluteLowerBoundary"/>,
        /// <see cref="RelativeUpperBoundary"/>, or <see cref="AbsoluteUpperBoundary"/> parameter is not set.
        /// </exception>
        ///
        public override void WriteXml(string nodeName, XmlWriter writer)
        {
            Validator.ThrowIfStringNullOrEmpty(nodeName, "nodeName");
            Validator.ThrowIfWriterNull(writer);

            if (_lowAbsolute == null && _lowRelative == null)
            {
                throw new ThingSerializationException(Resources.GlucoseZoneNoLowerBoundary);
            }

            if (_upperAbsolute == null && _upperRelative == null)
            {
                throw new ThingSerializationException(Resources.GlucoseZoneNoUpperBoundary);
            }

            writer.WriteStartElement(nodeName);

            if (!string.IsNullOrEmpty(_name))
            {
                writer.WriteAttributeString("name", _name);
            }

            writer.WriteStartElement("lower-bound");
            if (_lowAbsolute != null)
            {
                _lowAbsolute.WriteXml("absolute-glucose", writer);
            }
            else
            {
                writer.WriteElementString(
                    "percent-max-glucose",
                    _lowRelative.Value.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteEndElement();

            writer.WriteStartElement("upper-bound");
            if (_upperAbsolute != null)
            {
                _upperAbsolute.WriteXml("absolute-glucose", writer);
            }
            else
            {
                writer.WriteElementString(
                    "percent-max-glucose",
                    _upperRelative.Value.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets or sets the name for the blood glucose zone.
        /// </summary>
        ///
        /// <value>
        /// A string representing the name.
        /// </value>
        ///
        /// <remarks>
        /// Set the value to null if the name should not be stored.
        /// </remarks>
        ///
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _name;

        /// <summary>
        /// Gets or sets the lower boundary of the blood glucose zone as a
        /// percentage of the person's maximum blood glucose level.
        /// </summary>
        ///
        /// <value>
        /// A number representing the boundary.
        /// </value>
        ///
        /// <remarks>
        /// Only <see cref="RelativeLowerBoundary"/> or
        /// <see cref="AbsoluteLowerBoundary"/> can be stored. If both are
        /// set, only <see cref="AbsoluteLowerBoundary"/> is stored.
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than 0.0 or greater than 1.0.
        /// </exception>
        ///
        public double? RelativeLowerBoundary
        {
            get { return _lowRelative; }

            set
            {
                if (value != null && (value < 0.0 || value > 1.0))
                {
                    throw new ArgumentOutOfRangeException(nameof(RelativeLowerBoundary), Resources.GlucoseZoneRelativeBoundaryRange);
                }

                _lowRelative = value;
            }
        }

        private double? _lowRelative;

        /// <summary>
        /// Gets or sets the lower boundary of the blood glucose zone as a
        /// specific blood glucose level.
        /// </summary>
        ///
        /// <value>
        /// A <see cref="BloodGlucoseMeasurement"/> instance representing the
        /// boundary.
        /// </value>
        ///
        /// <remarks>
        /// Only <see cref="RelativeLowerBoundary"/> or
        /// <see cref="AbsoluteLowerBoundary"/> can be stored. If both are
        /// set, only <see cref="AbsoluteLowerBoundary"/> is stored.
        /// </remarks>
        ///
        public BloodGlucoseMeasurement AbsoluteLowerBoundary
        {
            get { return _lowAbsolute; }
            set { _lowAbsolute = value; }
        }

        private BloodGlucoseMeasurement _lowAbsolute;

        /// <summary>
        /// Gets or sets the upper boundary of the blood glucose zone as a
        /// percentage of the person's maximum blood glucose level.
        /// </summary>
        ///
        /// <value>
        /// A number representing the boundary.
        /// </value>
        ///
        /// <remarks>
        /// Only <see cref="RelativeUpperBoundary"/> or
        /// <see cref="AbsoluteUpperBoundary"/> may be stored. If both are
        /// set, only <see cref="AbsoluteUpperBoundary"/> will be stored.
        /// </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> parameter is less than 0.0 or greater than 1.0.
        /// </exception>
        ///
        public double? RelativeUpperBoundary
        {
            get { return _upperRelative; }

            set
            {
                if (value != null && (value < 0.0 || value > 1.0))
                {
                    throw new ArgumentOutOfRangeException(nameof(RelativeUpperBoundary), Resources.GlucoseZoneRelativeBoundaryRange);
                }

                _upperRelative = value;
            }
        }

        private double? _upperRelative;

        /// <summary>
        /// Gets or sets the upper boundary of the blood glucose zone as a
        /// specific blood glucose level.
        /// </summary>
        ///
        /// <remarks>
        /// Only <see cref="RelativeUpperBoundary"/> or
        /// <see cref="AbsoluteUpperBoundary"/> can be stored. If both are
        /// set, only <see cref="AbsoluteUpperBoundary"/> is stored.
        /// </remarks>
        ///
        public BloodGlucoseMeasurement AbsoluteUpperBoundary
        {
            get { return _upperAbsolute; }
            set { _upperAbsolute = value; }
        }

        private BloodGlucoseMeasurement _upperAbsolute;
    }
}
