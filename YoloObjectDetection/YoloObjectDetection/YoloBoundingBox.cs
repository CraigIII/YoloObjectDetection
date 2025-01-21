namespace ONNXObjectDetection
{
    // 描述偵測到的物件資訊的類別
    public class YoloBoundingBox
    {
        // 記錄偵測到的物件的位置的屬性
        public BoundingBox Dimensions { get; set; }

        // 記錄偵測到的物件的種類的屬性
        public string Label { get; set; }

         // 記錄偵測到的物件的信心指數的屬性
       public float Confidence { get; set; }

        //將偵測到的物件的位置建立成RectangleF型態, 做為標記用途
        public RectangleF Rect
        {
            get { return new RectangleF(Dimensions.X, Dimensions.Y, Dimensions.Width, Dimensions.Height); }
        }

        //記錄繪製矩形欲使用的色彩的屬性
        public Color BoxColor { get; set; }
    }
}