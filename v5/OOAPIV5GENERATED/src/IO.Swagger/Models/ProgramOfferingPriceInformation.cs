using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOfferingPriceInformation
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
    }
}
