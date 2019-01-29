using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// Invoice currency enum.
    /// </summary>
    [ComVisible(true)]
    public class CurrencyEnum : StringEnum<CurrencyEnum>
    {
        [ComVisible(true)]
        public static CurrencyEnum Aed => new CurrencyEnum("AED");
        
        [ComVisible(true)]
        public static CurrencyEnum Afn => new CurrencyEnum("AFN");
        
        [ComVisible(true)]
        public static CurrencyEnum All => new CurrencyEnum("ALL");
        
        [ComVisible(true)]
        public static CurrencyEnum Amd => new CurrencyEnum("AMD");
        
        [ComVisible(true)]
        public static CurrencyEnum Ang => new CurrencyEnum("ANG");
        
        [ComVisible(true)]
        public static CurrencyEnum Aoa => new CurrencyEnum("AOA");
        
        [ComVisible(true)]
        public static CurrencyEnum Ars => new CurrencyEnum("ARS");
        
        [ComVisible(true)]
        public static CurrencyEnum Aud => new CurrencyEnum("AUD");
        
        [ComVisible(true)]
        public static CurrencyEnum Awg => new CurrencyEnum("AWG");
        
        [ComVisible(true)]
        public static CurrencyEnum Azn => new CurrencyEnum("AZN");
        
        [ComVisible(true)]
        public static CurrencyEnum Bam => new CurrencyEnum("BAM");
        
        [ComVisible(true)]
        public static CurrencyEnum Bbd => new CurrencyEnum("BBD");
        
        [ComVisible(true)]
        public static CurrencyEnum Bdt => new CurrencyEnum("BDT");
        
        [ComVisible(true)]
        public static CurrencyEnum Bgn => new CurrencyEnum("BGN");
        
        [ComVisible(true)]
        public static CurrencyEnum Bhd => new CurrencyEnum("BHD");
        
        [ComVisible(true)]
        public static CurrencyEnum Bif => new CurrencyEnum("BIF");
        
        [ComVisible(true)]
        public static CurrencyEnum Bmd => new CurrencyEnum("BMD");
        
        [ComVisible(true)]
        public static CurrencyEnum Bnd => new CurrencyEnum("BND");
        
        [ComVisible(true)]
        public static CurrencyEnum Bob => new CurrencyEnum("BOB");
        
        [ComVisible(true)]
        public static CurrencyEnum Bov => new CurrencyEnum("BOV");
        
        [ComVisible(true)]
        public static CurrencyEnum Brl => new CurrencyEnum("BRL");
        
        [ComVisible(true)]
        public static CurrencyEnum Bsd => new CurrencyEnum("BSD");
        
        [ComVisible(true)]
        public static CurrencyEnum Btn => new CurrencyEnum("BTN");
        
        [ComVisible(true)]
        public static CurrencyEnum Bwp => new CurrencyEnum("BWP");
        
        [ComVisible(true)]
        public static CurrencyEnum Byr => new CurrencyEnum("BYR");
        
        [ComVisible(true)]
        public static CurrencyEnum Bzd => new CurrencyEnum("BZD");
        
        [ComVisible(true)]
        public static CurrencyEnum Cad => new CurrencyEnum("CAD");
        
        [ComVisible(true)]
        public static CurrencyEnum Cdf => new CurrencyEnum("CDF");
        
        [ComVisible(true)]
        public static CurrencyEnum Che => new CurrencyEnum("CHE");
        
        [ComVisible(true)]
        public static CurrencyEnum Chf => new CurrencyEnum("CHF");
        
        [ComVisible(true)]
        public static CurrencyEnum Chw => new CurrencyEnum("CHW");
        
        [ComVisible(true)]
        public static CurrencyEnum Clf => new CurrencyEnum("CLF");
        
        [ComVisible(true)]
        public static CurrencyEnum Clp => new CurrencyEnum("CLP");
        
        [ComVisible(true)]
        public static CurrencyEnum Cny => new CurrencyEnum("CNY");
        
        [ComVisible(true)]
        public static CurrencyEnum Cop => new CurrencyEnum("COP");
        
        [ComVisible(true)]
        public static CurrencyEnum Cou => new CurrencyEnum("COU");
        
        [ComVisible(true)]
        public static CurrencyEnum Crc => new CurrencyEnum("CRC");
        
        [ComVisible(true)]
        public static CurrencyEnum Cuc => new CurrencyEnum("CUC");
        
        [ComVisible(true)]
        public static CurrencyEnum Cup => new CurrencyEnum("CUP");
        
        [ComVisible(true)]
        public static CurrencyEnum Cve => new CurrencyEnum("CVE");
        
        [ComVisible(true)]
        public static CurrencyEnum Czk => new CurrencyEnum("CZK");
        
        [ComVisible(true)]
        public static CurrencyEnum Djf => new CurrencyEnum("DJF");
        
        [ComVisible(true)]
        public static CurrencyEnum Dkk => new CurrencyEnum("DKK");
        
        [ComVisible(true)]
        public static CurrencyEnum Dop => new CurrencyEnum("DOP");
        
        [ComVisible(true)]
        public static CurrencyEnum Dzd => new CurrencyEnum("DZD");
        
        [ComVisible(true)]
        public static CurrencyEnum Egp => new CurrencyEnum("EGP");
        
        [ComVisible(true)]
        public static CurrencyEnum Ern => new CurrencyEnum("ERN");
        
        [ComVisible(true)]
        public static CurrencyEnum Etb => new CurrencyEnum("ETB");
        
        [ComVisible(true)]
        public static CurrencyEnum Eur => new CurrencyEnum("EUR");
        
        [ComVisible(true)]
        public static CurrencyEnum Fjd => new CurrencyEnum("FJD");
        
        [ComVisible(true)]
        public static CurrencyEnum Fkp => new CurrencyEnum("FKP");
        
        [ComVisible(true)]
        public static CurrencyEnum Gbp => new CurrencyEnum("GBP");
        
        [ComVisible(true)]
        public static CurrencyEnum Gel => new CurrencyEnum("GEL");
        
        [ComVisible(true)]
        public static CurrencyEnum Ghs => new CurrencyEnum("GHS");
        
        [ComVisible(true)]
        public static CurrencyEnum Gip => new CurrencyEnum("GIP");
        
        [ComVisible(true)]
        public static CurrencyEnum Gmd => new CurrencyEnum("GMD");
        
        [ComVisible(true)]
        public static CurrencyEnum Gnf => new CurrencyEnum("GNF");
        
        [ComVisible(true)]
        public static CurrencyEnum Gtq => new CurrencyEnum("GTQ");
        
        [ComVisible(true)]
        public static CurrencyEnum Gyd => new CurrencyEnum("GYD");
        
        [ComVisible(true)]
        public static CurrencyEnum Hkd => new CurrencyEnum("HKD");
        
        [ComVisible(true)]
        public static CurrencyEnum Hnl => new CurrencyEnum("HNL");
        
        [ComVisible(true)]
        public static CurrencyEnum Hrk => new CurrencyEnum("HRK");
        
        [ComVisible(true)]
        public static CurrencyEnum Htg => new CurrencyEnum("HTG");
        
        [ComVisible(true)]
        public static CurrencyEnum Huf => new CurrencyEnum("HUF");
        
        [ComVisible(true)]
        public static CurrencyEnum Idr => new CurrencyEnum("IDR");
        
        [ComVisible(true)]
        public static CurrencyEnum Ils => new CurrencyEnum("ILS");
        
        [ComVisible(true)]
        public static CurrencyEnum Inr => new CurrencyEnum("INR");
        
        [ComVisible(true)]
        public static CurrencyEnum Iqd => new CurrencyEnum("IQD");
        
        [ComVisible(true)]
        public static CurrencyEnum Irr => new CurrencyEnum("IRR");
        
        [ComVisible(true)]
        public static CurrencyEnum Isk => new CurrencyEnum("ISK");
        
        [ComVisible(true)]
        public static CurrencyEnum Jmd => new CurrencyEnum("JMD");
        
        [ComVisible(true)]
        public static CurrencyEnum Jod => new CurrencyEnum("JOD");
        
        [ComVisible(true)]
        public static CurrencyEnum Jpy => new CurrencyEnum("JPY");
        
        [ComVisible(true)]
        public static CurrencyEnum Kes => new CurrencyEnum("KES");
        
        [ComVisible(true)]
        public static CurrencyEnum Kgs => new CurrencyEnum("KGS");
        
        [ComVisible(true)]
        public static CurrencyEnum Khr => new CurrencyEnum("KHR");
        
        [ComVisible(true)]
        public static CurrencyEnum Kmf => new CurrencyEnum("KMF");
        
        [ComVisible(true)]
        public static CurrencyEnum Kpw => new CurrencyEnum("KPW");
        
        [ComVisible(true)]
        public static CurrencyEnum Krw => new CurrencyEnum("KRW");
        
        [ComVisible(true)]
        public static CurrencyEnum Kwd => new CurrencyEnum("KWD");
        
        [ComVisible(true)]
        public static CurrencyEnum Kyd => new CurrencyEnum("KYD");
        
        [ComVisible(true)]
        public static CurrencyEnum Kzt => new CurrencyEnum("KZT");
        
        [ComVisible(true)]
        public static CurrencyEnum Lak => new CurrencyEnum("LAK");
        
        [ComVisible(true)]
        public static CurrencyEnum Lbp => new CurrencyEnum("LBP");
        
        [ComVisible(true)]
        public static CurrencyEnum Lkr => new CurrencyEnum("LKR");
        
        [ComVisible(true)]
        public static CurrencyEnum Lrd => new CurrencyEnum("LRD");
        
        [ComVisible(true)]
        public static CurrencyEnum Lsl => new CurrencyEnum("LSL");
        
        [ComVisible(true)]
        public static CurrencyEnum Lyd => new CurrencyEnum("LYD");
        
        [ComVisible(true)]
        public static CurrencyEnum Mad => new CurrencyEnum("MAD");
        
        [ComVisible(true)]
        public static CurrencyEnum Mdl => new CurrencyEnum("MDL");
        
        [ComVisible(true)]
        public static CurrencyEnum Mga => new CurrencyEnum("MGA");
        
        [ComVisible(true)]
        public static CurrencyEnum Mkd => new CurrencyEnum("MKD");
        
        [ComVisible(true)]
        public static CurrencyEnum Mmk => new CurrencyEnum("MMK");
        
        [ComVisible(true)]
        public static CurrencyEnum Mnt => new CurrencyEnum("MNT");
        
        [ComVisible(true)]
        public static CurrencyEnum Mop => new CurrencyEnum("MOP");
        
        [ComVisible(true)]
        public static CurrencyEnum Mro => new CurrencyEnum("MRO");
        
        [ComVisible(true)]
        public static CurrencyEnum Mur => new CurrencyEnum("MUR");
        
        [ComVisible(true)]
        public static CurrencyEnum Mvr => new CurrencyEnum("MVR");
        
        [ComVisible(true)]
        public static CurrencyEnum Mwk => new CurrencyEnum("MWK");
        
        [ComVisible(true)]
        public static CurrencyEnum Mxn => new CurrencyEnum("MXN");
        
        [ComVisible(true)]
        public static CurrencyEnum Mxv => new CurrencyEnum("MXV");
        
        [ComVisible(true)]
        public static CurrencyEnum Myr => new CurrencyEnum("MYR");
        
        [ComVisible(true)]
        public static CurrencyEnum Mzn => new CurrencyEnum("MZN");
        
        [ComVisible(true)]
        public static CurrencyEnum Nad => new CurrencyEnum("NAD");
        
        [ComVisible(true)]
        public static CurrencyEnum Ngn => new CurrencyEnum("NGN");
        
        [ComVisible(true)]
        public static CurrencyEnum Nio => new CurrencyEnum("NIO");
        
        [ComVisible(true)]
        public static CurrencyEnum Nok => new CurrencyEnum("NOK");
        
        [ComVisible(true)]
        public static CurrencyEnum Npr => new CurrencyEnum("NPR");
        
        [ComVisible(true)]
        public static CurrencyEnum Nzd => new CurrencyEnum("NZD");
        
        [ComVisible(true)]
        public static CurrencyEnum Omr => new CurrencyEnum("OMR");
        
        [ComVisible(true)]
        public static CurrencyEnum Pab => new CurrencyEnum("PAB");
        
        [ComVisible(true)]
        public static CurrencyEnum Pen => new CurrencyEnum("PEN");
        
        [ComVisible(true)]
        public static CurrencyEnum Pgk => new CurrencyEnum("PGK");
        
        [ComVisible(true)]
        public static CurrencyEnum Php => new CurrencyEnum("PHP");
        
        [ComVisible(true)]
        public static CurrencyEnum Pkr => new CurrencyEnum("PKR");
        
        [ComVisible(true)]
        public static CurrencyEnum Pln => new CurrencyEnum("PLN");
        
        [ComVisible(true)]
        public static CurrencyEnum Pyg => new CurrencyEnum("PYG");
        
        [ComVisible(true)]
        public static CurrencyEnum Qar => new CurrencyEnum("QAR");
        
        [ComVisible(true)]
        public static CurrencyEnum Ron => new CurrencyEnum("RON");
        
        [ComVisible(true)]
        public static CurrencyEnum Rsd => new CurrencyEnum("RSD");
        
        [ComVisible(true)]
        public static CurrencyEnum Rub => new CurrencyEnum("RUB");
        
        [ComVisible(true)]
        public static CurrencyEnum Rwf => new CurrencyEnum("RWF");
        
        [ComVisible(true)]
        public static CurrencyEnum Sar => new CurrencyEnum("SAR");
        
        [ComVisible(true)]
        public static CurrencyEnum Sbd => new CurrencyEnum("SBD");
        
        [ComVisible(true)]
        public static CurrencyEnum Scr => new CurrencyEnum("SCR");
        
        [ComVisible(true)]
        public static CurrencyEnum Sdg => new CurrencyEnum("SDG");
        
        [ComVisible(true)]
        public static CurrencyEnum Sek => new CurrencyEnum("SEK");
        
        [ComVisible(true)]
        public static CurrencyEnum Sgd => new CurrencyEnum("SGD");
        
        [ComVisible(true)]
        public static CurrencyEnum Shp => new CurrencyEnum("SHP");
        
        [ComVisible(true)]
        public static CurrencyEnum Sll => new CurrencyEnum("SLL");
        
        [ComVisible(true)]
        public static CurrencyEnum Sos => new CurrencyEnum("SOS");
        
        [ComVisible(true)]
        public static CurrencyEnum Srd => new CurrencyEnum("SRD");
        
        [ComVisible(true)]
        public static CurrencyEnum Ssp => new CurrencyEnum("SSP");
        
        [ComVisible(true)]
        public static CurrencyEnum Std => new CurrencyEnum("STD");
        
        [ComVisible(true)]
        public static CurrencyEnum Svc => new CurrencyEnum("SVC");
        
        [ComVisible(true)]
        public static CurrencyEnum Syp => new CurrencyEnum("SYP");
        
        [ComVisible(true)]
        public static CurrencyEnum Szl => new CurrencyEnum("SZL");
        
        [ComVisible(true)]
        public static CurrencyEnum Thb => new CurrencyEnum("THB");
        
        [ComVisible(true)]
        public static CurrencyEnum Tjs => new CurrencyEnum("TJS");
        
        [ComVisible(true)]
        public static CurrencyEnum Tmt => new CurrencyEnum("TMT");
        
        [ComVisible(true)]
        public static CurrencyEnum Tnd => new CurrencyEnum("TND");
        
        [ComVisible(true)]
        public static CurrencyEnum Top => new CurrencyEnum("TOP");
        
        [ComVisible(true)]
        public static CurrencyEnum Try => new CurrencyEnum("TRY");
        
        [ComVisible(true)]
        public static CurrencyEnum Ttd => new CurrencyEnum("TTD");
        
        [ComVisible(true)]
        public static CurrencyEnum Twd => new CurrencyEnum("TWD");
        
        [ComVisible(true)]
        public static CurrencyEnum Tzs => new CurrencyEnum("TZS");
        
        [ComVisible(true)]
        public static CurrencyEnum Uah => new CurrencyEnum("UAH");
        
        [ComVisible(true)]
        public static CurrencyEnum Ugx => new CurrencyEnum("UGX");
        
        [ComVisible(true)]
        public static CurrencyEnum Usd => new CurrencyEnum("USD");
        
        [ComVisible(true)]
        public static CurrencyEnum Usn => new CurrencyEnum("USN");
        
        [ComVisible(true)]
        public static CurrencyEnum Uyi => new CurrencyEnum("UYI");
        
        [ComVisible(true)]
        public static CurrencyEnum Uyu => new CurrencyEnum("UYU");
        
        [ComVisible(true)]
        public static CurrencyEnum Uzs => new CurrencyEnum("UZS");
        
        [ComVisible(true)]
        public static CurrencyEnum Vef => new CurrencyEnum("VEF");
        
        [ComVisible(true)]
        public static CurrencyEnum Vnd => new CurrencyEnum("VND");
        
        [ComVisible(true)]
        public static CurrencyEnum Vuv => new CurrencyEnum("VUV");
        
        [ComVisible(true)]
        public static CurrencyEnum Wst => new CurrencyEnum("WST");
        
        [ComVisible(true)]
        public static CurrencyEnum Xaf => new CurrencyEnum("XAF");
        
        [ComVisible(true)]
        public static CurrencyEnum Xag => new CurrencyEnum("XAG");
        
        [ComVisible(true)]
        public static CurrencyEnum Xau => new CurrencyEnum("XAU");
        
        [ComVisible(true)]
        public static CurrencyEnum Xba => new CurrencyEnum("XBA");
        
        [ComVisible(true)]
        public static CurrencyEnum Xbb => new CurrencyEnum("XBB");
        
        [ComVisible(true)]
        public static CurrencyEnum Xbc => new CurrencyEnum("XBC");
        
        [ComVisible(true)]
        public static CurrencyEnum Xbd => new CurrencyEnum("XBD");
        
        [ComVisible(true)]
        public static CurrencyEnum Xcd => new CurrencyEnum("XCD");
        
        [ComVisible(true)]
        public static CurrencyEnum Xdr => new CurrencyEnum("XDR");
        
        [ComVisible(true)]
        public static CurrencyEnum Xof => new CurrencyEnum("XOF");
        
        [ComVisible(true)]
        public static CurrencyEnum Xpd => new CurrencyEnum("XPD");
        
        [ComVisible(true)]
        public static CurrencyEnum Xpf => new CurrencyEnum("XPF");
        
        [ComVisible(true)]
        public static CurrencyEnum Xpt => new CurrencyEnum("XPT");
        
        [ComVisible(true)]
        public static CurrencyEnum Xsu => new CurrencyEnum("XSU");
        
        [ComVisible(true)]
        public static CurrencyEnum Xts => new CurrencyEnum("XTS");
        
        [ComVisible(true)]
        public static CurrencyEnum Xua => new CurrencyEnum("XUA");
        
        [ComVisible(true)]
        public static CurrencyEnum Xxx => new CurrencyEnum("XXX");
        
        [ComVisible(true)]
        public static CurrencyEnum Yer => new CurrencyEnum("YER");
        
        [ComVisible(true)]
        public static CurrencyEnum Zar => new CurrencyEnum("ZAR");
        
        [ComVisible(true)]
        public static CurrencyEnum Zmw => new CurrencyEnum("ZMW");
        
        [ComVisible(true)]
        public static CurrencyEnum Zwl => new CurrencyEnum("ZWL");
        
        /// <inheritdoc />
        private CurrencyEnum(string value) : base(value)
        {
        }
    }
}
