namespace GameGenerator
{
    public class LayoutParameters
    {
        // TODO: read from config

        public int PageHeight { get; set; } = 297;
        public int PageWidth { get; set; } = 210;
        public int PageMarginTop { get; set; } = 10;
        public int PageMarginBottom { get; set; } = 10;
        public int PageMarginLeft { get; set; } = 10;
        public int PageMarginRight { get; set; } = 10;
        public int CardWidth { get; set; } = 65;
        public int CardHeight { get; set; } = 92;

        public int CardMaxTextWidth { get; set; } = 56;
        public int CardMaxTextHeight { get; set; } = 31;


        public bool?  IsPortraitLayout { get; set; }
        public LayoutRecord Layout { get; set; }
    }
}