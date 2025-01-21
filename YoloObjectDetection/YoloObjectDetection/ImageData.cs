using Microsoft.ML.Data;

namespace ONNXObjectDetection
{
    //描述欲偵測的圖片的資訊的類別
    public class ImageData
    {
        [LoadColumn(0)]
        public string ImagePath;        //欲偵測的圖片的路徑

        [LoadColumn(1)]
        public string Label;                 //欲偵測的圖片的標籤

        //支援讀取存放欲辨識圖片的資料夾中所有的圖片的函式
        public static IEnumerable<ImageData> ReadFromFile(string imageFolder)
        {
            return Directory
                .EnumerateFiles(imageFolder)
                .Where(filePath => Path.GetExtension(filePath) != ".md")
                .Select(filePath => new ImageData { ImagePath = filePath, Label = Path.GetFileName(filePath) });
        }
    }
}