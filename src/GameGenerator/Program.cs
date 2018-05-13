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
                    CardCount = 6,
                    ImageName = "Tundra.jpg",
                    Description = @"Bezleśne zbiorowisko roślinności w zimnym klimacie strefy arktycznej i subarktycznej. Latem temperatura nie przekracza 15 °C, trwa dzień polarny."
                },
                new CardInfo
                {
                    Title = "Chrobotek reniferowy",
                    CardType = CardType.Plant,
                    ZoneSetName = ZoneName.Tundra,
                    CardCount = 3,
                    ImageName = "Chrobotek reniferowy.jpg",
                    Description = @"Gatunek grzybów należący do rodziny chrobotkowatych <i>(Cladoniaceae)</i>. Ze względu na współżycie z glonami zaliczany jest do porostów."
                },

                new CardInfo
                {
                    Title = "Tajga",
                    CardType = CardType.Land,
                    ZoneSetName = ZoneName.Tajga,
                    CardCount = 6,
                    ImageName = "Tajga.jpg",
                    Description = @"Tajga, borealne lasy iglaste – lasy iglaste, które występują w północnej części Azji i Ameryki Północnej, w obrębie klimatu umiarkowanego chłodnego na półkuli północnej. Tajgę w większości porastają lasy iglaste oraz, w niewielkim stopniu, lasy liściaste."
                },
                new CardInfo
                {
                    Title = "Renifer tundrowy",
                    CardType = CardType.Animal,
                    ZoneSetName = ZoneName.Tajga,
                    CardCount = 3,
                    ImageName = "ReniferTundrowy.jpg",
                    Description = @"Renifer tundrowy, ren, karibu (Rangifer tarandus) – gatunek ssaka z rodziny jeleniowatych <i>(Cervidae)</i>, zamieszkujący arktyczną tundrę i lasotundrę w Eurazji i Ameryce Północnej."
                },


                new CardInfo
                {
                    Title = "Klimat śródziemnomorski",
                    CardType = CardType.Land,
                    ZoneSetName = ZoneName.Środziemnomorski,
                    CardCount = 6,
                    ImageName = "Środziemnomorski.jpg",
                    Description = @"Rodzaj klimatu podzwrotnikowego (subtropikalnego), występujący nad Morzem Śródziemnym, a także w Kalifornii, RPA, na południu Australii oraz u wybrzeży Chile w Ameryce Południowej. Jego charakterystyczną cechą są ciepłe i suche lata powyżej 20 °C oraz łagodne zimy powyżej 0 °C."
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
