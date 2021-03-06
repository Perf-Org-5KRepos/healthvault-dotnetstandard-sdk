// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Microsoft.HealthVault.Clients;
using Microsoft.HealthVault.Exceptions;
using Microsoft.HealthVault.Helpers;
using Microsoft.HealthVault.Thing;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// Represents a thing type that encapsulates an insulin
    /// injection use.
    /// </summary>
    ///
    /// <remarks>
    /// The use of an insulin injection unit might require more than one dose
    /// based on the prescription.
    /// </remarks>
    ///
    public class InsulinInjectionUse : ThingBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InsulinInjectionUse"/> class with
        /// default values.
        /// </summary>
        ///
        /// <remarks>
        /// The item is not added to the health record until the <see cref="IThingClient.CreateNewThingsAsync{ThingBase}(Guid, ICollection{ThingBase})"/> method is called.
        /// </remarks>
        ///
        public InsulinInjectionUse()
            : base(TypeId)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="InsulinInjectionUse"/> class
        /// with the specified date/time, insulin type, and amount.
        /// </summary>
        ///
        /// <param name="when">
        /// The date/time when the injection was administrated.
        /// </param>
        ///
        /// <param name="insulinType">
        /// The type of insulin being used.
        /// </param>
        ///
        /// <param name="amount">
        /// The amount of insulin.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="when"/>,
        /// <paramref name="amount"/>, or <paramref name="insulinType"/> parameter
        /// is <b>null</b>.
        /// </exception>
        ///
        public InsulinInjectionUse(
            HealthServiceDateTime when,
            CodableValue insulinType,
            InsulinInjectionMeasurement amount)
            : base(TypeId)
        {
            When = when;
            InsulinType = insulinType;
            Amount = amount;
        }

        /// <summary>
        /// Retrieves the unique identifier for the item type.
        /// </summary>
        ///
        /// <value>
        /// A GUID.
        /// </value>
        ///
        public static new readonly Guid TypeId =
            new Guid("184166BE-8ADB-4D9C-8162-C403040E31AD");

        /// <summary>
        /// Populates this <see cref="InsulinInjectionUse"/> instance from the
        /// data in the XML.
        /// </summary>
        ///
        /// <param name="typeSpecificXml">
        /// The XML to get the insulin injection use data from.
        /// </param>
        ///
        /// <exception cref="InvalidOperationException">
        /// The first node in <paramref name="typeSpecificXml"/> is not
        /// an insulin-injection-use node.
        /// </exception>
        ///
        protected override void ParseXml(IXPathNavigable typeSpecificXml)
        {
            XPathNavigator itemNav =
                typeSpecificXml.CreateNavigator().SelectSingleNode(
                    "diabetes-insulin-injection-use");

            Validator.ThrowInvalidIfNull(itemNav, Resources.InsulinInjectionUseUnexpectedNode);

            _when = new HealthServiceDateTime();
            _when.ParseXml(itemNav.SelectSingleNode("when"));

            _insulinType = new CodableValue();
            _insulinType.ParseXml(itemNav.SelectSingleNode("type"));

            _amount = new InsulinInjectionMeasurement();
            _amount.ParseXml(itemNav.SelectSingleNode("amount"));

            XPathNavigator deviceIdNav =
                itemNav.SelectSingleNode("device-id");

            if (deviceIdNav != null)
            {
                _deviceId = deviceIdNav.Value;
            }
        }

        /// <summary>
        /// Writes the insulin injection use data to the specified XmlWriter.
        /// </summary>
        ///
        /// <param name="writer">
        /// The XmlWriter to write the insulin injection use data to.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="writer"/> is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ThingSerializationException">
        /// The <see cref="InsulinType"/> or <see cref="When"/> property is <b>null</b>, or the <see cref="Amount"/> property is <b>null</b> or empty.
        /// </exception>
        ///
        public override void WriteXml(XmlWriter writer)
        {
            Validator.ThrowIfWriterNull(writer);
            Validator.ThrowSerializationIfNull(_when, Resources.InsulinInjectionWhenNotSet);
            Validator.ThrowSerializationIfNull(_insulinType, Resources.InsulinInjectionTypeNotSet);
            Validator.ThrowSerializationIfNull(_amount, Resources.InsulinInjectionAmountNotSet);

            // <diabetes-insulin-injection-use>
            writer.WriteStartElement("diabetes-insulin-injection-use");

            // <when>
            _when.WriteXml("when", writer);

            // <type>
            _insulinType.WriteXml("type", writer);

            // <amount>
            _amount.WriteXml("amount", writer);

            if (!string.IsNullOrEmpty(_deviceId))
            {
                // <device-id>
                writer.WriteElementString("device-id", _deviceId);
            }

            // </diabetes-insulin-injection-use>
            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets or sets the date/time when the insulin injection use occurred.
        /// </summary>
        ///
        /// <value>
        /// A <see cref="HealthServiceDateTime"/> instance representing
        /// the date. The default value is the current year, month, and day.
        /// </value>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="value"/> parameter is <b>null</b>.
        /// </exception>
        ///
        public HealthServiceDateTime When
        {
            get { return _when; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(When), Resources.WhenNullValue);
                _when = value;
            }
        }

        private HealthServiceDateTime _when = new HealthServiceDateTime();

        /// <summary>
        /// Gets or sets the type of insulin being used in the injector.
        /// </summary>
        ///
        /// <value>
        /// A <see cref="CodableValue"/> instance representing the type.
        /// </value>
        ///
        /// <remarks>
        /// The preferred vocabulary is "insulin-types".
        /// </remarks>
        ///
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="value"/> parameter is <b>null</b>.
        /// </exception>
        ///
        public CodableValue InsulinType
        {
            get { return _insulinType; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(InsulinType), Resources.InsulinInjectionTypeMandatory);
                _insulinType = value;
            }
        }

        private CodableValue _insulinType;

        /// <summary>
        /// Gets or sets the amount of insulin.
        /// </summary>
        ///
        /// <value>
        /// A value of <see cref="InsulinInjectionMeasurement"/>.
        /// </value>
        ///
        public InsulinInjectionMeasurement Amount
        {
            get { return _amount; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(Amount), Resources.InsulinInjectionAmountMandatory);
                _amount = value;
            }
        }

        private InsulinInjectionMeasurement _amount;

        /// <summary>
        /// Gets or sets the identifier for the device.
        /// </summary>
        ///
        /// <value>
        /// A string representing the device identifier.
        /// </value>
        ///
        /// <remarks>
        /// Set the value to <b>null</b> if the device identifier should not be
        /// stored.
        /// </remarks>
        ///
        /// <exception cref="ArgumentException">
        /// If <paramref name="value"/> contains only whitespace.
        /// </exception>
        ///
        public string DeviceId
        {
            get { return _deviceId; }

            set
            {
                Validator.ThrowIfStringIsWhitespace(value, "DeviceId");
                _deviceId = value;
            }
        }

        private string _deviceId;

        /// <summary>
        /// Gets a string representation of the insulin injection use item.
        /// </summary>
        ///
        /// <returns>
        /// A string representation of the insulin injection use item.
        /// </returns>
        ///
        public override string ToString()
        {
            return
                string.Format(
                    Resources.InsulinInjectionToStringFormat,
                    InsulinType.Text,
                    Amount.ToString());
        }
    }
}
