using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     The customer's info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class Customer : FieldsDictionary
    {
        /// <summary>
        ///     The client's e-mail.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        ///     The client's identifier in merchant's system.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        ///     The phone number to which invoice issued.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
    }
}