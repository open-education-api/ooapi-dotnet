using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOfferingPriceInformation : IEquatable<ProgramOfferingPriceInformation>
    {
        /// <summary>
        /// The type of the cost. This is an *extensible enumeration*.  The following values have been defined in the specification:   - &#x60;STAP eligible&#x60;: the costs that a student can get STAP subsidy for   - &#x60;total costs&#x60;: the total costs that a student is to pay to follow this offering  Implementations are allowed to add additional values to those above, as long as they do not overlap in definition to existing values. 
        /// </summary>
        /// <value>The type of the cost. This is an *extensible enumeration*.  The following values have been defined in the specification:   - &#x60;STAP eligible&#x60;: the costs that a student can get STAP subsidy for   - &#x60;total costs&#x60;: the total costs that a student is to pay to follow this offering  Implementations are allowed to add additional values to those above, as long as they do not overlap in definition to existing values. </value>
        [JsonRequired]

        [DataMember(Name = "costType")]
        public string CostType { get; set; }

        /// <summary>
        /// The total amount of the cost as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.
        /// </summary>
        /// <value>The total amount of the cost as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.</value>
        [RegularExpression("/^\\d+(?:\\.\\d+)?$/")]
        [DataMember(Name = "amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The part of the cost that is VAT, as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.
        /// </summary>
        /// <value>The part of the cost that is VAT, as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.</value>
        [RegularExpression("/^\\d+(?:\\.\\d+)?$/")]
        [DataMember(Name = "vatAmount")]
        public string VatAmount { get; set; }

        /// <summary>
        /// The part of the cost that is non-VAT. as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.
        /// </summary>
        /// <value>The part of the cost that is non-VAT. as a string. Use a &#x27;.&#x27; (dot) as an optional separator. The numbers before the separator signify the major units of the currency, after the dot the minor units. Only a single separator is allowed. Do not use a comma.</value>
        [RegularExpression("/^\\d+(?:\\.\\d+)?$/")]
        [DataMember(Name = "amountWithoutVat")]
        public string AmountWithoutVat { get; set; }

        /// <summary>
        /// The currency this cost is in. Should correspond to one of the currency codes from ISO 4217.
        /// </summary>
        /// <value>The currency this cost is in. Should correspond to one of the currency codes from ISO 4217.</value>

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// An array of optional pre-formatted strings in different locales. Clients can choose to use this string instead of rendering their own based on the current locale of the user.
        /// </summary>
        /// <value>An array of optional pre-formatted strings in different locales. Clients can choose to use this string instead of rendering their own based on the current locale of the user.</value>

        [DataMember(Name = "displayAmount")]
        public List<LanguageValueItem> DisplayAmount { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProgramOfferingPriceInformation {\n");
            sb.Append("  CostType: ").Append(CostType).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  VatAmount: ").Append(VatAmount).Append("\n");
            sb.Append("  AmountWithoutVat: ").Append(AmountWithoutVat).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  DisplayAmount: ").Append(DisplayAmount).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProgramOfferingPriceInformation)obj);
        }

        /// <summary>
        /// Returns true if ProgramOfferingPriceInformation instances are equal
        /// </summary>
        /// <param name="other">Instance of ProgramOfferingPriceInformation to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProgramOfferingPriceInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    CostType == other.CostType ||
                    CostType != null &&
                    CostType.Equals(other.CostType)
                ) &&
                (
                    Amount == other.Amount ||
                    Amount != null &&
                    Amount.Equals(other.Amount)
                ) &&
                (
                    VatAmount == other.VatAmount ||
                    VatAmount != null &&
                    VatAmount.Equals(other.VatAmount)
                ) &&
                (
                    AmountWithoutVat == other.AmountWithoutVat ||
                    AmountWithoutVat != null &&
                    AmountWithoutVat.Equals(other.AmountWithoutVat)
                ) &&
                (
                    Currency == other.Currency ||
                    Currency != null &&
                    Currency.Equals(other.Currency)
                ) &&
                (
                    DisplayAmount == other.DisplayAmount ||
                    DisplayAmount != null &&
                    DisplayAmount.SequenceEqual(other.DisplayAmount)
                ) &&
                (
                    Ext == other.Ext ||
                    Ext != null &&
                    Ext.Equals(other.Ext)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (CostType != null)
                    hashCode = hashCode * 59 + CostType.GetHashCode();
                if (Amount != null)
                    hashCode = hashCode * 59 + Amount.GetHashCode();
                if (VatAmount != null)
                    hashCode = hashCode * 59 + VatAmount.GetHashCode();
                if (AmountWithoutVat != null)
                    hashCode = hashCode * 59 + AmountWithoutVat.GetHashCode();
                if (Currency != null)
                    hashCode = hashCode * 59 + Currency.GetHashCode();
                if (DisplayAmount != null)
                    hashCode = hashCode * 59 + DisplayAmount.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ProgramOfferingPriceInformation left, ProgramOfferingPriceInformation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProgramOfferingPriceInformation left, ProgramOfferingPriceInformation right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
