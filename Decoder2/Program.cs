using System;
using System.IO;
using System.Text;

namespace shitDecoder2
{
    class Program
    {
        public static int SwapEndianness(int value)
{
    var b1 = (value >> 0) & 0xff;
    var b2 = (value >> 8) & 0xff;
    var b3 = (value >> 16) & 0xff;
    var b4 = (value >> 24) & 0xff;

    return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
} 
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StreamWriter sw = new StreamWriter("decrypt.txt");
           
           

            using (BinaryReader b = new BinaryReader(File.Open("Font_default.g1n", FileMode.Open)))
            {



                // Seek to our required position.
                b.BaseStream.Seek(0x20064, SeekOrigin.Begin);

                for (int x = 1; x <= 23124; x++)
                {
                    Console.WriteLine(Convert.ToChar(x + 31));

                    sw.WriteLine("Character: "+ Convert.ToChar(x + 31));
                    sw.WriteLine("Code: " + (x + 31));
                    // Read the next 2000 bytes.

                    byte[] unknown1 = b.ReadBytes(3);

                    //textBox1.Text = textBox1.Text + Environment.NewLine + "Unknown1: " + BitConverter.ToString(unknown1);
                    byte[] sizeFont = b.ReadBytes(2);
                    String temporalfont = BitConverter.ToString(sizeFont);
                    string[] spliterfont = temporalfont.Split("-");
                    int part1 = int.Parse(spliterfont[0], System.Globalization.NumberStyles.HexNumber);
                    int part2 = int.Parse(spliterfont[1], System.Globalization.NumberStyles.HexNumber);
                    if (part2 % 2 == 0)
                    {
                      sw.WriteLine("Heigth: " + part2 );
                    }
                    else
                    { sw.WriteLine("Heigth: " + (part2 + 1) ); }

                    if ( part1 % 2 == 0) {
                        sw.WriteLine("Width: " + part1 );
                    }
                    else
                    { sw.WriteLine("Width: " + (part1 + 1) ); }

                   

                    byte[] unknown2 = b.ReadBytes(3);
                   
                   

                    //textBox1.Text = textBox1.Text + Environment.NewLine + "unknown2: " + BitConverter.ToString(unknown2);
                    byte[] positionFont = b.ReadBytes(4);

                    Console.WriteLine("positionFont: " + BitConverter.ToString(positionFont) + "\n");
                    String test = BitConverter.ToString(positionFont);
                    test = test.Replace("-", "");
                    int intValue = int.Parse(test, System.Globalization.NumberStyles.HexNumber);
                    int result = SwapEndianness(intValue);

                    result = result + 0x25AC4;


                    sw.WriteLine("positionFont: " + result.ToString("X") + "\n");
                    sw.WriteLine("-----------------------------------------Decrypt by Xex-----------------------");
                }
                sw.Close();

            }
        }
    }
}
