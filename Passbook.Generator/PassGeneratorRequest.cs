using Passbook.Generator.Exceptions;
using Passbook.Generator.Fields;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Passbook.Generator;

public class PassGeneratorRequest
{
    public PassGeneratorRequest()
    {
        SemanticTags = [];
        HeaderFields = [];
        PrimaryFields = [];
        SecondaryFields = [];
        AuxiliaryFields = [];
        BackFields = [];
        Images = [];
        RelevantDates = [];
        RelevantLocations = [];
        RelevantBeacons = [];
        AssociatedStoreIdentifiers = [];
        Localizations = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        Barcodes = [];
        UserInfo = new Dictionary<string, object>();
        PreferredStyleSchemes = [];
    }

    #region Standard Keys

    /// <summary>
    /// Required. Pass type identifier, as issued by Apple. The value must correspond with your signing certificate.
    /// </summary>
    public string PassTypeIdentifier { get; set; }

    /// <summary>
    /// Required. Version of the file format. The value must be 1.
    /// </summary>
    public int FormatVersion { get { return 1; } }

    /// <summary>
    /// Required. Serial number that uniquely identifies the pass. No two passes with the same pass type identifier may have the same serial number.
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// Required. A simple description of the pass
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Required. Team identifier of the organization that originated and signed the pass, as issued by Apple.
    /// </summary>
    public string TeamIdentifier { get; set; }

    /// <summary>
    /// Required. Display name of the organization that originated and signed the pass.
    /// </summary>
    public string OrganizationName { get; set; }

    /// <summary>
    /// Disables sharing of the pass.
    /// </summary>
    public bool SharingProhibited { get; set; }

    /// <summary>
    /// Used to render an iOS application associated with the event in the event guide. 
    /// This application has no ability to read passes off the device through passkit.
    /// </summary>
    public int AuxiliaryStoreIdentifiers { get; set; }

    /// <summary>
    /// Used to turn off the top gradient that is automatically applied to all passes.
    /// </summary>
    public bool SuppressHeaderDarkening { get; set; }

    /// <summary>
    /// Indicates whether the pass faces should automatically determine the foreground/labelColor from the background image. 
    /// Ignores the colors specified by foregroundColor and labelColor.
    /// </summary>
    public bool UseAutomaticColors { get; set; }

    /// <summary>
    /// A background color to override the ticket footer (where seat information is located). 
    /// Specified as a CSS-style RGB triple, such as rgb(23, 187, 82).
    /// </summary>
    public string FooterBackgroundColor { get; set; }

    /// <summary>
    /// A URL that launches the user into the issuer's flow for selling their current ticket. 
    /// We recommend providing as deep a link as possible into the sale flow.
    /// </summary>
    public string SellURL { get; set; }

    /// <summary>
    /// A URL that launches the user into the issuer's flow for transferring the current ticket. 
    /// We recommend providing as deep a link as possible into the transfer flow.
    /// </summary>
    public string TransferURL { get; set; }

    /// <summary>
    /// A URL that links out to the bag policy of the venue.
    /// </summary>
    public string BagPolicyURL { get; set; }

    /// <summary>
    /// A URL that links out to the food ordering page for the venue. 
    /// This can be in-seat food delivery, pre-order for pickup at a vendor, or any other appropriate food ordering service.
    /// </summary>
    public string OrderFoodURL { get; set; }

    /// <summary>
    /// A URL that links to order merchandise for the specific event. 
    /// This can be a ship-to-home eCommerce site, a pre-order to pick up at the venue, or any other appropriate merchandise flow. 
    /// This link can also be updated throughout the user's journey to provide more accurately tailored links at certain times 
    /// (for instance before vs. after a user has entered an event). This can be done through a pass update, 
    /// which is referenced in the previous documentation for tickets.
    /// </summary>
    public string MerchandiseURL { get; set; }

    /// <summary>
    /// A URL that links to any documentation you have about public or private transit to the venue.
    /// </summary>
    public string TransitInformationURL { get; set; }

    /// <summary>
    /// A URL that links to any information you have about parking.
    /// </summary>
    public string ParkingInformationURL { get; set; }

    /// <summary>
    /// A URL that links to any content you have about getting to the venue.
    /// </summary>
    public string DirectionsInformationURL { get; set; }

    /// <summary>
    /// A URL that links to your or the venue's accessibility content.
    /// </summary>
    public string AccessibilityURL { get; set; }

