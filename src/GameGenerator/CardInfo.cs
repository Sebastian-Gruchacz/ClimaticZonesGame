namespace GameGenerator
{
    public class CardInfo
    {
        public int CardCount { get; set; }

        public CardType CardType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public ZoneName ZoneSetName { get; set; }

        public int IconLocation { get; set; }

        public Card ToCard()
        {
            return new Card
            {
                CardType = this.CardType,
                Title = this.Title,
                Description = this.Description,
                ImageName = this.ImageName,
                ZoneSetName = this.ZoneSetName,
                IconLocation = this.IconLocation
            };
        }
    }
}