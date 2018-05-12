namespace GameGenerator
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new LayoutParameters();
            var layout = new Layout(parameters);
            layout.Calculations();

            var cardInfo = new CardInfo[]
            {
                new CardInfo
                {
                    Title = "Tundra",
                    CardType = CardType.Land,
                    ZoneSetName = ZoneName.Tundra,
                    CardCount = 3,
                    ImageName = "Tundra.jpg",
                    Description = @"Bezleśne zbiorowisko roślinności w zimnym klimacie strefy arktycznej i subarktycznej. Latem temperatura nie przekracza 15 °C, trwa dzień polarny."
                },
                new CardInfo
                {
                    Title = "Chrobotek reniferowy",
                    CardType = CardType.Plant,
                    ZoneSetName = ZoneName.Tundra,
                    CardCount = 2,
                    ImageName = "Chrobotek reniferowy.jpg",
                    Description = @"Gatunek grzybów należący do rodziny chrobotkowatych (Cladoniaceae). Ze względu na współżycie z glonami zaliczany jest do porostów."
                },
            };

            var cards = Helpers.BuildCardSet(cardInfo);
            var renderer = new CardsFileRenderer(parameters);

            using (var output = new StreamWriter(@".\Content\output.html"))
            {
                renderer.RenderFile(cards, output);

                output.Flush();
                output.Close();
            }
                
        }
    }
}