    /// <summary>
    /// A URL that links to your experience to buy or access pre-paid parking, or general parking information.
    /// </summary>
    public string PurchaseParkingURL { get; set; }

    /// <summary>
    /// A URL that can link to experiences that you can add on to your ticket (e.g. loaded value, upgrades, etc.), 
    /// or access your existing pre-purchased or pre-loaded add-on experiences, including any necessary QR or barcode links to access the experience.
    /// </summary>
    public string AddOnURL { get; set; }

    /// <summary>
    /// The preferred email address to contact the venue, event, or issuer.
    /// </summary>
    public string ContactVenueEmail { get; set; }

    /// <summary>
    /// A URL that links the user to the website of the venue, event, or issuer.
    /// </summary>
    public string ContactVenueWebsite { get; set; }

    /// <summary>
    /// The phone number that can be used to contact the venue, event, or issuer.
    /// </summary>
    public string ContactVenuePhoneNumber { get; set; }

    #endregion

    #region Images Files

    /// <summary>
    /// When using in memory, the binary of each image is put here.
    /// </summary>
    public Dictionary<PassbookImage, byte[]> Images { get; set; }

    #endregion

    #region Companion App Keys

    #endregion

    #region Expiration Keys

    public DateTimeOffset? ExpirationDate { get; set; }

    public bool? Voided { get; set; }

    #endregion

    #region Visual Appearance Keys

    /// <summary>
    /// Optional. Foreground color of the pass, specified as a CSS-style RGB triple. For example, rgb(100, 10, 110).
    /// </summary>
    public string ForegroundColor { get; set; }

    /// <summary>
    /// Optional. Background color of the pass, specified as an CSS-style RGB triple. For example, rgb(23, 187, 82).
    /// </summary>
    public string BackgroundColor { get; set; }

    /// <summary>
    /// Optional. Color of the label text, specified as a CSS-style RGB triple. For example, rgb(255, 255, 255).
    /// If omitted, the label color is determined automatically.
    /// </summary>
    public string LabelColor { get; set; }

    /// <summary>
    /// Optional. Text displayed next to the logo on the pass.
    /// </summary>
    public string LogoText { get; set; }

    /// <summary>
    /// Optional. If true, the strip image is displayed without a shine effect. The default value is false.
    /// </summary>
    public bool? SuppressStripShine { get; set; }

    /// <summary>
    /// Optional. The semantic tags to add to the pass. Read more about them here https://developer.apple.com/documentation/walletpasses/semantictags
    /// </summary>
    public SemanticTags SemanticTags { get; }

    /// <summary>
    /// Optional. Fields to be displayed prominently on the front of the pass.
    /// </summary>
    public List<Field> HeaderFields { get; private set; }

    /// <summary>
    /// Optional. Fields to be displayed prominently on the front of the pass.
    /// </summary>
    public List<Field> PrimaryFields { get; private set; }

    /// <summary>
    /// Optional. Fields to be displayed on the front of the pass.
    /// </summary>
    public List<Field> SecondaryFields { get; private set; }

    /// <summary>
    /// Optional. Additional fields to be displayed on the front of the pass.
    /// </summary>
    public List<Field> AuxiliaryFields { get; private set; }

    /// <summary>
    /// Optional. Information about fields that are displayed on the back of the pass.
    /// </summary>
    public List<Field> BackFields { get; private set; }

    /// <summary>
    /// Optional. Information specific to barcodes.
    /// </summary>
    public Barcode Barcode { get; private set; }

    /// <summary>
    /// Required. Pass type.
    /// </summary>
    public PassStyle Style { get; set; }

    /// <summary>
    /// Required for boarding passes; otherwise not allowed. Type of transit.
    /// </summary>
    public TransitType TransitType { get; set; }

    /// <summary>
    /// Optional for event tickets and boarding passes; otherwise not allowed. Identifier used to group related passes
    /// </summary>
    public string GroupingIdentifier { get; set; }

    /// <summary>
    /// Style schemes provide a new way to assign pass type in a more dynamic nature. The strings 
    /// correspond to "schemes" that the internally get resolved into a style.
    /// </summary>
    public List<PreferredStyleScheme> PreferredStyleSchemes { get; }

    #endregion

    #region Relevance Keys

    /// <summary>
    /// Optional. Date and time when the pass becomes relevant. For example, the start time of a movie.
    /// </summary>
    [Obsolete("Use RevevantDates with only one parameter.")]
    public DateTimeOffset? RelevantDate { get; set; }

