class Program
{
    static void Main(string[] args)
    {
        static void download(string sourcePath, string destPath, char key)
        {
            using (var source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (var dest = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    int len = 2;
                    var bytes = new byte[len];
                    var fileSize = source.Length;

                    do
                    {
                        len = source.Read(bytes, 0, len);
                        for (int i = 0; i < len; i++)
                        {
                            // Hər bir byte XOR edilir
                            bytes[i] = (byte)(bytes[i] ^ key);
                        }
                        dest.Write(bytes, 0, len);

                        fileSize -= len;

                    } while (len > 0);
                }
            }
        }

        Console.WriteLine("Enter Source Path:");
        var sourcePath = Console.ReadLine();
        Console.WriteLine("Enter Destination Path:");
        var destPath = Console.ReadLine();
        Console.WriteLine("Enter Key:");
        var key = Convert.ToChar(Console.ReadLine());

        ThreadPool.QueueUserWorkItem(o =>
        {
            download(sourcePath, destPath, key);
            Console.WriteLine("END");
        });
        Console.ReadKey();
    }
}

