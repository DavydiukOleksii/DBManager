using System.IO;

namespace DBManager.Infrastructure
{
    public static class ImageConverter
    {
        //method for convert images to base64
        public static byte[] GetImage(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
    }
}
