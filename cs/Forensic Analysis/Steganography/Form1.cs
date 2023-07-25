using System.Drawing.Imaging;
using System.Numerics;
using System.Text;

namespace SAR0083_Steganography
{
    public partial class Form1 : Form
    {
        private string Title = "SAR0083 - Steganography";
        private string OriginalFileName = "";
        private ImageFile OriginalImageFile = null;
        private BitmapData OriginalBitmapData = null;
        private string EncodingFileName = "";
        private BinaryFile EncodingBinaryFile = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void HideUI()
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            richTextBox1.Enabled = false;
            button5.Enabled = false;
            label1.Enabled = false;
            button4.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void ShowUI()
        {
            button2.Enabled = true;

            if (this.OriginalFileName != "")
            {
                button3.Enabled = true;
                button1.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                
                if(radioButton1.Checked)
                {
                    richTextBox1.Enabled = true;
                    button5.Enabled = false;
                    label1.Enabled = false;
                    button4.Enabled = true;
                }
                else
                {
                    richTextBox1.Enabled = false;
                    button5.Enabled = true;
                    label1.Enabled = true;
                    
                    if(this.EncodingFileName != "")
                    {
                        button4.Enabled = true;
                    }
                }
            }

            progressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HideUI();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.bmp; *.gif; *.jpg; *.jpeg; *.png)|*.bmp; *.gif; *.jpg; *.jpeg; *.png";
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.OriginalFileName = ofd.FileName;
                this.OriginalImageFile = new ImageFile(this.OriginalFileName);
                this.OriginalBitmapData = new BitmapData(OriginalImageFile.Read());

                this.Text = this.Title + " (" + Path.GetFileName(this.OriginalFileName) + ")";
                using(Bitmap bitmap = new Bitmap(ofd.FileName))
                {
                    pictureBox1.Image = new Bitmap(bitmap);
                }
            }

            ShowUI();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HideUI();

