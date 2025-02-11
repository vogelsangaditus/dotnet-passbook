﻿using System;

namespace Passbook.Generator
{
    public static class PassbookImageExtensions
    {
        public static string ToFilename(this PassbookImage passbookImage)
        {
            switch (passbookImage)
            {
                case PassbookImage.Icon:
                    return "icon.png";
                case PassbookImage.Icon2X:
                    return "icon@2x.png";
                case PassbookImage.Icon3X:
                    return "icon@3x.png";
                case PassbookImage.Logo:
                    return "logo.png";
                case PassbookImage.Logo2X:
                    return "logo@2x.png";
                case PassbookImage.Logo3X:
                    return "logo@3x.png";
                case PassbookImage.Background:
                    return "background.png";
                case PassbookImage.Background2X:
                    return "background@2x.png";
                case PassbookImage.Background3X:
                    return "background@3x.png";
                case PassbookImage.Strip:
                    return "strip.png";
                case PassbookImage.Strip2X:
                    return "strip@2x.png";
                case PassbookImage.Strip3X:
                    return "strip@3x.png";
                case PassbookImage.Thumbnail:
                    return "thumbnail.png";
                case PassbookImage.Thumbnail2X:
                    return "thumbnail@2x.png";
                case PassbookImage.Thumbnail3X:
                    return "thumbnail@3x.png";
                case PassbookImage.Footer:
                    return "footer.png";
                case PassbookImage.Footer2X:
                    return "footer@2x.png";
                case PassbookImage.Footer3X:
                    return "footer@3x.png";
                case PassbookImage.Artwork:
                    return "artwork.png";
                case PassbookImage.Artwork2X:
                    return "artwork@2x.png";
                case PassbookImage.Artwork3X:
                    return "artwork@3x.png";
                case PassbookImage.VenueMap:
                    return "venueMap.png";
                default:
                    throw new NotImplementedException("Unknown PassbookImage type.");
            }
        }
    }

    public enum PassbookImage
    {
        /// <summary>
        /// Background image, 180x220 points
        /// </summary>
        Background,
        /// <summary>
        /// @2x Retina background image, 180x220 points
        /// </summary>
        Background2X,
        /// <summary>
        /// @3x Retina background image, 180x220 points
        /// </summary>
        Background3X,
        /// <summary>
        /// Icon as per https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/Alerts.html#//apple_ref/doc/uid/TP40006556-CH14
        /// </summary>
        Icon,
        /// <summary>
        /// @2x Retina icon as per https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/Alerts.html#//apple_ref/doc/uid/TP40006556-CH14
        /// </summary>
        Icon2X,
        /// <summary>
        /// Retina icon as per https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/Alerts.html#//apple_ref/doc/uid/TP40006556-CH14
        /// </summary>
        Icon3X,
        /// <summary>
        /// Logo, 160x50 points
        /// </summary>
        Logo,
        /// <summary>
        /// @2x Retina logo, 160x50 points
        /// </summary>
        Logo2X,
        /// <summary>
        /// @3x Retina logo, 160x50 points
        /// </summary>
        Logo3X,
        /// <summary>
        /// Strip
        /// <list type="bullet">
        ///		<item>
        ///			<description>On iPhone 6 and 6 Plus The allotted space is 375x98 points for event tickets, 375x144 points for gift cards and coupons, and 375x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On prior hardware The allotted space is 320x84 points for event tickets, 320x110 points for other pass styles with a square barcode on devices with 3.5 inch screens, and 320x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On iOS 6 The strip image is only 312 points wide; the height varies as described above. A shine effect is automatically applied to the strip image; to suppress it use the suppressStripShine key.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Strip,
        /// <summary>
        /// @2x Retina strip
        /// <list type="bullet">
        ///		<item>
        ///			<description>On iPhone 6 and 6 Plus The allotted space is 375x98 points for event tickets, 375x144 points for gift cards and coupons, and 375x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On prior hardware The allotted space is 320x84 points for event tickets, 320x110 points for other pass styles with a square barcode on devices with 3.5 inch screens, and 320x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On iOS 6 The strip image is only 312 points wide; the height varies as described above. A shine effect is automatically applied to the strip image; to suppress it use the suppressStripShine key.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Strip2X,
        /// <summary>
        /// @3x Retina strip
        /// <list type="bullet">
        ///     <item>
        ///         <description>On iPhone 6 and 6 Plus The allotted space is 375x98 points for event tickets, 375x144 points for gift cards and coupons, and 375x123 in all other cases.</description>
        ///     </item>
        ///     <item>
        ///         <description>On prior hardware The allotted space is 320x84 points for event tickets, 320x110 points for other pass styles with a square barcode on devices with 3.5 inch screens, and 320x123 in all other cases.</description>
        ///     </item>
        ///     <item>
        ///         <description>On iOS 6 The strip image is only 312 points wide; the height varies as described above. A shine effect is automatically applied to the strip image; to suppress it use the suppressStripShine key.</description>
        ///     </item>
        /// </list>
        /// </summary>
        Strip3X,
        /// <summary>
        /// Thumbnail, 90x90 points
        /// </summary>
        Thumbnail,
        /// <summary>
        /// @2x Retina thumbnail, 90x90 points
        /// </summary>
        Thumbnail2X,
        /// <summary>
        /// @3x Retina thumbnail, 90x90 points
        /// </summary>
        Thumbnail3X,
        /// <summary>
        /// Footer, 286x15 points
        /// </summary>
        Footer,
        /// <summary>
        /// @2x Retina footer, 286x15 points
        /// </summary>
        Footer2X,
        /// <summary>
        /// @3x Retina footer, 286x15 points
        /// </summary>
        Footer3X,

        /// <summary>
        /// Artwork, 358x448 points (alternative to 'background.png')
        /// </summary>
        Artwork,

        /// <summary>
        /// @2x Retina artwork, 358x448 points (alternative to 'background.png')
        /// </summary>
        Artwork2X,

        /// <summary>
        /// @3x Retina artwork, 358x448 points (alternative to 'background.png')
        /// </summary>
        Artwork3X,

        /// <summary>
        /// Issuer-provided static map of the venue, which will be shown in the event guide.
        /// </summary>
        VenueMap,
    }
}