    /// <summary>
    /// Optional. A list of time spans where the pass is relevant. No more than 10 entries can be specified.
    /// </summary>
    public List<RelevantDate> RelevantDates { get; }

    /// <summary>
    /// Optional. Locations where the passisrelevant. For example, the location of your store.
    /// </summary>
    public List<RelevantLocation> RelevantLocations { get; private set; }

    /// <summary>
    /// Optional. Beacons marking locations where the pass is relevant.
    /// </summary>
    public List<RelevantBeacon> RelevantBeacons { get; private set; }

    /// <summary>
    /// Optional. Maximum distance in meters from a relevant latitude and longitude that the pass is relevant
    /// </summary>
    public int? MaxDistance { get; set; }

    #endregion

    #region Certificates

    /// <summary>
    /// A byte array containing the PassKit certificate
    /// </summary>
    public X509Certificate2 PassbookCertificate { get; set; }

    /// <summary>
    /// A byte array containing the Apple WWDRCA X509 certificate
    /// </summary>
    public X509Certificate2 AppleWWDRCACertificate { get; set; }

    #endregion

    #region Web Service Keys

    /// <summary>
    /// The authentication token to use with the web service.
    /// </summary>
    public string AuthenticationToken { get; set; }
    /// <summary>
    /// The URL of a web service that conforms to the API described in Pass Kit Web Service Reference.
    /// The web service must use the HTTPS protocol and includes the leading https://.
    /// On devices configured for development, there is UI in Settings to allow HTTP web services.
    /// </summary>
    public string WebServiceUrl { get; set; }

    #endregion

    #region Associated App Keys

    public List<long> AssociatedStoreIdentifiers { get; set; }

    public string AppLaunchURL { get; set; }

    #endregion

    #region Barcodes

    public List<Barcode> Barcodes { get; private set; }

    #endregion

    #region User Info Keys

    public IDictionary<string, object> UserInfo { get; set; }

    #endregion

    #region Localization
    public Dictionary<string, Dictionary<string, string>> Localizations { get; set; }
    #endregion

    #region NFC

    public Nfc Nfc { get; set; }

    #endregion

    #region Helpers and Serialization

    public void AddHeaderField(Field field)
    {
        EnsureFieldKeyIsUnique(field.Key);
        HeaderFields.Add(field);
    }

    public void AddPrimaryField(Field field)
    {
        EnsureFieldKeyIsUnique(field.Key);
        PrimaryFields.Add(field);
    }

    public void AddSecondaryField(Field field)
    {
        EnsureFieldKeyIsUnique(field.Key);
        SecondaryFields.Add(field);
    }

    public void AddAuxiliaryField(Field field)
    {
        EnsureFieldKeyIsUnique(field.Key);
        AuxiliaryFields.Add(field);
    }

    public void AddBackField(Field field)
    {
        EnsureFieldKeyIsUnique(field.Key);
        BackFields.Add(field);
    }

    private void EnsureFieldKeyIsUnique(string key)
    {
        if (HeaderFields.Any(x => x.Key == key) ||
            PrimaryFields.Any(x => x.Key == key) ||
            SecondaryFields.Any(x => x.Key == key) ||
            AuxiliaryFields.Any(x => x.Key == key) ||
            BackFields.Any(x => x.Key == key))
        {
            throw new DuplicateFieldKeyException(key);
        }
    }

    public void AddBarcode(BarcodeType type, string message, string encoding, string alternateText)
    {
        Barcodes.Add(new Barcode(type, message, encoding, alternateText));
    }

    public void AddBarcode(BarcodeType type, string message, string encoding)
    {
        Barcodes.Add(new Barcode(type, message, encoding));
    }

    public void SetBarcode(BarcodeType type, string message, string encoding, string alternateText = null)
    {
        Barcode = new Barcode(type, message, encoding, alternateText);
    }

    public void AddLocation(double latitude, double longitude)
    {
        AddLocation(latitude, longitude, null);
    }

    public void AddLocation(double latitude, double longitude, string relevantText)
    {
        RelevantLocations.Add(new RelevantLocation() { Latitude = latitude, Longitude = longitude, RelevantText = relevantText });
    }

    public void AddBeacon(string proximityUUID, string relevantText)
    {
        RelevantBeacons.Add(new RelevantBeacon() { ProximityUUID = proximityUUID, RelevantText = relevantText });
    }