            if (this.OriginalBitmapData.IsEncodingDetected())
            {
                MessageBox.Show(
                    "Encoding has been detected!\n\nFind out what text/file is inside the image by clicking [Decode image...] button.",
                    "Encoding detected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Encoding has not been detected!\n\nYou can add some text/file into the image by clicking [Encode] button.",
                    "Encoding not detected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            ShowUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideUI();

            if (this.OriginalBitmapData.Decode())
            {
                if(this.OriginalBitmapData.ContentFormat == "txt")
                {
                    MessageBox.Show(
                        this.OriginalBitmapData.TextContent,
                        "Decoded text",
                        MessageBoxButtons.OK
                    );
                }
                else{
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = this.OriginalBitmapData.ContentFormat.ToUpper() + " File(*." + this.OriginalBitmapData.ContentFormat.ToLower() + ")|*." + this.OriginalBitmapData.ContentFormat.ToLower();

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        BinaryFile binaryFile = new BinaryFile(sfd.FileName);
                        binaryFile.Write(this.OriginalBitmapData.Content);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Decoding could not be performed because encoding had not been detected!",
                    "Decoding error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            ShowUI();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HideUI();
            ShowUI();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HideUI();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files(*.)|";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.EncodingFileName = ofd.FileName;
                this.EncodingBinaryFile = new BinaryFile(this.EncodingFileName);

                label1.Text = Path.GetFileName(this.EncodingFileName);
            }

            ShowUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideUI();
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Path.GetExtension(this.OriginalFileName).Replace(".", "").ToUpper() + " File(*." + Path.GetExtension(this.OriginalFileName).Replace(".", "").ToLower() + ")|*." + Path.GetExtension(this.OriginalFileName).Replace(".", "").ToLower();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bool encodedSuccessfully = false;

                if (radioButton1.Checked)
                {
                    if(!this.OriginalBitmapData.EncodeText(richTextBox1.Text))
                    {
                        MessageBox.Show(
                            "Encoding could not be performed because the text is too long!",
                            "Encoding error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                    else
                    {
                        encodedSuccessfully = true;
                    }
                }
                else
                {
                    byte[] encodingContent = this.EncodingBinaryFile.Read();
                    string encodingContentFormat = Path.GetExtension(this.EncodingFileName).Replace(".", "").ToLower();
                    uint encodingContentLength = (uint)(encodingContent.Length * 8);

                    if(!this.OriginalBitmapData.Encode(encodingContentFormat, encodingContentLength, encodingContent))
                    {
                        MessageBox.Show(
                            "Encoding could not be performed because the file is too big!",
                            "Encoding error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                    else
                    {
                        encodedSuccessfully = true;
                    }
                }

                if(encodedSuccessfully)
                {
                    ImageFile imageFile = new ImageFile(sfd.FileName);
                    imageFile.Write(this.OriginalBitmapData);

                    using (Bitmap bitmap = new Bitmap(sfd.FileName))
                    {
                        pictureBox1.Image = new Bitmap(bitmap);
                    }
                }
            }

            ShowUI();
        }
    }

    public class RawBitmapData
    {
        protected uint width;
        protected uint height;
        protected byte[][][]? matrix;

        public uint Width { get { return this.width; } }
        public uint Height { get { return this.height; } }
        public uint MaxDataLength { get { return this.width * this.height * 3; } }
        public byte[][][]? Matrix { get { return this.matrix; } }

        public RawBitmapData(uint width, uint height, byte[][][]? matrix)
        {
            this.width = width;
            this.height = height;
            this.matrix = matrix;
        }

        public bool IsEncodingDetected()
        {
            // Width x Height x Color (byte = unsigned 8-bit integer)
            for (int x = 0; x < this.Width - 1; x++)
            {
                for (int y = 0; y < this.Height - 1; y++)
                {
                    byte colorsChanged = 0;
                    for (int z = 0; z < 3; z++)
                    {
                        byte tl = matrix[x][y][z];
                        byte tr = matrix[x + 1][y][z];
                        byte bl = matrix[x][y + 1][z];
                        byte br = matrix[x + 1][y + 1][z];
                        byte tlB = (byte)(tl / 2);
                        byte trB = (byte)(tr / 2);
                        byte blB = (byte)(bl / 2);
                        byte brB = (byte)(br / 2);
                        byte tlLSB = (byte)(tl % 2);
                        byte trLSB = (byte)(tr % 2);
                        byte blLSB = (byte)(bl % 2);
                        byte brLSB = (byte)(br % 2);

                        // All bits excluding LSB are equal
                        if (tlB == trB && trB == blB && blB == brB)
                        {
                            // Only 1 pixel has different LSB
                            if ((tlLSB + trLSB + blLSB + brLSB) % 2 == 1)
                            {
                                if (++colorsChanged == 3)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return false;
        }
    }

    public class BitmapData : RawBitmapData
    {
        // Max file extension length
        private const byte FILE_EXTENSION_MAX_LENGTH = 8;

        // Only 6 bits needed for 10 numerals & 26 char long lowercase ENG alphabet
        private const byte FILE_EXTENSION_CHAR_BIT_SIZE = 6;

        private const byte FILE_EXTENSION_MAX_LENGTH_BIT_SIZE = FILE_EXTENSION_MAX_LENGTH * FILE_EXTENSION_CHAR_BIT_SIZE;

        // Max content length in bits
        private uint MaxContentLengthBitSize
        {
            get
            {
                uint maxContentLengthBitSize = 1;
                uint maxContentLength = this.MaxDataLength - FILE_EXTENSION_MAX_LENGTH_BIT_SIZE - maxContentLengthBitSize;

                // Find out how many bits is needed for saving content length
                for (; ; maxContentLengthBitSize++, maxContentLength--)
                {
                    if (Math.Pow(2, maxContentLengthBitSize) > BitOperations.RoundUpToPowerOf2(maxContentLength))
                    {
                        break;
                    }
                }

                return maxContentLengthBitSize;
            }
        }

        private int MaxContentLength { get { return (int)(this.MaxDataLength - FILE_EXTENSION_MAX_LENGTH_BIT_SIZE - MaxContentLengthBitSize); } }

        private uint HeaderSize { get { return FILE_EXTENSION_MAX_LENGTH_BIT_SIZE + MaxContentLengthBitSize; } }

        private bool isEncoded = false;
        private string contentFormat = "";
        private uint contentLength = 0;
        private byte[]? content = null;

        public bool IsEncoded { get { return this.isEncoded; } }
        public string ContentFormat { get { return this.contentFormat; } }
        public uint ContentLength { get { return this.contentLength; } }
        public uint DataLength { get { return HeaderSize + this.contentLength; } }
        public byte[]? Content { get { return this.content; } }
        public string TextContent { get { return (this.content == null ? "" : Encoding.UTF8.GetString(this.content)); } }

        public BitmapData(uint width, uint height, byte[][][]? data, bool isEncoded = false, string contentFormat = "", uint contentLength = 0, byte[]? content = null) : this(new RawBitmapData(width, height, data), isEncoded, contentFormat, contentLength, content) { }

        public BitmapData(RawBitmapData rawBitmapData, bool isEncoded = false, string contentFormat = "", uint contentLength = 0, byte[]? content = null) : base(rawBitmapData.Width, rawBitmapData.Height, rawBitmapData.Matrix)
        {
            this.isEncoded = isEncoded;
            this.contentFormat = contentFormat;
            this.contentLength = contentLength;
            this.content = content;
        }

        public bool CanEncodeContentLength(uint contentLength)
        {
            return (/*!this.IsEncoded && */contentLength <= this.MaxContentLength);
        }

        public bool EncodeText(string text)
        {
            return this.Encode("txt", (uint)text.Length * 8, Encoding.UTF8.GetBytes(text));
        }

        public bool Encode(string contentFormat, uint contentLength, byte[] content)
        {
            // Cannot encode
            if (!this.CanEncodeContentLength(contentLength))
            {
                return false;
            }
            // Can encode
            else
            {
                // Content format to bits
                string headerContentFormatString = "";
                foreach (byte contentFormatChar in Encoding.ASCII.GetBytes(contentFormat.ToLower()))
                {
                    string headerContentFormatChar = "";

                    // ASCII numbers from 1 to 10
                    if (contentFormatChar > 47 && contentFormatChar < 58)
                    {
                        headerContentFormatChar = Convert.ToString(contentFormatChar - 47, 2).PadLeft(FILE_EXTENSION_CHAR_BIT_SIZE, '0');
                    }
                    // ASCII lowercase alphabet from 11 to 36
                    else if (contentFormatChar > 96 && contentFormatChar < 123)
                    {
                        headerContentFormatChar = Convert.ToString(contentFormatChar - 86, 2).PadLeft(FILE_EXTENSION_CHAR_BIT_SIZE, '0');
                    }

                    headerContentFormatString += headerContentFormatChar;
                }
                headerContentFormatString = headerContentFormatString.PadRight(FILE_EXTENSION_MAX_LENGTH_BIT_SIZE, '0');
                char[] headerContentFormatCharArray = headerContentFormatString.ToCharArray();

                // Content length to bits
                char[] headerContentLengthString = Convert.ToString(contentLength, 2).PadLeft((int)this.MaxContentLengthBitSize, '0').ToCharArray();

                // Content format + Content length => Header
                char[] headerString = new char[] { };
                headerString = headerString.Concat(headerContentFormatCharArray).Concat(headerContentLengthString).ToArray();

                // Content to bits
                char[] contentString = new char[] { };
                foreach (byte contentPart in content)
                {
                    char[] contentStringPart = Convert.ToString(contentPart, 2).PadLeft(8, '0').ToCharArray();
                    contentString = contentString.Concat(contentStringPart).ToArray();
                }

                // Header + Content => Data
                char[] dataString = new char[] { };
                dataString = dataString.Concat(headerString).Concat(contentString).ToArray();

                // Change class properties
                this.isEncoded = true;
                this.contentFormat = contentFormat;
                this.contentLength = contentLength;
                this.content = content;

                // Data => Matrix
                int x = 0;
                int y = 0;
                for (int i = 0; i < this.DataLength; i++)
                {
                    x = (int)((i / 3) / this.Height);
                    y = (int)((i / 3) % this.Height);
                    int z = i % 3;

                    // Change LSB if needed
                    if ((this.Matrix[x][y][z] % 2 == 0 && dataString[i] == '1')
                    || (this.Matrix[x][y][z] % 2 == 1 && dataString[i] == '0'))
                    {
                        this.matrix[x][y][z] ^= 1;
                    }
                }

                return true;
            }
        }

        public bool Decode()
        {
            // Cannot decode
            if(!this.IsEncodingDetected()) { return false; }

            // Matrix => Content format + Content length
            char[] headerContentFormatCharArray = new char[FILE_EXTENSION_MAX_LENGTH_BIT_SIZE];
            char[] headerContentLengthString = new char[this.MaxContentLengthBitSize];
            int x = 0;
            int y = 0;
            for (int i = 0; i < this.DataLength; i++)
            {
                x = (int)((i / 3) / this.Height);
                y = (int)((i / 3) % this.Height);
                int z = i % 3;

                // Get content format bits
                if (i < FILE_EXTENSION_MAX_LENGTH_BIT_SIZE)
                {
                    headerContentFormatCharArray[i] = char.Parse((this.Matrix[x][y][z] % 2).ToString());
                }
                // Get content length bits
                else if (i < this.HeaderSize)
                {
                    headerContentLengthString[i - FILE_EXTENSION_MAX_LENGTH_BIT_SIZE] = char.Parse((this.Matrix[x][y][z] % 2).ToString());
                }
            }

            // Change content format property
            byte[] contentFormatBytes = new byte[headerContentFormatCharArray.Length];
            bool contentFormatBytesInterrupted = false;
            for (int i = 0; i < headerContentFormatCharArray.Length; i += FILE_EXTENSION_CHAR_BIT_SIZE)
            {
                byte contentFormatByte = (byte)(Convert.ToByte(
                    headerContentFormatCharArray[i].ToString() +
                    headerContentFormatCharArray[i + 1].ToString() +
                    headerContentFormatCharArray[i + 2].ToString() +
                    headerContentFormatCharArray[i + 3].ToString() +
                    headerContentFormatCharArray[i + 4].ToString() +
                    headerContentFormatCharArray[i + 5].ToString(),
                    2
                ));

                if (contentFormatByte > 0 && contentFormatByte < 11)
                {
                    contentFormatBytes[i / FILE_EXTENSION_CHAR_BIT_SIZE] = (byte)(contentFormatByte + 47);
                }
                else if (contentFormatByte > 10 && contentFormatByte < 37)
                {
                    contentFormatBytes[i / FILE_EXTENSION_CHAR_BIT_SIZE] = (byte)(contentFormatByte + 86);
                }
                else
                {
                    byte[] contentFormatBytesPart = new byte[i / FILE_EXTENSION_CHAR_BIT_SIZE];
                    Array.Copy(contentFormatBytes, 0, contentFormatBytesPart, 0, i / FILE_EXTENSION_CHAR_BIT_SIZE);

                    this.contentFormat = Encoding.ASCII.GetString(contentFormatBytesPart);
                    contentFormatBytesInterrupted = true;
                    break;
                }
            }
            if (!contentFormatBytesInterrupted)
            {
                this.contentFormat = Encoding.ASCII.GetString(contentFormatBytes);
            }

            // Change content length property
            this.contentLength = (uint)Convert.ToUInt32(new string(headerContentLengthString), 2);

            // Matrix => Content
            char[] contentString = new char[this.ContentLength];
            x = 0;
            y = 0;
            for (int i = (int)this.HeaderSize; i < this.DataLength; i++)
            {
                x = (int)((i / 3) / this.Height);
                y = (int)((i / 3) % this.Height);
                int z = i % 3;

                // Get content bits
                if (i < this.DataLength)
                {
                    contentString[i - this.HeaderSize] = char.Parse((this.Matrix[x][y][z] % 2).ToString());
                }
            }

            // Change content property
            this.content = new byte[contentString.Length / 8];
            for (int i = 0; i < contentString.Length; i += 8)
            {
                this.content[i / 8] = Convert.ToByte(
                    contentString[i].ToString() +
                    contentString[i + 1].ToString() +
                    contentString[i + 2].ToString() +
                    contentString[i + 3].ToString() +
                    contentString[i + 4].ToString() +
                    contentString[i + 5].ToString() +
                    contentString[i + 6].ToString() +
                    contentString[i + 7].ToString(),
                    2
                );
            }

            // Change encoded property
            this.isEncoded = true;

            return true;
        }
    }

    public class ImageFile
    {
        private string path;

        public string Path { get { return this.path; } }

        public ImageFile(string path)
        {
            this.path = path;
        }

        public RawBitmapData? Read()
        {
            try
            {
                using (Bitmap bitmap = new Bitmap(this.Path))
                {
                    // Width x Height x Color (byte = unsigned 8-bit integer)
                    byte[][][] matrix = new byte[bitmap.Width][][];
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        matrix[x] = new byte[bitmap.Height][];
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            matrix[x][y] = new byte[3]
                            {
                            bitmap.GetPixel(x, y).R,
                            bitmap.GetPixel(x, y).G,
                            bitmap.GetPixel(x, y).B
                            };
                        }
                    }

                    return new RawBitmapData((uint)bitmap.Width, (uint)bitmap.Height, matrix);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool Write(RawBitmapData rawBitmapData)
        {
            try
            {
                using (Bitmap bitmap = new Bitmap((int)rawBitmapData.Width, (int)rawBitmapData.Height))
                {
                    // Width x Height x Color (byte = unsigned 8-bit integer)
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(
                                rawBitmapData.Matrix[x][y][0],
                                rawBitmapData.Matrix[x][y][1],
                                rawBitmapData.Matrix[x][y][2]
                            ));
                        }
                    }

                    string fileExtension = System.IO.Path.GetExtension(this.Path);
                    ImageFormat imageFormat;
                    switch (fileExtension.ToLower())
                    {
                        case @".bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case @".gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case @".jpg":
                        case @".jpeg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        default:
                            imageFormat = ImageFormat.Png;
                            break;
                    }

                    bitmap.Save(this.Path, imageFormat);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    public class BinaryFile
    {
        private string path;

        public string Path { get { return this.path; } }

        public BinaryFile(string path)
        {
            this.path = path;
        }

        public byte[]? Read()
        {
            try
            {
                byte[] content = null;

                using (FileStream fs = File.OpenRead(this.Path))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        content = br.ReadBytes((int)fs.Length);
                    }
                }

                return content;
            }
            catch
            {
                return null;
            }
        }

        public bool Write(byte[] content)
        {
            try
            {
                using (FileStream fs = File.OpenWrite(this.Path))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(content);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}