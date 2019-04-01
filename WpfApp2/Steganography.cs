using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace SteganographyApp
{
    public class Steganography
    {
        public Bitmap Image { get; private set; }

        public Steganography(Image image)
        {
            this.Image = new Bitmap(image);
        }
        public Steganography(Bitmap image)
        {
            this.Image = image;
        }

        public void Encrypt(string message)
        {
            if (message.Length >= (Image.Width * Image.Height))
            {
                throw new Exception("The image size is too small to support the " +
                    "message." + "\n" + "Please choose a bigger image.");
            }

            int i = 0;
            char[] flag = GenerateMessageLengthFlag(message).ToCharArray();
            char[] binaryMessage = StringToBinary(message).ToCharArray();
            char[] messageToWrite = null;

            for (int y = 0; y < Image.Height; y++)
            {
                for (int x = 0; x < Image.Width; x++)
                {
                    if (x < 4 && y == 0)
                        messageToWrite = flag;
                    else if (x == 4 && y == 0)
                    {
                        messageToWrite = binaryMessage;
                        i = 0;
                    }
                    else if (i >= binaryMessage.Length)
                        break;

                    Color pixel = Image.GetPixel(x, y);
                    StringBuilder[] argbBinaryValues = ArgbValuesToBinary(pixel);
                    int[] finalArgbValues = ReplaceBinaryValues(argbBinaryValues, messageToWrite, i);
                    i += 8;

                    Image.SetPixel(x, y, Color.FromArgb
                        (
                            finalArgbValues[0],
                            finalArgbValues[1],
                            finalArgbValues[2],
                            finalArgbValues[3]
                        ));
                }
                if (i >= binaryMessage.Length)
                    break;
            }
        }

        public string Decrypt()
        {
            string messageFlag = string.Empty;
            string finalMessage = string.Empty;
            int messageLength = GetMessageLength();

            int x = 4;
            int y = 0;
            while (messageLength > 0)
            {
                if (x == Image.Width)
                {
                    x = 0;
                    y++;
                }
                if (y >= Image.Height)
                    break;
                messageFlag = string.Empty;
                var pixel = Image.GetPixel(x, y);
                messageFlag += ConvertPixelToBinaryString(pixel);
                finalMessage += (char)Convert.ToInt32(messageFlag, 2);
                messageLength--;
                x++;
            }

            return finalMessage;
        }

        private string StringToBinary(string message)
        {
            StringBuilder ret = new StringBuilder();
            foreach (char c in message.ToCharArray())
            {
                ret.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            return ret.ToString();
        }

        private string GenerateMessageLengthFlag(string message)
        {
            int len = message.Length;
            string binaryString = Convert.ToString(len, 2).PadLeft(32, '0');

            return binaryString;
        }

        private StringBuilder[] ArgbValuesToBinary(Color pixel)
        {
            StringBuilder[] argbArray = new StringBuilder[4]
            {
                new StringBuilder(Convert.ToString(pixel.A, 2).PadLeft(8, '0')),
                new StringBuilder(Convert.ToString(pixel.R, 2).PadLeft(8, '0')),
                new StringBuilder(Convert.ToString(pixel.G, 2).PadLeft(8, '0')),
                new StringBuilder(Convert.ToString(pixel.B, 2).PadLeft(8, '0'))
            };

            return argbArray;
        }

        private int[] ReplaceBinaryValues(StringBuilder[] argbBinaryValues, char[] messageToWrite, int index)
        {
            int[] argbValues = new int[4];

            for (int j = 0; j < 4; j++)
            {
                argbBinaryValues[j].Remove(6, 2);
                argbBinaryValues[j].Append(messageToWrite, index, 2);
                index += 2;
            }

            for (int j = 0; j < 4; j++)
            {
                argbValues[j] = Convert.ToInt32(argbBinaryValues[j].ToString(), 2);
            }
            return argbValues;
        }

        private int GetMessageLength()
        {
            string messageLength = string.Empty;

            Color[] lengthPixels = new Color[4]
            {
                Image.GetPixel(0,0),
                Image.GetPixel(1,0),
                Image.GetPixel(2,0),
                Image.GetPixel(3,0)
            };

            foreach(var pixel in lengthPixels)
            {
                messageLength += ConvertPixelToBinaryString(pixel);
            }

            return Convert.ToInt32(messageLength, 2);
        }

        private string ConvertPixelToBinaryString(Color pixel)
        {
            string message = string.Empty;
            message += Convert.ToString(pixel.A, 2).PadLeft(8, '0').Substring(6);
            message += Convert.ToString(pixel.R, 2).PadLeft(8, '0').Substring(6);
            message += Convert.ToString(pixel.G, 2).PadLeft(8, '0').Substring(6);
            message += Convert.ToString(pixel.B, 2).PadLeft(8, '0').Substring(6);

            return message;
        }
    }

}