    public void AddBeacon(string proximityUUID, string relevantText, int major)
    {
        RelevantBeacons.Add(new RelevantBeacon() { ProximityUUID = proximityUUID, RelevantText = relevantText, Major = major });
    }

    public void AddBeacon(string proximityUUID, string relevantText, int major, int minor)
    {
        RelevantBeacons.Add(new RelevantBeacon() { ProximityUUID = proximityUUID, RelevantText = relevantText, Major = major, Minor = minor });
    }

    public void AddLocalization(string languageCode, string key, string value)
    {
        Dictionary<string, string> values;

        if (!Localizations.TryGetValue(languageCode, out values))
        {
            values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Localizations.Add(languageCode, values);
        }

        values[key] = value;
    }

    public virtual void PopulateFields()
    {
        // NO OP.
    }

    public void Write(Utf8JsonWriter writer)
    {
        PopulateFields();

        writer.WriteStartObject();

        Trace.TraceInformation("Writing semantics..");
        WriteSemantics(writer);
        Trace.TraceInformation("Writing standard keys..");
        WriteStandardKeys(writer);
        Trace.TraceInformation("Writing user information..");
        WriteUserInfo(writer);
        Trace.TraceInformation("Writing relevance keys..");
        WriteRelevanceKeys(writer);
        Trace.TraceInformation("Writing appearance keys..");
        WriteAppearanceKeys(writer);
        Trace.TraceInformation("Writing expiration keys..");
        WriteExpirationKeys(writer);
        Trace.TraceInformation("Writing barcode keys..");
        WriteBarcodes(writer);

        if (Nfc != null)
        {
            Trace.TraceInformation("Writing NFC fields");
            WriteNfcKeys(writer);
        }

        Trace.TraceInformation("Opening style section..");
        OpenStyleSpecificKey(writer);

        Trace.TraceInformation("Writing header fields");
        WriteSection(writer, "headerFields", HeaderFields);
        Trace.TraceInformation("Writing primary fields");
        WriteSection(writer, "primaryFields", PrimaryFields);
        Trace.TraceInformation("Writing secondary fields");
        WriteSection(writer, "secondaryFields", SecondaryFields);
        Trace.TraceInformation("Writing auxiliary fields");
        WriteSection(writer, "auxiliaryFields", AuxiliaryFields);
        Trace.TraceInformation("Writing back fields");
        WriteSection(writer, "backFields", BackFields);

        if (Style == PassStyle.BoardingPass)
        {
            writer.WritePropertyName("transitType");
            writer.WriteStringValue(TransitType.ToString());
        }

        Trace.TraceInformation("Closing style section..");
        CloseStyleSpecificKey(writer);
        
        WritePreferredStyleSchemes(writer);

        WriteBarcode(writer);
        WriteUrls(writer);

        writer.WriteEndObject();
    }

