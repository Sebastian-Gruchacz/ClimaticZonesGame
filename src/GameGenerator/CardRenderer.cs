namespace GameGenerator
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;

    public class CardRenderer
    {
        private const double INCH_MILLIMETERS = 25.4;
        private const float GRUCHA_CONST = 4; // have no time to figure calculations correctly, but this is just enough ;-)
        private readonly LayoutParameters _parameters;

        private string _landLayout;
        private string _animalLayout;
        private string _plantLayout;
        private readonly IntPtr _windowHandle;


        public CardRenderer(LayoutParameters parameters)
        {
            this._parameters = parameters;
            this._windowHandle = Process.GetCurrentProcess().MainWindowHandle;

            this.LoadLayouts();
        }

        private void LoadLayouts()
        {
            this._landLayout = Helpers.LoadFile(@"Data\Layouts\Land.html");
            this._animalLayout = Helpers.LoadFile(@"Data\Layouts\Animal.html");
            this._plantLayout = Helpers.LoadFile(@"Data\Layouts\Plant.html");
        }

        public string RenderCardHtmlContent(Card card)
        {
            string template = this.PickTemplate(card);
            decimal descFontSize = this.CalculateDescriptionFontSize(card.Description);

            // TODO: use dictionary replacer
            return template
                .Replace("{#TITLE#}", card.Title)
                .Replace("{#DESCRIPTION#}", card.Description)
                .Replace("{#IMAGE_NAME#}", card.ImageName)
                .Replace("{#ZONE_IMAGE_NAME#}", this.GetZoneImageName(card))
                .Replace("{#CARD_TYPE_IMAGE_NAME#}", this.GetCardTypeImageName(card))
                .Replace("{#DESC_FONT_SIZE#}", descFontSize.ToString("F1", CultureInfo.InvariantCulture) + "mm")
                .Replace("{#ICON_LOCATION#}", card.IconLocation.ToString("D"));
        }

        private decimal CalculateDescriptionFontSize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(text));
            }

            decimal minFontSize = 2m;
            decimal maxFontSize = 3.8m;
            decimal fontSizeStep = 0.2m;
            float maxHeight = 31;

            SizeF layoutSize = new SizeF(this._parameters.CardWidth - 4, 0); // 4 - padding both side
            Graphics g = Graphics.FromHwnd(this._windowHandle);
            var baseFont = new FontFamily("Verdana");

            for (decimal fontSize = maxFontSize; fontSize >= minFontSize; fontSize -= fontSizeStep)
            {
                Font font = new Font(baseFont, (float)fontSize, FontStyle.Regular, GraphicsUnit.Millimeter);

                var measured = g.MeasureString(text, font, layoutSize);
                var requiredHeightMilimeters = measured.Height / g.DpiY * INCH_MILLIMETERS  / GRUCHA_CONST;
                if (requiredHeightMilimeters > maxHeight)
                {
                    continue;
                }

                return fontSize;
            }

            return 0.0m; // invisible text, should alert!
        }

        private string PickTemplate(Card card)
        {
            switch (card.CardType)
            {
                case CardType.Land: return this._landLayout;
                case CardType.Animal: return this._animalLayout;
                case CardType.Plant: return this._plantLayout;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetCardTypeImageName(Card card)
        {
            return $"{card.CardType}.jpg";
        }

        private string GetZoneImageName(Card card)
        {
            return $"{card.ZoneSetName}.jpg";
        }
    }
}
