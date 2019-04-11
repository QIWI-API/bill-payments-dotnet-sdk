using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    ///     Invoice currency enum.
    /// </summary>
    [ComVisible(true)]
    public class CurrencyEnum : StringEnum<CurrencyEnum>
    {
        /// <inheritdoc />
        private CurrencyEnum(string value) : base(value)
        {
        }

        /// <summary>
        ///     UAE Dirham (AED).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Aed => new CurrencyEnum("AED");

        /// <summary>
        ///     Afghan Afghani (AFN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Afn => new CurrencyEnum("AFN");

        /// <summary>
        ///     Albanian Lek (ALL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum All => new CurrencyEnum("ALL");

        /// <summary>
        ///     Armenian Dram (AMD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Amd => new CurrencyEnum("AMD");

        /// <summary>
        ///     Netherlands Antillean Guilder (ANG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ang => new CurrencyEnum("ANG");

        /// <summary>
        ///     Angola Kwanza (AOA).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Aoa => new CurrencyEnum("AOA");

        /// <summary>
        ///     Argentine Peso (ARS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ars => new CurrencyEnum("ARS");

        /// <summary>
        ///     Australian Dollar (AUD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Aud => new CurrencyEnum("AUD");

        /// <summary>
        ///     Aruban Florin (AWG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Awg => new CurrencyEnum("AWG");

        /// <summary>
        ///     Azerbaijanian Manat (AZN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Azn => new CurrencyEnum("AZN");

        /// <summary>
        ///     Bosnian Convertible Marka (BAM).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bam => new CurrencyEnum("BAM");

        /// <summary>
        ///     Barbadian Dollars (BBD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bbd => new CurrencyEnum("BBD");

        /// <summary>
        ///     Bangladesh Taka (BDT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bdt => new CurrencyEnum("BDT");

        /// <summary>
        ///     Bulgaria Lev (BGN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bgn => new CurrencyEnum("BGN");

        /// <summary>
        ///     Bahraini Dinar (BHD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bhd => new CurrencyEnum("BHD");

        /// <summary>
        ///     Burundian Franc (BIF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bif => new CurrencyEnum("BIF");

        /// <summary>
        ///     Bermudian Dollar (BMD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bmd => new CurrencyEnum("BMD");

        /// <summary>
        ///     Bruneian Dollar (BND).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bnd => new CurrencyEnum("BND");

        /// <summary>
        ///     Bolivian Bolíviano (BOB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bob => new CurrencyEnum("BOB");

        /// <summary>
        ///     Brazilian Real (BRL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Brl => new CurrencyEnum("BRL");

        /// <summary>
        ///     Bahamian Dollar (BSD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bsd => new CurrencyEnum("BSD");

        /// <summary>
        ///     Bhutanese Ngultrum (BTN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Btn => new CurrencyEnum("BTN");

        /// <summary>
        ///     Botswana Pula (BWP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bwp => new CurrencyEnum("BWP");

        /// <summary>
        ///     Belarusian Ruble (BYR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Byr => new CurrencyEnum("BYR");

        /// <summary>
        ///     Belizean Dollar (BZD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Bzd => new CurrencyEnum("BZD");

        /// <summary>
        ///     Canadian Dollar (CAD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cad => new CurrencyEnum("CAD");

        /// <summary>
        ///     Congolese Franc (CDF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cdf => new CurrencyEnum("CDF");

        /// <summary>
        ///     Swiss Franc (CHF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Chf => new CurrencyEnum("CHF");

        /// <summary>
        ///     Chilean Peso (CLP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Clp => new CurrencyEnum("CLP");

        /// <summary>
        ///     Chinese Yuan (CNY).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cny => new CurrencyEnum("CNY");

        /// <summary>
        ///     Colombian Peso (COP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cop => new CurrencyEnum("COP");

        /// <summary>
        ///     Costa Rican Colon (CRC).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Crc => new CurrencyEnum("CRC");

        /// <summary>
        ///     Cuban Convertible (CUC).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cuc => new CurrencyEnum("CUC");

        /// <summary>
        ///     Cuban Peso (CUP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cup => new CurrencyEnum("CUP");

        /// <summary>
        ///     Cape Verdean Escudo (CVE).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Cve => new CurrencyEnum("CVE");

        /// <summary>
        ///     Czech Koruna (CZK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Czk => new CurrencyEnum("CZK");

        /// <summary>
        ///     Djiboutian Franc (DJF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Djf => new CurrencyEnum("DJF");

        /// <summary>
        ///     Danish Krone (DKK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Dkk => new CurrencyEnum("DKK");

        /// <summary>
        ///     Dominican Peso (DOP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Dop => new CurrencyEnum("DOP");

        /// <summary>
        ///     Algerian Dinar (DZD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Dzd => new CurrencyEnum("DZD");

        /// <summary>
        ///     Egyptian Pound (EGP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Egp => new CurrencyEnum("EGP");

        /// <summary>
        ///     Eritrean Nakfa (ERN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ern => new CurrencyEnum("ERN");

        /// <summary>
        ///     Ethiopian Birr (ETB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Etb => new CurrencyEnum("ETB");

        /// <summary>
        ///     Euro (EUR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Eur => new CurrencyEnum("EUR");

        /// <summary>
        ///     Fijian Dollar (FJD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Fjd => new CurrencyEnum("FJD");

        /// <summary>
        ///     Falkland Island Pound (FKP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Fkp => new CurrencyEnum("FKP");

        /// <summary>
        ///     Great British Pound (GBP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gbp => new CurrencyEnum("GBP");

        /// <summary>
        ///     Georgian Lari (GEL)
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gel => new CurrencyEnum("GEL");

        /// <summary>
        ///     Guernsey Pound (GGP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ggp => new CurrencyEnum("GGP");

        /// <summary>
        ///     Ghanaian Cedi (GHS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ghs => new CurrencyEnum("GHS");

        /// <summary>
        ///     Gibraltar Pound (GIP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gip => new CurrencyEnum("GIP");

        /// <summary>
        ///     Gambian Dalasi (GMD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gmd => new CurrencyEnum("GMD");

        /// <summary>
        ///     Guinean Franc (GNF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gnf => new CurrencyEnum("GNF");

        /// <summary>
        ///     Guatemalan Quetzal (GTQ).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gtq => new CurrencyEnum("GTQ");

        /// <summary>
        ///     Guyanese Dollar (GYD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Gyd => new CurrencyEnum("GYD");

        /// <summary>
        ///     Hong Kong Dollar (HKD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Hkd => new CurrencyEnum("HKD");

        /// <summary>
        ///     Honduran Lempira (HNL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Hnl => new CurrencyEnum("HNL");

        /// <summary>
        ///     Croatian Kuna (HRK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Hrk => new CurrencyEnum("HRK");

        /// <summary>
        ///     Haitian Gourde (HTG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Htg => new CurrencyEnum("HTG");

        /// <summary>
        ///     Hungarian Forint (HUF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Huf => new CurrencyEnum("HUF");

        /// <summary>
        ///     Indonesian Rupiah (IDR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Idr => new CurrencyEnum("IDR");

        /// <summary>
        ///     Israeli Shekel (ILS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ils => new CurrencyEnum("ILS");

        /// <summary>
        ///     Isle of Man Pound (IMP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Imp => new CurrencyEnum("IMP");

        /// <summary>
        ///     Indian Rupee (INR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Inr => new CurrencyEnum("INR");

        /// <summary>
        ///     Iraqi Dinar (IQD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Iqd => new CurrencyEnum("IQD");

        /// <summary>
        ///     Iranian Rial (IRR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Irr => new CurrencyEnum("IRR");

        /// <summary>
        ///     Icelandic Krona (ISK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Isk => new CurrencyEnum("ISK");

        /// <summary>
        ///     Jersey Pound (JEP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Jep => new CurrencyEnum("JEP");

        /// <summary>
        ///     Jamaican Dollar (JMD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Jmd => new CurrencyEnum("JMD");

        /// <summary>
        ///     Jordanian Dinar (JOD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Jod => new CurrencyEnum("JOD");

        /// <summary>
        ///     Japanese Yen (JPY).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Jpy => new CurrencyEnum("JPY");

        /// <summary>
        ///     Kenyan Shilling (KES).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kes => new CurrencyEnum("KES");

        /// <summary>
        ///     Kyrgyzstani Som (KGS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kgs => new CurrencyEnum("KGS");

        /// <summary>
        ///     Cambodian Riel (KHR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Khr => new CurrencyEnum("KHR");

        /// <summary>
        ///     Comoran Franc (KMF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kmf => new CurrencyEnum("KMF");

        /// <summary>
        ///     North Korean Won (KPW).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kpw => new CurrencyEnum("KPW");

        /// <summary>
        ///     South Korean Won (KRW).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Krw => new CurrencyEnum("KRW");

        /// <summary>
        ///     Kuwaiti Dinar (KWD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kwd => new CurrencyEnum("KWD");

        /// <summary>
        ///     Caymanian Dollar (KYD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kyd => new CurrencyEnum("KYD");

        /// <summary>
        ///     Kazakhstani Tenge (KZT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Kzt => new CurrencyEnum("KZT");

        /// <summary>
        ///     Laotian Kip (LAK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lak => new CurrencyEnum("LAK");

        /// <summary>
        ///     Lebanese Pound (LBP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lbp => new CurrencyEnum("LBP");

        /// <summary>
        ///     Sri Lankan Rupee (LKR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lkr => new CurrencyEnum("LKR");

        /// <summary>
        ///     Liberian Dollar (LRD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lrd => new CurrencyEnum("LRD");

        /// <summary>
        ///     Basotho Loti (LSL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lsl => new CurrencyEnum("LSL");

        /// <summary>
        ///     Lithuanian Litas (LTL)
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ltl => new CurrencyEnum("LTL");

        /// <summary>
        ///     Latvian Lat (LVL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lvl => new CurrencyEnum("LVL");

        /// <summary>
        ///     Libyan Dinar (LYD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Lyd => new CurrencyEnum("LYD");

        /// <summary>
        ///     Moroccan Dirham (MAD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mad => new CurrencyEnum("MAD");

        /// <summary>
        ///     Moldovan Leu (MDL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mdl => new CurrencyEnum("MDL");

        /// <summary>
        ///     Malagasy Ariary (MGA).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mga => new CurrencyEnum("MGA");

        /// <summary>
        ///     Macedonian Denar (MKD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mkd => new CurrencyEnum("MKD");

        /// <summary>
        ///     Burmese Kyat (MMK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mmk => new CurrencyEnum("MMK");

        /// <summary>
        ///     Mongolian Tughrik (MNT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mnt => new CurrencyEnum("MNT");

        /// <summary>
        ///     Macau Pataca (MOP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mop => new CurrencyEnum("MOP");

        /// <summary>
        ///     Mauritanian Ouguiya (MRO).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mro => new CurrencyEnum("MRO");

        /// <summary>
        ///     Mauritian Rupee (MUR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mur => new CurrencyEnum("MUR");

        /// <summary>
        ///     Maldivian Rufiyaa (MVR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mvr => new CurrencyEnum("MVR");

        /// <summary>
        ///     Malawian Kwacha (MWK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mwk => new CurrencyEnum("MWK");

        /// <summary>
        ///     Mexican Peso (MXN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mxn => new CurrencyEnum("MXN");

        /// <summary>
        ///     Malaysian Ringgit (MYR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Myr => new CurrencyEnum("MYR");

        /// <summary>
        ///     Mozambican Metical (MZN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Mzn => new CurrencyEnum("MZN");

        /// <summary>
        ///     Namibian Dollar (NAD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Nad => new CurrencyEnum("NAD");

        /// <summary>
        ///     Nigerian Naira (NGN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ngn => new CurrencyEnum("NGN");

        /// <summary>
        ///     Nicaraguan Cordoba (NIO).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Nio => new CurrencyEnum("NIO");

        /// <summary>
        ///     Norwegian Krone (NOK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Nok => new CurrencyEnum("NOK");

        /// <summary>
        ///     Nepalese Rupee (NPR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Npr => new CurrencyEnum("NPR");

        /// <summary>
        ///     New Zealand Dollar (NZD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Nzd => new CurrencyEnum("NZD");

        /// <summary>
        ///     Omani Rial (OMR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Omr => new CurrencyEnum("OMR");

        /// <summary>
        ///     Panamanian Balboa (PAB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pab => new CurrencyEnum("PAB");

        /// <summary>
        ///     Peruvian Sol (PEN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pen => new CurrencyEnum("PEN");

        /// <summary>
        ///     Papua New Guinean Kina (PGK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pgk => new CurrencyEnum("PGK");

        /// <summary>
        ///     Philippine Peso (PHP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Php => new CurrencyEnum("PHP");

        /// <summary>
        ///     Pakistani Rupee (PKR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pkr => new CurrencyEnum("PKR");

        /// <summary>
        ///     Polish Zloty (PLN).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pln => new CurrencyEnum("PLN");

        /// <summary>
        ///     Paraguayan Guarani (PYG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Pyg => new CurrencyEnum("PYG");

        /// <summary>
        ///     Qatari Riyal (QAR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Qar => new CurrencyEnum("QAR");

        /// <summary>
        ///     Romanian New Leu (RON).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ron => new CurrencyEnum("RON");

        /// <summary>
        ///     Serbian Dinar (RSD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Rsd => new CurrencyEnum("RSD");

        /// <summary>
        ///     Russian Ruble (RUB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Rub => new CurrencyEnum("RUB");

        /// <summary>
        ///     Rwandan Franc (RWF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Rwf => new CurrencyEnum("RWF");

        /// <summary>
        ///     Saudi Arabian Riyal (SAR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sar => new CurrencyEnum("SAR");

        /// <summary>
        ///     Solomon Islander Dollar (SBD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sbd => new CurrencyEnum("SBD");

        /// <summary>
        ///     Seychellois Rupee (SCR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Scr => new CurrencyEnum("SCR");

        /// <summary>
        ///     Sudanese Pound (SDG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sdg => new CurrencyEnum("SDG");

        /// <summary>
        ///     Swedish Krona (SEK).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sek => new CurrencyEnum("SEK");

        /// <summary>
        ///     Singapore Dollar (SGD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sgd => new CurrencyEnum("SGD");

        /// <summary>
        ///     Saint Helenian Pound (SHP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Shp => new CurrencyEnum("SHP");

        /// <summary>
        ///     Leonean Leone (SLL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sll => new CurrencyEnum("SLL");

        /// <summary>
        ///     Somali Shilling (SOS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Sos => new CurrencyEnum("SOS");

        /// <summary>
        ///     Seborgan Luigino (SPL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Spl => new CurrencyEnum("SPL");

        /// <summary>
        ///     Surinamese Dollar (SRD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Srd => new CurrencyEnum("SRD");

        /// <summary>
        ///     Sao Tomean Dobra (STD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Std => new CurrencyEnum("STD");

        /// <summary>
        ///     Salvadoran Colon (SVC).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Svc => new CurrencyEnum("SVC");

        /// <summary>
        ///     Syrian Pound (SYP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Syp => new CurrencyEnum("SYP");

        /// <summary>
        ///     Swazi Lilangeni (SZL).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Szl => new CurrencyEnum("SZL");

        /// <summary>
        ///     Thai Baht (THB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Thb => new CurrencyEnum("THB");

        /// <summary>
        ///     Tajikistani Somoni (TJS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Tjs => new CurrencyEnum("TJS");

        /// <summary>
        ///     Turkmenistani Manat (TMT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Tmt => new CurrencyEnum("TMT");

        /// <summary>
        ///     Tunisian Dinar (TND).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Tnd => new CurrencyEnum("TND");

        /// <summary>
        ///     Tongan Pa'anga (TOP).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Top => new CurrencyEnum("TOP");

        /// <summary>
        ///     Turkish Lira (TRY).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Try => new CurrencyEnum("TRY");

        /// <summary>
        ///     Trinidadian Dollar (TTD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ttd => new CurrencyEnum("TTD");

        /// <summary>
        ///     Tuvaluan Dollar (TVD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Tvd => new CurrencyEnum("TVD");

        /// <summary>
        ///     Taiwan New Dollar (TWD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Twd => new CurrencyEnum("TWD");

        /// <summary>
        ///     Tanzanian Shilling (TZS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Tzs => new CurrencyEnum("TZS");

        /// <summary>
        ///     Ukrainian Hryvnia (UAH).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Uah => new CurrencyEnum("UAH");

        /// <summary>
        ///     Ugandan Shilling (UGX).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Ugx => new CurrencyEnum("UGX");

        /// <summary>
        ///     US Dollar (USD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Usd => new CurrencyEnum("USD");

        /// <summary>
        ///     Uruguayan Peso (UYU).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Uyu => new CurrencyEnum("UYU");

        /// <summary>
        ///     Uzbekistani Som (UZS).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Uzs => new CurrencyEnum("UZS");

        /// <summary>
        ///     Venezuelan Bolivar (VEB).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Veb => new CurrencyEnum("VEB");

        /// <summary>
        ///     Venezuelan Bolivar (VEF).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Vef => new CurrencyEnum("VEF");

        /// <summary>
        ///     Vietnamese Dong (VND).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Vnd => new CurrencyEnum("VND");

        /// <summary>
        ///     Ni-Vanuatu Vatu (VUV).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Vuv => new CurrencyEnum("VUV");

        /// <summary>
        ///     Samoan Tala (WST).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Wst => new CurrencyEnum("WST");

        /// <summary>
        ///     Bitcoin (XBT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Xbt => new CurrencyEnum("XBT");

        /// <summary>
        ///     Silver Ounce (XAG).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Xag => new CurrencyEnum("XAG");

        /// <summary>
        ///     Gold Ounce (XAU).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Xau => new CurrencyEnum("XAU");

        /// <summary>
        ///     Palladium Ounce (XPD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Xpd => new CurrencyEnum("XPD");

        /// <summary>
        ///     Platinum Ounce (XPT).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Xpt => new CurrencyEnum("XPT");

        /// <summary>
        ///     Yemeni Rial (YER).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Yer => new CurrencyEnum("YER");

        /// <summary>
        ///     South African Rand (ZAR).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Zar => new CurrencyEnum("ZAR");

        /// <summary>
        ///     Zambian Kwacha (ZMW).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Zmw => new CurrencyEnum("ZMW");

        /// <summary>
        ///     Zimbabwean Dollar (ZWD).
        /// </summary>
        [ComVisible(true)]
        public static CurrencyEnum Zwd => new CurrencyEnum("ZWD");
    }
}