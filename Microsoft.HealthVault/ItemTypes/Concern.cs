// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Xml;
using System.Xml.XPath;
using Microsoft.HealthVault.Exceptions;
using Microsoft.HealthVault.Helpers;
using Microsoft.HealthVault.Thing;

namespace Microsoft.HealthVault.ItemTypes
{
    /// <summary>
    /// A concern that a person has about a condition or life issue.
    /// </summary>
    ///
    /// <remarks>
    /// A concern that a person has about a condition or life issue.
    /// Concerns are more general than conditions, and are about the
    /// person's feelings. Examples include "concerned about managing
    /// a chronic condition", "family issues", "emotional issues", etc.
    /// </remarks>
    ///
    public class Concern : ThingBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Concern"/> class,
        /// with default values.
        /// </summary>
        ///
        public Concern()
            : base(TypeId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Concern"/> class,
        /// with a specified description.
        /// </summary>
        ///
        /// <param name="description">
        /// The description of a concern.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="description"/> is <b>null</b>.
        /// </exception>
        ///
        public Concern(CodableValue description)
            : base(TypeId)
        {
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Concern"/> class,
        /// with the specified description.
        /// </summary>
        ///
        /// <param name="description">
        /// The description of a concern.
        /// </param>
        ///
        /// <exception cref="ArgumentException">
        /// If <paramref name="description"/> is <b> null</b> or empty.
        /// </exception>
        ///
        public Concern(string description)
            : base(TypeId)
        {
            _description = new CodableValue(description);
        }

        /// <summary>
        /// Retrieves the unique identifier for the item type.
        /// </summary>
        ///
        public static new readonly Guid TypeId =
            new Guid("AEA2E8F2-11DD-4A7D-AB43-1D58764EBC19");

        /// <summary>
        /// Populates this <see cref="Concern"/> instance from the data in the XML.
        /// </summary>
        ///
        /// <param name="typeSpecificXml">
        /// The XML to get the concern data from.
        /// </param>
        ///
        /// <exception cref="InvalidOperationException">
        /// If the first node in <paramref name="typeSpecificXml"/> is not
        /// a concern node.
        /// </exception>
        ///
        protected override void ParseXml(IXPathNavigable typeSpecificXml)
        {
            XPathNavigator itemNav =
                typeSpecificXml.CreateNavigator().SelectSingleNode("concern");

            Validator.ThrowInvalidIfNull(itemNav, Resources.ConcernUnexpectedNode);

            // description
            _description = new CodableValue();
            _description.ParseXml(itemNav.SelectSingleNode("description"));

            // status
            _status =
                XPathHelper.GetOptNavValue<CodableValue>(itemNav, "status");
        }

        /// <summary>
        /// Writes the concern data to the specified XmlWriter.
        /// </summary>
        ///
        /// <param name="writer">
        /// The XmlWriter to write the concern data to.
        /// </param>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="writer"/> is <b>null</b>.
        /// </exception>
        ///
        /// <exception cref="ThingSerializationException">
        /// If <see cref="Description"/> is <b>null</b> or empty.
        /// </exception>
        ///
        public override void WriteXml(XmlWriter writer)
        {
            Validator.ThrowIfWriterNull(writer);
            Validator.ThrowSerializationIfNull(_description, Resources.ConcernDescriptionNotSet);
            Validator.ThrowSerializationIfNull(_description.Text, Resources.CodableValueNullText);

            // <concern>
            writer.WriteStartElement("concern");

            // description
            _description.WriteXml("description", writer);

            // status
            XmlWriterHelper.WriteOpt(
                writer,
                "status",
                _status);

            // </concern>
            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets or sets the description of a concern.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="value"/> is <b>null</b> on set.
        /// </exception>
        ///
        public CodableValue Description
        {
            get { return _description; }

            set
            {
                Validator.ThrowIfArgumentNull(value, nameof(Description), Resources.ConcernDescriptionMandatory);
                _description = value;
            }
        }

        private CodableValue _description = new CodableValue();

        /// <summary>
        /// Gets or sets the status of the concern.
        /// </summary>
        ///
        /// <remarks>
        /// Examples of status include active or inactive. If the
        /// status is not known the value should be set to <b>null</b>.
        /// </remarks>
        ///
        public CodableValue Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private CodableValue _status;

        /// <summary>
        /// Gets the description of a concern instance.
        /// </summary>
        ///
        /// <returns>
        /// A string representation of the concern item.
        /// </returns>
        ///
        public override string ToString()
        {
            return _description.ToString();
        }
    }
}
