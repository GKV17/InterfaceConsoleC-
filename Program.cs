using System;
using System.Collections.Generic;
using System.Linq;

namespace firstpraktika
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Title = "Norton Commander";
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.CursorVisible = true;

            var files = GetDemoFiles();
            var leftItems = GetLeftPanelFiles();
            int selectedRowIndex = 0;

            while (true)
            {
                DrawInterface(files, selectedRowIndex, leftItems);

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.F10)
                    break;
                else if (key.Key == ConsoleKey.DownArrow && selectedRowIndex < files.Count - 1)
                    selectedRowIndex++;
                else if (key.Key == ConsoleKey.UpArrow && selectedRowIndex > 0)
                    selectedRowIndex--;
            }
        }

        static List<FileItem> GetDemoFiles()
        {
            return new List<FileItem>
            {
                new FileItem("..c", 0, new DateTime(2002,10,11,19,48,0), true),
                new FileItem("ncdd.exe", 128380, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("lemax.exe", 4372, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("filee", 0, new DateTime(2002,10,11,19,48,0), true),
                new FileItem("ncedit.exe", 255, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("tif2b.exe", 255, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("1view.exe", 255, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("nc.cfg", 417392, new DateTime(2002,10,12,9,2,0), false),
                new FileItem("ctor.exe", 255, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("temax.dat", 81738, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("main.exe", 54805, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("wpview.exe", 16133, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("5.ansi.set", 255, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("arview.exe", 54805, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("nclean.exe", 16133, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("norton.ini", 41914, new DateTime(1995,5,25,5,0,0), false),
                new FileItem("bitmap.exe", 81738, new DateTime(1995,5,25,5,0,0), false),
            };
        }

        static List<string> GetLeftPanelFiles()
        {
            var rawData = @"
ncdd
exe
telemax
exe
Ajaccgdo
ncedit
exe
tif2dib
exe
nc
nc_exit
cominclabel
cfg
ncff
exe
vector
exe
telemax
dat
ncmain
exe
exe
wpb2dib
exe
mc_exit
doc
ncnet
exe
wpview
exe
wpv2wmf
exe
123view
exe
Incsf
exe
Inc
ext
arcview exe
Incsi
exe
Inc
fil
bitmap
exe
nczip
exe
ncpscrip
hdr
c1p2dib
exe
packer
exe
Inc
hlp
dbview
exe
paraview
exe
ncff
hlp
draw2wmf
exe
pct2dib
exe
telemax
hlp
drw2wmf
exe
playwave
exe
Inc
ico
ico2dib
exe
q&aview
exe
Inc
ini
msp2dib
exe
rbview
exe
Incclean
ini
nc
ncclean
exe
refview
exe
norton
ini
helloexe
saver
".Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                  .Where(line => !string.IsNullOrWhiteSpace(line))
                  .ToList();

            var files = new List<string>();

            foreach (string line in rawData)
            {
                if (line.Contains(" "))
                {
                    files.Add(line.Replace(" ", "."));
                }
                else
                {
                    files.Add(line);
                }
            }

            var result = new List<string>();
            var commonExtensions = new HashSet<string> { "exe", "com", "cfg", "dat", "doc", "ext", "fil", "hdr", "hlp", "ico", "ini", "ns5" };

            for (int i = 0; i < files.Count; i++)
            {
                string current = files[i];

                if (current.Contains("."))
                {
                    result.Add(current);
                    continue;
                }

                if (i + 1 < files.Count && commonExtensions.Contains(files[i + 1]))
                {
                    result.Add($"{current}.{files[i + 1]}");
                    i++;
                }
                else
                {
                    result.Add(current);
                }
            }

            return result;
        }

        static void DrawInterface(List<FileItem> files, int selectedRowIndex, List<string> leftItems)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            DrawTopMenu();
            DrawPanels(files, selectedRowIndex, leftItems);
            DrawPanelFooters();
            DrawBottomMenu();

            if (selectedRowIndex < 18)
            {
                Console.SetCursorPosition(42, 3 + selectedRowIndex);
            }
            else
            {
                Console.SetCursorPosition(42, 20);
            }
        }

        static void DrawPanelFooters()
        {
            string footerText = "katalog   11.10.02    18.46";

            Console.SetCursorPosition(2, 21);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(footerText.PadRight(36));

            Console.SetCursorPosition(42, 21);
            Console.Write(footerText.PadRight(36));
        }

        static void DrawTopMenu()
        {
            Console.SetCursorPosition(0, 0);

            // Save original colors
            ConsoleColor originalFg = Console.ForegroundColor;
            ConsoleColor originalBg = Console.BackgroundColor;

            // Set green background for the entire line
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;

            // Left
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("eft ");

            // File
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("F");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("ile ");

            // Disk
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("D");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("isk ");

            // Commands
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("C");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("ommands ");

            // Right
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("R");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("ight                                       ");
            Console.BackgroundColor = ConsoleColor.Blue;

            Console.Write("  8 30");

            // Fill the rest of the line with green background
            int currentPos = Console.CursorLeft;
            while (currentPos < Console.WindowWidth)
            {
                Console.Write(" ");
                currentPos++;
            }

            // Restore original colors
            Console.ForegroundColor = originalFg;
            Console.BackgroundColor = originalBg;
        }
        static void DrawPanels(List<FileItem> files, int selectedRowIndex, List<string> leftItems)
        {
            // Save original colors
            ConsoleColor originalBg = Console.BackgroundColor;
            ConsoleColor originalFg = Console.ForegroundColor;

            // Draw borders in white
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            // Left panel with equally spaced vertical lines
            DrawBox(0, 1, 39, 22, "St.NC");
            DrawVerticalLines(0, 1, 20, new int[] { 13, 26 }, useHeavy: true);
            DrawHorizontalLineBottom(0, 20, 39, new int[] { 13, 26 });

            // Right panel with St.NC in the middle
            DrawBox(40, 1, 79, 22, "St.NC");
            DrawVerticalLines(40, 1, 20, new int[] { 53, 63, 73 }, useHeavy: true);
            DrawHorizontalLineBottom(40, 20, 79, new int[] { 53, 63, 73 });

            // Restore yellow color for headers
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Left panel headers with down arrow using ASCII
            Console.SetCursorPosition(2, 2); Console.Write("C: \u2193 name");
            Console.SetCursorPosition(14, 2); Console.Write("name");
            Console.SetCursorPosition(27, 2); Console.Write("size");

            // Right panel headers with down arrow using ASCII
            Console.SetCursorPosition(42, 2); Console.Write("C:↓name");
            Console.SetCursorPosition(54, 2); Console.Write("size");
            Console.SetCursorPosition(64, 2); Console.Write("date");
            Console.SetCursorPosition(74, 2); Console.Write("time");

            // Restore original colors for content
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;

            RenderLeftPanel(leftItems);
            RenderRightPanel(files, selectedRowIndex);
        }

        static void DrawBox(int left, int top, int right, int bottom, string middleText = "")
        {
            // Draw top line with middle text
            Console.SetCursorPosition(left, top);
            Console.Write('\u2554');

            int middlePos = left + (right - left) / 2 - middleText.Length / 2;
            int currentPos = left + 1;

            while (currentPos < right)
            {
                if (currentPos == middlePos && !string.IsNullOrEmpty(middleText))
                {
                    Console.Write(middleText);
                    currentPos += middleText.Length;
                }
                else if (currentPos < right)
                {
                    Console.Write('\u2550');
                    currentPos++;
                }
            }

            Console.Write('\u2557');

            // Draw sides
            for (int y = top + 1; y < bottom; y++)
            {
                Console.SetCursorPosition(left, y);
                Console.Write('\u2551');
                Console.SetCursorPosition(right, y);
                Console.Write('\u2551');
            }

            // Draw bottom line
            Console.SetCursorPosition(left, bottom);
            Console.Write('\u255A');
            for (int i = left + 1; i < right; i++) Console.Write('\u2550');
            Console.Write('\u255D');
        }

        static void RenderLeftPanel(List<string> items)
        {
            int row = 3;
            int colIndex = 0;
            int[] colStarts = { 2, 14, 27 };
            int[] colWidths = { 11, 12, 11 };

            foreach (var item in items)
            {
                if (row > 20) break;

                Console.SetCursorPosition(colStarts[colIndex], row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ShortenName(item, colWidths[colIndex] - 1).PadRight(colWidths[colIndex] - 1));

                colIndex++;
                if (colIndex >= 3)
                {
                    colIndex = 0;
                    row++;
                }
            }
        }

        static void RenderRightPanel(List<FileItem> files, int selectedRowIndex)
        {
            int row = 3;
            int index = 0;
            int[] colStarts = { 42, 54, 64, 74 };
            int[] colWidths = { 11, 9, 9, 5 };

            foreach (var file in files)
            {
                if (row > 20) break;

                bool isSelected = (index == selectedRowIndex);
                ConsoleColor bg = isSelected ? ConsoleColor.Cyan : ConsoleColor.DarkBlue;
                ConsoleColor fg = isSelected ? ConsoleColor.Black :
                                  file.IsDirectory ? ConsoleColor.Cyan : ConsoleColor.Yellow;

                for (int i = 0; i < colStarts.Length; i++)
                {
                    Console.SetCursorPosition(colStarts[i], row);
                    Console.BackgroundColor = bg;
                    Console.ForegroundColor = fg;
                    Console.Write(new string(' ', colWidths[i]));
                }

                Console.SetCursorPosition(42, row);
                Console.Write(ShortenName(file.Name, 10).PadRight(10));

                Console.SetCursorPosition(54, row);
                string sizeText = file.IsDirectory ? " katalog " : file.Size.ToString();
                Console.Write(sizeText.PadLeft(7));

                Console.SetCursorPosition(64, row);
                Console.Write(file.DateTime.ToString("dd.MM.yy"));

                Console.SetCursorPosition(74, row);
                Console.Write(file.DateTime.ToString("HH:mm"));

                if (isSelected)
                {
                    Console.BackgroundColor = bg;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(54, row); Console.Write('►');
                    Console.SetCursorPosition(62, row); Console.Write('◄');
                }

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;

                row++;
                index++;
            }
        }

        static void DrawBottomMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 23);
            Console.Write("C:NC\u005C>                                                                          ");
            Console.SetCursorPosition(0, 24);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("1Help     2Menu    3View    4Edit    5Copy    6Move   7MkDir    8Delete   10Quit");



        }





        static void DrawVerticalLines(int left, int top, int height, int[] positions, bool useHeavy = false)
        {
            char vert = useHeavy ? '\u2502' : '\u2502';
            char topConnector = useHeavy ? '\u2564' : '\u2564';

            foreach (int x in positions)
            {
                Console.SetCursorPosition(x, top);
                Console.Write(topConnector);
                for (int y = top + 1; y < top + height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(vert);
                }
            }
        }

        static void DrawHorizontalLineBottom(int left, int y, int right, int[] verticalPositions)
        {
            for (int x = left + 1; x < right; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(Array.Exists(verticalPositions, pos => pos == x) ? '\u2534' : '\u2500');
            }
        }

        static string ShortenName(string name, int maxLen)
        {
            if (string.IsNullOrEmpty(name)) return "";
            if (name.Length <= maxLen) return name;

            string ext = System.IO.Path.GetExtension(name);
            int extLen = ext.Length;
            int stemLen = maxLen - 3 - extLen;

            if (stemLen <= 0)
                return name.Substring(0, maxLen - 1) + "~";

            return name.Substring(0, stemLen) + "..." + ext;
        }
    }

    class FileItem
    {
        public string Name { get; }
        public int Size { get; }
        public DateTime DateTime { get; }
        public bool IsDirectory { get; }

        public FileItem(string name, int size, DateTime dt, bool isDir)
        {
            Name = name;
            Size = size;
            DateTime = dt;
            IsDirectory = isDir;
        }
    }
}