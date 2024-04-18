using System.Drawing;
using System.Drawing.Imaging;

namespace MyBlogApi.Store
{
    public class ImageStore
    {
        public Bitmap Base64StringToImage(string base64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(arr);
                return new Bitmap(ms);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string SaveImageFiles(Bitmap bitmap)
        {
            string imageName= Guid.NewGuid().ToString();
            string imagePath= Environment.CurrentDirectory+ $"/Img/{imageName}.png";

            if (bitmap != null)
            {
                bitmap.Save(imagePath,ImageFormat.Jpeg);
            }
            return imageName;
        }
    }
}
