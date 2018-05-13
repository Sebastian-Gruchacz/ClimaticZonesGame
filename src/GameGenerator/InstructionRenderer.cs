namespace GameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class InstructionRenderer
    {
        private string _instructionTemplate;

        public InstructionRenderer()
        {
            this.LoadTemplate();
        }

        private void LoadTemplate()
        {
            this._instructionTemplate = Helpers.LoadFile(@"Data\Instruction.html");
        }

        public void PrintInstruction(CardStatistics stats, StreamWriter writer)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats));
            }
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            
            var replacements = this.BuildReplacementsDictionary(stats);

            Helpers.WriteFileFromTemplate(writer, this._instructionTemplate, replacements);
        }

        private IDictionary<string, string> BuildReplacementsDictionary(CardStatistics stats)
        {
            var r = new Dictionary<string, string>();

            r.Add("LAND_CARDS_COUNT", stats.LandCardsCount.ToString("D"));
            r.Add("ANIMAL_CARDS_COUNT", stats.AnimalCardsCount.ToString("D"));
            r.Add("PLANT_CARDS_COUNT", stats.PlantCardsCount.ToString("D"));
            r.Add("LAND_CARDS_MULTIPLIER", this.GetMinmax(stats.LandCardsMultiplierMin, stats.LandCardsMultiplierMax));
            r.Add("ANIMAL_CARDS_MULTIPLIER", this.GetMinmax(stats.AnimalCardsMultiplierMin, stats.AnimalCardsMultiplierMax));
            r.Add("PLANT_CARDS_MULTIPLIER", this.GetMinmax(stats.PlantCardsMultiplierMin, stats.PlantCardsMultiplierMax));
            r.Add("CARDS_TOTAL", stats.CardsTotal.ToString("D"));
            r.Add("PLAYERS_NUMBER", this.GetMinmax(2, 4)); // TODO: read from ini
            r.Add("LAND_CARDS_TYPES_COUNT", stats.LandDefinitionsCount.ToString("D"));
            r.Add("ANIMAL_CARDS_TYPES_COUNT", stats.AnimalDefinitionsCount.ToString("D"));
            r.Add("PLANT_CARDS_TYPES_COUNT", stats.PlantDefinitionsCount.ToString("D"));

            return r;
        }

        private string GetMinmax(int min, int max)
        {
            if (min == max)
            {
                return min.ToString();
            }
            else
            {
                return $"{min} - {max}";
            }
        }

    }
}
