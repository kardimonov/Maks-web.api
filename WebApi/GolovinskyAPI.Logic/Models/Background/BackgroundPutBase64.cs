﻿namespace GolovinskyAPI.Logic.Models.Background
{
    public class BackgroundPutBase64 : BackgroundBase
    {
        public string Image { get; set; }

        public char Orientation { get; set; }

        public char Place { get; set; }
    }
}
