using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloObjectDetection
{
    //描述YOLO模型輸入和輸出欄位名稱的結構
    public struct ModelSettings
    {       
        public const string ModelInput = "image";       // 輸入欄位名稱
        public const string ModelOutput = "grid";       // 輸出欄位名稱
    }
}
