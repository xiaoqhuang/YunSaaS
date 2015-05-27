using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Helper
{
    public static class ValidateCodeHelper
    {
        public static byte[] GenerateValidateGraphic(out string randomCode)
        {
            randomCode = Guid.NewGuid().ToString().ToUpper().Replace("O", "").Replace("I", "").Substring(0, 5);
            using (Bitmap bitmap = new Bitmap(randomCode.Length*16, 28))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                    Font font = new Font("Arial", 14, FontStyle.Bold);
                    Brush brush = new SolidBrush(Color.Black);
                    int maxAngle = 20; //随机转动最大角度  
                    Random random = new Random();
                    //画图片的干扰线
                    for (int i = 0; i < 25; i++)
                    {
                        int x1 = random.Next(bitmap.Width);
                        int x2 = random.Next(bitmap.Width);
                        int y1 = random.Next(bitmap.Height);
                        int y2 = random.Next(bitmap.Height);
                        graphics.DrawLine(new Pen(Color.FromArgb(random.Next())), x1, y1, x2, y2);
                    }
                    //画图片的前景干扰点
                    for (int i = 0; i < 100; i++)
                    {
                        int x = random.Next(bitmap.Width);
                        int y = random.Next(bitmap.Height);
                        bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
                    }
                    for (int i = 0; i < randomCode.Length; i++)
                    {
                        int angle = random.Next(-maxAngle, maxAngle);
                        graphics.RotateTransform(angle); //旋转随机角度
                        graphics.DrawString(randomCode[i].ToString(), font, brush, 1f, 5f);
                        graphics.RotateTransform(-angle); //角度还原
                        //移动光标到指定位置
                        graphics.TranslateTransform(15f, 0);
                    }
                    MemoryStream stream = new MemoryStream();
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    return stream.ToArray();
                }
            }
        }
    }
}