namespace ONNXObjectDetection
{
    //描述偵測到的物件位置的類別
    public class BoundingBox
    {
        public float X { get; set; }                //左上角點X座標
        public float Y { get; set; }                //左上角點Y座標
        public float Height { get; set; }      //高度
        public float Width { get; set; }       //寬度
    }
}