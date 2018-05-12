namespace GameGenerator
{
    using System;

    public class CardRenderer
    {
        private readonly LayoutParameters _parameters;

        private string _landLayout;
        private string _animalLayout;
        private string _plantLayout;


        public CardRenderer(LayoutParameters parameters)
        {
            this._parameters = parameters;

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

            // TODO: use dictionary replacer
            return template
                .Replace("{#TITLE#}", card.Title)
                .Replace("{#DESCRIPTION#}", card.Description)
                .Replace("{#IMAGE_NAME#}", card.ImageName)
                .Replace("{#ZONE_IMAGE_NAME#}", this.GetZoneImageName(card))
                .Replace("{#CARD_TYPE_IMAGE_NAME#}", this.GetCardTypeImageName(card));
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