    private void WriteRelevanceKeys(Utf8JsonWriter writer)
    {
        if (RelevantDate.HasValue)
        {
            writer.WritePropertyName("relevantDate");
            writer.WriteStringValue(RelevantDate.Value.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }

        if (RelevantDates.Count > 0)
        {
            writer.WritePropertyName("relevantDates");
            writer.WriteStartArray();

            foreach (var relevantDate in RelevantDates)
            {
                relevantDate.Write(writer);
            }

            writer.WriteEndArray();
        }

        if (MaxDistance.HasValue)
        {
            writer.WritePropertyName("maxDistance");
            writer.WriteStringValue(MaxDistance.Value.ToString());
        }

        if (RelevantLocations.Count > 0)
        {
            writer.WritePropertyName("locations");
            writer.WriteStartArray();

            foreach (var location in RelevantLocations)
            {
                location.Write(writer);
            }

            writer.WriteEndArray();
        }

        if (RelevantBeacons.Count > 0)
        {
            writer.WritePropertyName("beacons");
            writer.WriteStartArray();

            foreach (var beacon in RelevantBeacons)
            {
                beacon.Write(writer);
            }

            writer.WriteEndArray();
        }
    }

    private void WriteUrls(Utf8JsonWriter writer)
    {
        if (!string.IsNullOrEmpty(AuthenticationToken))
        {
            writer.WritePropertyName("authenticationToken");
            writer.WriteStringValue(AuthenticationToken);
            writer.WritePropertyName("webServiceURL");
            writer.WriteStringValue(WebServiceUrl);
        }
    }

    private void WriteBarcode(Utf8JsonWriter writer)
    {
        if (Barcode != null)
        {
            writer.WritePropertyName("barcode");
            Barcode.Write(writer);
        }
    }

    private void WriteBarcodes(Utf8JsonWriter writer)
    {
        if (Barcodes.Count > 0)
        {
            writer.WritePropertyName("barcodes");
            writer.WriteStartArray();

            foreach (var barcode in Barcodes)
            {
                barcode.Write(writer);
            }

            writer.WriteEndArray();
        }
    }

    private void WriteSemantics(Utf8JsonWriter writer)
    {
        SemanticTags.Write(writer);
    }

    private void WriteStandardKeys(Utf8JsonWriter writer)
    {
        writer.WritePropertyName("passTypeIdentifier");
        writer.WriteStringValue(PassTypeIdentifier);

        writer.WritePropertyName("formatVersion");
        writer.WriteNumberValue(FormatVersion);

        writer.WritePropertyName("serialNumber");
        writer.WriteStringValue(SerialNumber);

        writer.WritePropertyName("description");
        writer.WriteStringValue(Description);

        writer.WritePropertyName("organizationName");
        writer.WriteStringValue(OrganizationName);

        writer.WritePropertyName("teamIdentifier");
        writer.WriteStringValue(TeamIdentifier);

        writer.WritePropertyName("sharingProhibited");
        writer.WriteBooleanValue(SharingProhibited);

        writer.WritePropertyName("auxiliaryStoreIdentifiers");
        writer.WriteNumberValue(AuxiliaryStoreIdentifiers);

        writer.WritePropertyName("suppressHeaderDarkening");
        writer.WriteBooleanValue(SuppressHeaderDarkening);

        writer.WritePropertyName("useAutomaticColors");
        writer.WriteBooleanValue(UseAutomaticColors);

        writer.WritePropertyName("footerBackgroundColor");
        writer.WriteStringValue(FooterBackgroundColor);

        writer.WritePropertyName("sellURL");
        writer.WriteStringValue(SellURL);

        writer.WritePropertyName("transferURL");
        writer.WriteStringValue(TransferURL);

        writer.WritePropertyName("bagPolicyURL");
        writer.WriteStringValue(BagPolicyURL);

        writer.WritePropertyName("orderFoodURL");
        writer.WriteStringValue(OrderFoodURL);

        writer.WritePropertyName("merchandiseURL");
        writer.WriteStringValue(MerchandiseURL);

        writer.WritePropertyName("transitInformationURL");
        writer.WriteStringValue(TransitInformationURL);

        writer.WritePropertyName("parkingInformationURL");
        writer.WriteStringValue(ParkingInformationURL);

        writer.WritePropertyName("directionsInformationURL");
        writer.WriteStringValue(DirectionsInformationURL);

        writer.WritePropertyName("accessibilityURL");
        writer.WriteStringValue(AccessibilityURL);

        writer.WritePropertyName("purchaseParkingURL");
        writer.WriteStringValue(PurchaseParkingURL);

        writer.WritePropertyName("addOnURL");
        writer.WriteStringValue(AddOnURL);

        writer.WritePropertyName("contactVenueEmail");
        writer.WriteStringValue(ContactVenueEmail);

        writer.WritePropertyName("contactVenueWebsite");
        writer.WriteStringValue(ContactVenueWebsite);

        writer.WritePropertyName("contactVenuePhoneNumber");
        writer.WriteStringValue(ContactVenuePhoneNumber);

        if (!string.IsNullOrEmpty(LogoText))
        {
            writer.WritePropertyName("logoText");
            writer.WriteStringValue(LogoText);
        }

        if (AssociatedStoreIdentifiers.Count > 0)
        {
            writer.WritePropertyName("associatedStoreIdentifiers");
            writer.WriteStartArray();

            foreach (var storeIdentifier in AssociatedStoreIdentifiers)
            {
                writer.WriteNumberValue(storeIdentifier);
            }

            writer.WriteEndArray();
        }

        if (!string.IsNullOrEmpty(AppLaunchURL))
        {
            writer.WritePropertyName("appLaunchURL");
            writer.WriteStringValue(AppLaunchURL);
        }
    }

    private void WriteUserInfo(Utf8JsonWriter writer)
    {
        if (UserInfo != null)
        {
            writer.WritePropertyName("userInfo");
            string json = JsonSerializer.Serialize(UserInfo);
            writer.WriteRawValue(json);
        }
    }

    private void WriteAppearanceKeys(Utf8JsonWriter writer)
    {
        if (!string.IsNullOrEmpty(ForegroundColor))
        {
            writer.WritePropertyName("foregroundColor");
            writer.WriteStringValue(ConvertColor(ForegroundColor));
        }

        if (!string.IsNullOrEmpty(BackgroundColor))
        {
            writer.WritePropertyName("backgroundColor");
            writer.WriteStringValue(ConvertColor(BackgroundColor));
        }

        if (!string.IsNullOrEmpty(LabelColor))
        {
            writer.WritePropertyName("labelColor");
            writer.WriteStringValue(ConvertColor(LabelColor));
        }

        if (SuppressStripShine.HasValue)
        {
            writer.WritePropertyName("suppressStripShine");
            writer.WriteBooleanValue(SuppressStripShine.Value);
        }

        if (!string.IsNullOrEmpty(GroupingIdentifier))
        {
            writer.WritePropertyName("groupingIdentifier");
            writer.WriteStringValue(GroupingIdentifier);
        }
    }

    private void WriteExpirationKeys(Utf8JsonWriter writer)
    {
        if (ExpirationDate.HasValue)
        {
            writer.WritePropertyName("expirationDate");
            writer.WriteStringValue(ExpirationDate.Value.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }

        if (Voided.HasValue)
        {
            writer.WritePropertyName("voided");
            writer.WriteBooleanValue(Voided.Value);
        }
    }

    private void WritePreferredStyleSchemes(Utf8JsonWriter writer)
    {
        if (PreferredStyleSchemes.Count > 0)
        {
            writer.WritePropertyName("preferredStyleSchemes");
            writer.WriteStartArray();

            foreach (var scheme in PreferredStyleSchemes)
            {
                var key = scheme.ToString();
                writer.WriteStringValue(char.ToLowerInvariant(key[0]) + key.Substring(1));               
            }

            writer.WriteEndArray();
        }
    }

    private void OpenStyleSpecificKey(Utf8JsonWriter writer)
    {
        string key = Style.ToString();

        writer.WritePropertyName(char.ToLowerInvariant(key[0]) + key.Substring(1));
        writer.WriteStartObject();
    }

    private static void CloseStyleSpecificKey(Utf8JsonWriter writer)
    {
        writer.WriteEndObject();
    }

    private static void WriteSection(Utf8JsonWriter writer, string sectionName, List<Field> fields)
    {
        writer.WritePropertyName(sectionName);
        writer.WriteStartArray();

        foreach (var field in fields)
        {
            field.Write(writer);
        }

        writer.WriteEndArray();
    }

    private void WriteNfcKeys(Utf8JsonWriter writer)
    {
        if (!string.IsNullOrEmpty(Nfc.Message))
        {
            writer.WritePropertyName("nfc");
            writer.WriteStartObject();
            writer.WritePropertyName("message");
            writer.WriteStringValue(Nfc.Message);

            if (!string.IsNullOrEmpty(Nfc.EncryptionPublicKey))
            {
                writer.WritePropertyName("encryptionPublicKey");
                writer.WriteStringValue(Nfc.EncryptionPublicKey);
            }

            writer.WriteEndObject();
        }
    }

    private static string ConvertColor(string color)
    {
        if (!string.IsNullOrEmpty(color) && color.Substring(0, 1) == "#")
        {
            int r, g, b;

            if (color.Length == 3)
            {
                r = int.Parse(color.Substring(1, 1), NumberStyles.HexNumber);
                g = int.Parse(color.Substring(2, 1), NumberStyles.HexNumber);
                b = int.Parse(color.Substring(3, 1), NumberStyles.HexNumber);
            }
            else if (color.Length >= 6)
            {
                r = int.Parse(color.Substring(1, 2), NumberStyles.HexNumber);
                g = int.Parse(color.Substring(3, 2), NumberStyles.HexNumber);
                b = int.Parse(color.Substring(5, 2), NumberStyles.HexNumber);
            }
            else
            {
                throw new ArgumentException("use #rgb or #rrggbb for color values", color);
            }

            return $"rgb({r},{g},{b})";
        }
        else
        {
            return color;
        }
    }

    #endregion
}
