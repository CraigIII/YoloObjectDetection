using Microsoft.ML;
using Microsoft.ML.Data;
using System.Diagnostics;
using YoloObjectDetection;

namespace ONNXObjectDetection
{
    //包裝OnnxModel功能的類別
    public class OnnxModel
    {
        private readonly string imagesFolder;
        private readonly string modelLocation;
        private readonly MLContext mlContext;

        //建構函式
        public OnnxModel(string imagesFolder, string modelLocation, MLContext mlContext)
        {
            this.imagesFolder = imagesFolder;
            this.modelLocation = modelLocation;
            this.mlContext = mlContext;
        }
        
        //支援載入YOLO模型的函式
        private ITransformer LoadModel(string modelLocation)
        {
            Trace.WriteLine("Read model");
            Trace.WriteLine($"Model location: {modelLocation}");
            Trace.WriteLine($"Default parameters: image size=({ImageSettings.imageWidth},{ImageSettings.imageHeight})");

            // 取得輸入資料的相關資訊
            var data = mlContext.Data.LoadFromEnumerable(new List<ImageData>());

            // 定義物件偵測管線
            var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "image", imageFolder: "", inputColumnName: nameof(ImageData.ImagePath))
                            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "image", imageWidth: ImageSettings.imageWidth, imageHeight: ImageSettings.imageHeight, inputColumnName: "image"))
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "image"))
                            .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { ModelSettings.ModelOutput }, inputColumnNames: new[] { ModelSettings.ModelInput }));

            // 建立物件偵測模型
            var model = pipeline.Fit(data);
            // 傳回建立的物件偵測模型
            return model;
        }

        //應用程式可以呼叫Score函式, 並傳入欲偵測的圖片, 並取得偵測的結果
        public IEnumerable<float[]> Score(IDataView data)
        {
            //載入物件偵測模型
            var model = LoadModel(modelLocation);
            //叫用PredictDataUsingModel函式進行物偵測並傳回偵測的結果
            return PredictDataUsingModel(data, model);
        }

        // 支援對傳入的圖片進行物件的函式
        private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
        {
            Trace.WriteLine($"Images location: {imagesFolder}");
            Trace.WriteLine("");
            Trace.WriteLine("=====Identify the objects in the images=====");
            Trace.WriteLine("");
            //執行物件偵測並取得偵測結果
            IDataView scoredData = model.Transform(testData);
            //取得物件偵測結果的信心指數
            IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(ModelSettings.ModelOutput);
            //傳回取得的物件偵測結果的信心指數
            return probabilities;
        }
    }
}