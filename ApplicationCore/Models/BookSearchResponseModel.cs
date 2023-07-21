using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class BookSearchResponseModel
    {
        //Model of the possible responses from the Google Books API
        public string? Kind { get; set; }
        public int TotalItems { get; set; }
        public List<Item>? Items { get; set; }
    }

    public class Item
    {
        public string? Kind { get; set; }
        public string? Id { get; set; }
        public string? Etag { get; set; }
        public string? SelfLink { get; set; }
        public VolumeInfo? VolumeInfo { get; set; }
        public SaleInfo? SaleInfo { get; set; }
        public AccessInfo? AccessInfo { get; set; }
        public SearchInfo? SearchInfo { get; set; }
    }

    public class VolumeInfo
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public List<string>? Authors { get; set; }
        public string? PublishedDate { get; set; }
        public string? Description { get; set; }
        public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
        public ReadingModes? ReadingModes { get; set; }
        public int? PageCount { get; set; }
        public string? PrintType { get; set; }
        public List<string>? Categories { get; set; }
        public double? AverageRating { get; set; }
        public int? RatingsCount { get; set; }
        public string? MaturityRating { get; set; }
        public bool? AllowAnonLogging { get; set; }
        public string? ContentVersion { get; set; }
        public PanelizationSummary? PanelizationSummary { get; set; }
        public ImageLinks? ImageLinks { get; set; }
        public string? Language { get; set; }
        public string? PreviewLink { get; set; }
        public string? InfoLink { get; set; }
        public string? CanonicalVolumeLink { get; set; }
    }

    public class IndustryIdentifier
    {
        public string? Type { get; set; }
        public string? Identifier { get; set; }
    }

    public class ReadingModes
    {
        public bool? Text { get; set; }
        public bool? Image { get; set; }
    }

    public class PanelizationSummary
    {
        public bool? ContainsEpubBubbles { get; set; }
        public bool? ContainsImageBubbles { get; set; }
    }

    public class ImageLinks
    {
        public string? SmallThumbnail { get; set; }
        public string? Thumbnail { get; set; }
    }

    public class SaleInfo
    {
        public string? Country { get; set; }
        public string? Saleability { get; set; }
        public bool? IsEbook { get; set; }
    }

    public class AccessInfo
    {
        public string? Country { get; set; }
        public string? Viewability { get; set; }
        public bool? Embeddable { get; set; }
        public bool? PublicDomain { get; set; }
        public string? TextToSpeechPermission { get; set; }
        public Epub? Epub { get; set; }
        public Pdf? Pdf { get; set; }
        public string? WebReaderLink { get; set; }
        public string? AccessViewStatus { get; set; }
        public bool? QuoteSharingAllowed { get; set; }
    }

    public class Epub
    {
        public bool? IsAvailable { get; set; }
    }

    public class Pdf
    {
        public bool? IsAvailable { get; set; }
    }

    public class SearchInfo
    {
        public string? TextSnippet { get; set; }
    }
}
