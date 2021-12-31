// PAL samples

// Sample 1
var palette = new PAL();

palette[0] = new Color(0, 0, 0);
palette[1] = new Color(63, 0, 0);
palette[2] = new Color(63, 63, 63);
...

// Sample 2
var colors = new Color[256];
... // Setup each Color array element value.
var palette = new PAL(colors);

// Sample 3
var palette = new PAL("DIV.PAL");

// Sample 4
byte[] buffer = System.IO.File.ReadAllBytes("DIV.PAL");
var palette = new PAL(buffer);

// Sample 5
var palette = PAL.FromImage("background.png");

// Sample 6
byte[] buffer = System.IO.File.ReadAllBytes("background.png");
var palette = PAL.FromImage(buffer);

// Sample 7
var palette = PAL.FromImage("background.png", true);
...
byte[] buffer = System.IO.File.ReadAllBytes("background.png");
var palette = PAL.FromImage(buffer, true);

// Sample 8
var palette = PAL.FromImage("COIN.MAP");
var palette = PAL.FromImage("PLAYER.FPG");

// Sample 9
Color color = palette[42]; // Reads the color at index 42.

// Sample 10
foreach (Color color in palette)
{
    Console.WriteLine(color); // Prints the current color value in console.
}

// Sample 11
palette[42] = new Color(0, 16, 63); // Writes the color at index 42.

// Sample 12
palette.Sort();

// Sample 13
palette.Save("NEW.PAL");
