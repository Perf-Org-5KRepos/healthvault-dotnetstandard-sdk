// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.XPath;
using Microsoft.HealthVault.Exceptions;
using Microsoft.HealthVault.Helpers;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// Information containing name value pair of defibrillator episode property.
    /// </summary>
    public class DefibrillatorEpisodeField : ItemBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefibrillatorEpisodeField"/> class with default values.
        /// </summary>
        ///
        public DefibrillatorEpisodeField()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DefibrillatorEpisodeField"/> class
        /// specifying mandatory values.
        /// </summary>
        ///
        /// <param name="name">
        /// Represents the name of the name/value pair.
        /// </param>
        ///
        /// <param name="value">
        /// Represents the value of the name/value pair.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="value"/> is <b>null</b>.
        /// </exception>
        ///
        public DefibrillatorEpisodeField(CodableValue name, CodableValue value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Populates this <see cref="DefibrillatorEpisodeField"/> instance from the data in the specified XML.
        /// </summary>
        ///
        /// <param name="navigator">
        /// The XML to get the DefibrillatorEpisodeField data from.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="navigator"/> parameter is <b>null</b>.
        /// </exception>
        ///
        public override void ParseXml(XPathNavigator navigator)
        {
            Validator.ThrowIfNavigatorNull(navigator);

            _name = new CodableValue();
            _name.ParseXml(navigator.SelectSingleNode("field-name"));

            _value = new CodableValue();
            _value.ParseXml(navigator.SelectSingleNode("field-value"));
        }

        /// <summary>
        /// Writes the XML representation of the  <see cref="DefibrillatorEpisodeField"/> into
        /// the specified XML writer.
        /// </summary>
        ///
        /// <param name="nodeName">
        /// The name of the outer node for the defibrillator episode field item.
        /// </param>
        ///
        /// <param name="writer">
        /// The XML writer into which the DefibrillatorEpisodeField should be
        /// written.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="writer"/> parameter is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ThingSerializationException">
        /// If name or value is <b>null</b>.
        /// </exception>
        ///
        public override void WriteXml(string nodeName, XmlWriter writer)
        {
            Validator.ThrowIfStringNullOrEmpty(nodeName, "WriteXmlEmptyNodeName");
            Validator.ThrowIfWriterNull(writer);
            Validator.ThrowSerializationIfNull(_name, Resources.DefibrillatorEpisodeFieldNameNullValue);
            Validator.ThrowSerializationIfNull(_value, Resources.DefibrillatorEpisodeFieldValueNullValue);

            writer.WriteStartElement(nodeName);
            _name.WriteXml("field-name", writer);
            _value.WriteXml("field-value", writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Name of the defibrillator episode property.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "FXCop thinks that CodableValue is a collection, so it throws this error.")]
        public CodableValue Name
        {
            get
            {
                return _name;
            }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(Name), Resources.DefibrillatorEpisodeFieldNameNullValue);
                _name = value;
            }
        }

        private CodableValue _name;

        /// <summary>
        /// Value of the defibrillator episode property.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "FXCop thinks that CodableValue is a collection, so it throws this error.")]
        public CodableValue Value
        {
            get
            {
                return _value;
            }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(Value), Resources.DefibrillatorEpisodeFieldValueNullValue);
                _value = value;
            }
        }

        private CodableValue _value;
    }
}