namespace GameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CsvHelper;
    using CsvHelper.Configuration;

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
                    Description =
                        @"Bezleśne zbiorowisko roślinności w zimnym klimacie strefy arktycznej i subarktycznej. Latem temperatura nie przekracza 15 °C, trwa dzień polarny."
                },
                new CardInfo
                {
                    Title = "Chrobotek reniferowy",
                    CardType = CardType.Plant,
                    ZoneSetName = ZoneName.Tundra,
                    CardCount = 3,
                    ImageName = "Chrobotek reniferowy.jpg",
                    Description =
                        @"Gatunek grzybów należący do rodziny chrobotkowatych <i>(Cladoniaceae)</i>. Ze względu na współżycie z glonami zaliczany jest do porostów."
                },

                new CardInfo
                {
                    Title = "Tajga",
                    CardType = CardType.Land,
                    ZoneSetName = ZoneName.Tajga,
                    CardCount = 6,
                    ImageName = "Tajga.jpg",
                    Description =
                        @"Tajga, borealne lasy iglaste – lasy iglaste, które występują w północnej części Azji i Ameryki Północnej, w obrębie klimatu umiarkowanego chłodnego na półkuli północnej. Tajgę w większości porastają lasy iglaste oraz, w niewielkim stopniu, lasy liściaste."
                },
                new CardInfo
                {
                    Title = "Renifer tundrowy",
                    CardType = CardType.Animal,
                    ZoneSetName = ZoneName.Tajga,
                    CardCount = 3,
                    ImageName = "ReniferTundrowy.jpg",
                    Description =
                        @"Renifer tundrowy, ren, karibu (Rangifer tarandus) – gatunek ssaka z rodziny jeleniowatych <i>(Cervidae)</i>, zamieszkujący arktyczną tundrę i lasotundrę w Eurazji i Ameryce Północnej."
                },


                new CardInfo
                {
                    Title = "Klimat śródziemnomorski",
                    CardType = CardType.Land,
                    ZoneSetName = ZoneName.Środziemnomorski,
                    CardCount = 6,
                    ImageName = "Środziemnomorski.jpg",
                    Description =
                        @"Rodzaj klimatu podzwrotnikowego (subtropikalnego), występujący nad Morzem Śródziemnym, a także w Kalifornii, RPA, na południu Australii oraz u wybrzeży Chile w Ameryce Południowej. Jego charakterystyczną cechą są ciepłe i suche lata powyżej 20 °C oraz łagodne zimy powyżej 0 °C."
                },
            };
            cardInfo = LoadCardsDefinitions().ToArray();

            var cards = Helpers.BuildCardSet(cardInfo).ToArray();
            var renderer = new CardsFileRenderer(parameters);

            using (var output = new StreamWriter(@".\Content\output.html"))
            {
                renderer.RenderFile(cards, output);

                output.Flush();
                output.Close();
            }

            var cardStats = new CardStatistics(cardInfo, cards);
            var instructioNRenderer = new InstructionRenderer();
            using (var output = new StreamWriter(@".\Content\instruction_output.html"))
            {
                instructioNRenderer.PrintInstruction(cardStats, output);

                output.Flush();
                output.Close();
            }

        }

        // Strefa;Rodzaj;Nazwa;Obrazek;Opis;"Ilość"
        private static IEnumerable<CardInfo> LoadCardsDefinitions()
        {
            var config = new Configuration
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                Quote = '"'
            };

            using (var reader = new StreamReader(@"Data\Cards.csv"))
            {
                using (CsvReader csvReader = new CsvReader(reader, config))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        yield return new CardInfo
                        {
                            ZoneSetName = (ZoneName) Enum.Parse(typeof(ZoneName), csvReader[@"Strefa"]),
                            CardType = CardTypeConvert(csvReader[@"Rodzaj"]),
                            Title = csvReader[@"Nazwa"],
                            ImageName = csvReader[@"Obrazek"],
                            Description = csvReader[@"Opis"],
                            CardCount = int.Parse(csvReader[@"Ilość"]),
                            IconLocation = int.Parse(DefaultString(csvReader[@"IconLocation"], "1"))
                        };
                    }

                }
            }
        }

        private static string DefaultString(string @base, string @default)
        {
            if (string.IsNullOrWhiteSpace(@base))
            {
                return @default;
            }

            return @base;
        }

        private static CardType CardTypeConvert(string s)
        {
            switch (s.ToUpper())
            {
                case "R": return CardType.Plant;
                case "Z": return CardType.Animal;
                case "S": return CardType.Land;
                default: throw new ArgumentOutOfRangeException(nameof(s));
            }
        }
    }
}
