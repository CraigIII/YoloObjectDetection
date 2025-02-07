﻿using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoloObjectDetection;

namespace ONNXObjectDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //選取欲辨識圖片的函式
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picOriginal.ImageLocation = openFileDialog1.FileName;
            }
        }

        //執行物件偵測
        private void btnDetect_Click(object sender, EventArgs e)
        {
            var modelFilePath = "Model/TinyYolo2_model.onnx";       //YOLO模型的位置
            MLContext mlContext = new MLContext();                            //建立MLContext類別的物件

            try
            {
                // 載入指定資料夾中的所有圖片進行物件偵測
                IEnumerable<ImageData> images = ImageData.ReadFromFile("images");
                IDataView imageDataView = mlContext.Data.LoadFromEnumerable(images);

                // 建立類別的物件
                var modelScorer = new OnnxModel("images", modelFilePath, mlContext);

                // 物件資料中的圖片並取得偵測結果
                IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

                // 解析偵測結果
                YoloOutputParser parser = new YoloOutputParser();

                //取得偵測到的物件的座標位置和大小
                var boundingBoxes =
                    probabilities
                    .Select(probability => parser.ParseOutputs(probability))
                    .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

                // 繪製被偵測的圖片中辨識成功的物件
                for (var i = 0; i < images.Count(); i++)
                {
                    string imageFileName = images.ElementAt(i).Label;
                    IList<YoloBoundingBox> detectedObjects = boundingBoxes.ElementAt(i);

                    DrawBoundingBox("images", imageFileName, detectedObjects);
                    LogDetectedObjects(imageFileName, detectedObjects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //繪製物件的位置矩形
        void DrawBoundingBox(string inputImageLocation, string imageName, IList<YoloBoundingBox> filteredBoundingBoxes)
        {
            Image image = Image.FromFile(Path.Combine(inputImageLocation, imageName));

            var originalImageHeight = image.Height;
            var originalImageWidth = image.Width;

            foreach (var box in filteredBoundingBoxes)
            {
                // Get Bounding Box Dimensions
                var x = (uint)Math.Max(box.Dimensions.X, 0);
                var y = (uint)Math.Max(box.Dimensions.Y, 0);
                var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                // Resize To Image
                x = (uint)originalImageWidth * x / ImageSettings.imageWidth;
                y = (uint)originalImageHeight * y / ImageSettings.imageHeight;
                width = (uint)originalImageWidth * width / ImageSettings.imageWidth;
                height = (uint)originalImageHeight * height / ImageSettings.imageHeight;

                // Bounding Box Text
                string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Define Text Options
                    Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                    // Define BoundingBox options
                    Pen pen = new Pen(box.BoxColor, 3.2f);
                    SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                    // Draw text on image 
                    thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

                    // Draw bounding box on image
                    thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                }
            }
            image.Save(imageName);
            picDetected.ImageLocation=imageName ;
        }

        //顯示辨識結果的函式
        void LogDetectedObjects(string imageName, IList<YoloBoundingBox> boundingBoxes)
        {
            Trace.WriteLine($".....The objects in the image {imageName} are detected as below....");

            foreach (var box in boundingBoxes)
            {
                Trace.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
            }

            Trace.WriteLine("");
        }
    }
}
